using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class UFOMoveMentAnimation : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float duration = 5f;
    [SerializeField] private int stopCount = 1;
    [SerializeField] private float stopDelay = 5f;

    [SerializeField] private UFOLightAnimation ufoLightAnim;

    [SerializeField] private GameObject[] aliens;

    public void Play()
    {
        StartCoroutine(MoveAlongSplineWithStops());
    }

    private IEnumerator MoveAlongSplineWithStops()
    {
        float step = 1f / (stopCount + 1);
        
        for (int i = 0; i <= stopCount; i++)
        {
            float startT = i * step;
            float endT = (i + 1) * step;
            float t = startT;
            float travelTime = duration * step;

            while (t < endT)
            {
                t += Time.deltaTime / travelTime;

                Vector3 localPos = splineContainer.Spline.EvaluatePosition(t);
                Vector3 worldPos = splineContainer.transform.TransformPoint(localPos);
                transform.position = worldPos;

                Vector3 localTangent = splineContainer.Spline.EvaluateTangent(t);
                Vector3 worldTangent = splineContainer.transform.TransformDirection(localTangent);
                transform.forward = worldTangent.normalized;

                yield return null;
            }

            if (i < stopCount)
            {
                if (ufoLightAnim != null)
                {
                    ufoLightAnim.DownLight();

                    yield return new WaitForSeconds(stopDelay - 1.5f);
                    
                    foreach (GameObject alien in aliens)
                    {
                        if (alien != null)
                            StartCoroutine(FadeIn(alien, 3f));
                    }
                    
                    ufoLightAnim.UpLight();
                    
                    yield return new WaitForSeconds(stopDelay);
                }
            }
        }
    }
    
    private IEnumerator FadeIn(GameObject obj, float duration)
    {
        SkinnedMeshRenderer[] renderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer rend in renderers)
        {
            rend.material = new Material(rend.material);
        }
    
        foreach (SkinnedMeshRenderer rend in renderers)
        {
            Color color = rend.material.color;
            color.a = 0f;
            rend.material.color = color;
            rend.material.SetColor("_EmissionColor", Color.white * 1f);
        }

        obj.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / duration);

            float alpha = progress;
            float emissionIntensity = 1f - progress;

            foreach (SkinnedMeshRenderer rend in renderers)
            {
                // 알파값 조절
                Color color = rend.material.color;
                color.a = alpha;
                rend.material.color = color;

                // Emission값 조절
                Color emissionColor = Color.white * emissionIntensity;
                rend.material.SetColor("_EmissionColor", emissionColor);
            }
            yield return null;
        }
    }
}

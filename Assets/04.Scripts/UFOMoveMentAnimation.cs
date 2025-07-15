using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class UFOMoveMentAnimation : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float stopDelay = 5f;

    [SerializeField] private UFOLightAnimation ufoLightAnim;
    
    [SerializeField] private int animationKnotIndex = 1;
    [SerializeField] private GameObject[] aliens;

    public void Play()
    {
        StartCoroutine(MoveAlongSplineWithStops());
    }

    private IEnumerator MoveAlongSplineWithStops()
    {
        Spline spline = splineContainer.Spline;
        var knots = spline.Knots.ToArray();

        if (animationKnotIndex >= knots.Length)
        {
            yield break;
        }

        Vector3 animKnotLocal = knots[animationKnotIndex].Position;
        Vector3 animKnotWorld = splineContainer.transform.TransformPoint(animKnotLocal);
        bool animPlayed = false;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            Vector3 localPos = spline.EvaluatePosition(t);
            Vector3 worldPos = splineContainer.transform.TransformPoint(localPos);
            transform.position = worldPos;

            Vector3 localTangent = spline.EvaluateTangent(t);
            Vector3 worldTangent = splineContainer.transform.TransformDirection(localTangent);
            transform.forward = worldTangent.normalized;

            if (!animPlayed && Vector3.Distance(worldPos, animKnotWorld) < 0.2f)
            {
                animPlayed = true;

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

            yield return null;
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

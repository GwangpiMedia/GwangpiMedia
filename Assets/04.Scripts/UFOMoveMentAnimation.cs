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

    public void PlaySplineMove()
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

                Vector3 pos = splineContainer.Spline.EvaluatePosition(t);
                transform.position = pos;

                Vector3 tangent = splineContainer.Spline.EvaluateTangent(t);
                transform.forward = tangent.normalized;

                yield return null;
            }

            if (i < stopCount)
            {
                if (ufoLightAnim != null)
                {
                    ufoLightAnim.DownLight();

                    yield return new WaitForSeconds(stopDelay - 1.5f);

                    ufoLightAnim.UpLight();
                    
                    yield return new WaitForSeconds(stopDelay);
                }
            }
        }
    }
}

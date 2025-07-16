using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class BuzzAnimation : MonoBehaviour
{
    [SerializeField] private Animator buzzAnimator;

    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float triggerThreshold = 0.1f;
    [SerializeField] private float duration = 4f;

    private bool prideTriggered = false;
    private bool razerTriggered = false;

    public void MoveBuzz()
    {
        StartCoroutine(MoveBuzzAlongSpline());
    }

    private IEnumerator MoveBuzzAlongSpline()
    {
        float t = 0f;

        Vector3 knot1World = Vector3.zero;
        Vector3 knot2World = Vector3.zero;

        BezierKnot[] knots = splineContainer.Spline.ToArray();

        if (knots.Length >= 3)
        {
            knot1World = splineContainer.transform.TransformPoint(knots[1].Position);
            knot2World = splineContainer.transform.TransformPoint(knots[2].Position);
        }
        else
        {
            yield break;
        }

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            Vector3 localPos = splineContainer.Spline.EvaluatePosition(t);
            Vector3 worldPos = splineContainer.transform.TransformPoint(localPos);
            transform.position = worldPos;

            if (!prideTriggered && Vector3.Distance(worldPos, knot1World) < triggerThreshold)
            {
                buzzAnimator.SetTrigger("Pride");
                prideTriggered = true;
            }

            if (!razerTriggered && Vector3.Distance(worldPos, knot2World) < triggerThreshold)
            {
                buzzAnimator.SetTrigger("Razer");
                razerTriggered = true;
            }

            yield return null;
        }
    }
}


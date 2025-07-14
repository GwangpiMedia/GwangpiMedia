using UnityEngine;
using UnityEngine.Splines;

public class AnimationEvent : MonoBehaviour
{
    [Header("Woody")]
    [SerializeField] private GameObject _headObj;
    [SerializeField] private float _bigHeadSize = 2.0f;

    [Header("Characters")]
    [SerializeField] private SplineAnimate _trainAndHammSplineAnimate;
    [SerializeField] private SplineAnimate _rexSplineAnimate;
    [SerializeField] private SplineAnimate _bearSplineAnimate;

    public void BigHead()
    {
        _headObj.transform.localScale = new Vector3(_bigHeadSize, _bigHeadSize, _bigHeadSize);
    }

    public void NormalHead()
    {
        _headObj.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void MoveTrain()
    {
        _trainAndHammSplineAnimate.Play();
    }

    public void MoveRex()
    {
        _rexSplineAnimate.Play();
    }

    public void MoveBear()
    {
        _bearSplineAnimate.Play();
    }
}

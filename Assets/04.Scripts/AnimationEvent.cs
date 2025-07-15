using UnityEngine;
using UnityEngine.Splines;

public class AnimationEvent : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private DoorAnimator _door;
    
    [Header("Woody")]
    [SerializeField] private GameObject _headObj;
    [SerializeField] private float _bigHeadSize = 2.0f;

    [Header("Characters")]
    [SerializeField] private SplineAnimate _trainAndHammSplineAnimate;
    [SerializeField] private SplineAnimate _rexSplineAnimate;
    [SerializeField] private SplineAnimate _bearSplineAnimate;
    [SerializeField] private UFOMoveMentAnimation _ufoMoveMentAnimation;
    [SerializeField] private SplineAnimate _solider1Animate;
    [SerializeField] private SplineAnimate _solider2Animate;
    [SerializeField] private SplineAnimate _solider3Animate;
    [SerializeField] private BuzzAnimation _buzzAnimation;

    public void DoorOpen()
    {
        _door.OpenDoors();
    }

    public void DoorClose()
    {
        _door.CloseDoors();
    }
    
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

    public void MoveUFO()
    {
        _ufoMoveMentAnimation.Play();
    }

    public void MoveSolider()
    {
        _solider1Animate.Play();
        _solider2Animate.Play();
        _solider3Animate.Play();
    }

    public void MoveBuzz()
    {
        _buzzAnimation.MoveBuzz();
    }

}

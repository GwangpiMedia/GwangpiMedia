using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class AnimationEvent : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private DoorAnimator _door;
    
    [Header("Woody")] 
    [SerializeField] private Transform _headObj;
    [SerializeField] private float _bigHeadSize = 2f;
    [SerializeField] private float _normalHeadSize = 1f;
    [SerializeField] private float _scaleDuration = 0.5f;
                     
    private Coroutine _scalingCoroutine;

    [Header("Characters")]
    [SerializeField] private SplineAnimate _trainAndHammSplineAnimate;
    [SerializeField] private SplineAnimate _rexSplineAnimate;
    [SerializeField] private SplineAnimate _bearSplineAnimate;
    [SerializeField] private UFOMoveMentAnimation _ufoMoveMentAnimation;
    [SerializeField] private SplineAnimate _solider1Animate;
    [SerializeField] private SplineAnimate _solider2Animate;
    [SerializeField] private SplineAnimate _solider3Animate;
    [SerializeField] private BuzzAnimation _buzzAnimation;
    [SerializeField] private ChairAnimation _chairAnimation;
    [SerializeField] private SplineAnimate _spaceShipAnimate;

    public void DoorOpen()
    {
        _door.OpenDoors();
    }

    public void DoorClose()
    {
        _door.CloseDoors();
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

    public void MoveChair()
    {
        _chairAnimation.KnockOverChair();
    }

    public void BigHead()
    {
        if (_scalingCoroutine != null) StopCoroutine(_scalingCoroutine);
        _scalingCoroutine = StartCoroutine(ScaleHead(Vector3.one * _bigHeadSize, _scaleDuration));
    }
    
    public void NormalHead()
    {
        if (_scalingCoroutine != null) StopCoroutine(_scalingCoroutine);
        _scalingCoroutine = StartCoroutine(ScaleHead(Vector3.one * _normalHeadSize, _scaleDuration));
    }
    

    private IEnumerator ScaleHead(Vector3 targetScale, float duration)
    {
        Vector3 startScale = _headObj.localScale;
        float elapsed = 0f;
    
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            _headObj.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
    
        _headObj.localScale = targetScale;
    }

    public void Knock()
    {
        SoundManager.Instance.PlaySFX("KnockingGlass");
    }

    public void SpaceShipMove()
    {
        _spaceShipAnimate.Play();
    }

    public void SpaceshipSound()
    {
        SoundManager.Instance.PlaySFX("Spaceship");
    }
}

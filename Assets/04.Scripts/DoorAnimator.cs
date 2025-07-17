using UnityEngine;
using System.Collections;

public class DoorAnimator : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    [SerializeField] private float slideDistance = 1.5f;
    [SerializeField] private float duration = 1f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;

    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;
    
    private void Awake()
    {   
        leftClosedPos = leftDoor.localPosition;
        rightClosedPos = rightDoor.localPosition;
        Vector3 rightDir = rightDoor.right.normalized;
        
        float adjustedSlideDistance = slideDistance / Mathf.Sqrt(2);
        leftOpenPos = new Vector3(
            leftClosedPos.x - adjustedSlideDistance,
            leftClosedPos.y,
            leftClosedPos.z + adjustedSlideDistance);
        rightOpenPos = rightClosedPos + rightDir * slideDistance;
    }

    public void OpenDoors()
    {
        SoundManager.Instance.PlayBGM("BGM");
        StopAllCoroutines();
        StartCoroutine(SlideDoor(leftDoor, leftClosedPos, leftOpenPos));
        StartCoroutine(SlideDoor(rightDoor, rightClosedPos, rightOpenPos));
    }

    public void CloseDoors()
    {
        StopAllCoroutines();
        StartCoroutine(SlideDoor(leftDoor, leftOpenPos, leftClosedPos));
        StartCoroutine(SlideDoor(rightDoor, rightOpenPos, rightClosedPos));
    }

    private IEnumerator SlideDoor(Transform door, Vector3 from, Vector3 to)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            door.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }
    }
}

using System.Collections;
using UnityEngine;

public class ChairAnimation : MonoBehaviour
{
    [Header("회전 목표 각도 (Euler)")]
    [SerializeField] private Vector3 endEulerAngles = new Vector3(-90f, 0f, 0f);

    [Header("이동 거리")]
    [SerializeField] private Vector3 moveOffset = new Vector3(0f, 0f, -0.3f);

    [SerializeField] private float rotateDuration = 0.5f;

    public void KnockOverChair()
    {
        StartCoroutine(RotateAndMoveChair());
    }

    private IEnumerator RotateAndMoveChair()
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(endEulerAngles);

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + transform.TransformDirection(moveOffset);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / rotateDuration;

            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
    }
}

using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject headObj;
    [SerializeField] private float bigHeadSize = 2.0f;

    public void BigHead()
    {
        headObj.transform.localScale = new Vector3(bigHeadSize, bigHeadSize, bigHeadSize);
    }

    public void NormalHead()
    {
        headObj.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}

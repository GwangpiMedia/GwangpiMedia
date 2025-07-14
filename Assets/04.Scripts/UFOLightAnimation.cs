using System.Collections;
using UnityEngine;

public class UFOLightAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float minScaleZ = 0f;
    [SerializeField] private float maxScaleZ = 1.8f;
    [SerializeField] private GameObject UFO;

    private bool isAnimating = false;


    public void DownLight()
    {
        StartCoroutine(DownAnimateScale());
    }

    public void UpLight()
    {
        StartCoroutine(UpAnimateScale());
    }

    private IEnumerator DownAnimateScale()
    {
        while (UFO.transform.localScale.z < maxScaleZ)
        {
            Vector3 scale = UFO.transform.localScale;
            scale.z += speed * Time.deltaTime;
            UFO.transform.localScale = scale;
            yield return null;
        }
    }

    private IEnumerator UpAnimateScale()
    {
        while (UFO.transform.localScale.z > minScaleZ)
        {
            Vector3 scale = UFO.transform.localScale;
            scale.z -= speed * Time.deltaTime;
            UFO.transform.localScale = scale;
            yield return null;
        }

    }
}

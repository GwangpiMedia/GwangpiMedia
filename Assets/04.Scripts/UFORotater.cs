using UnityEngine;

public class UFORotater : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationSpeed = 90f;

    private void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

}

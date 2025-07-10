using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool isMoving = false;
    private float goalZ;

    [SerializeField] private float targetZ = -0.5f;
    [SerializeField] private float moveSpeed = 1f;

    public void MoveZ()
    {
        goalZ = transform.position.z + targetZ;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            float newZ = Mathf.MoveTowards(transform.position.z, goalZ, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

            if (Mathf.Abs(transform.position.z - goalZ) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
}

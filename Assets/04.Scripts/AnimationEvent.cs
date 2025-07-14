using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [Header("Woody")]
    [SerializeField] private GameObject _headObj;
    [SerializeField] private float _bigHeadSize = 2.0f;

    [Header("Characters")]
    [SerializeField] private MovableObject _trainAndHamm;
    [SerializeField] private MovableObject _rex;

    private void Update()
    {
        UpdateMovement(_trainAndHamm);
        UpdateMovement(_rex);
    }

    public void BigHead()
    {
        _headObj.transform.localScale = new Vector3(_bigHeadSize, _bigHeadSize, _bigHeadSize);
    }

    public void NormalHead()
    {
        _headObj.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void UpdateMovement(MovableObject movable)
    {
        if (!movable.isMoving || movable.obj == null) return;

        Transform Object = movable.obj.transform;
        Object.position = Vector3.MoveTowards(Object.position, movable.goalPos.position, movable.speed * Time.deltaTime);

        if (Vector3.Distance(Object.position, movable.goalPos.position) < 0.1f)
        {
            movable.isMoving = false;
        }
    }

    public void MoveTrain()
    {
        _trainAndHamm.isMoving = true;
    }

    public void MoveRex()
    {
        _rex.isMoving = true;
    }
}

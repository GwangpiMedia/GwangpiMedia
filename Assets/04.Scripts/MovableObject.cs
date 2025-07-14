using UnityEngine;

[System.Serializable]
public class MovableObject
{
    public GameObject obj;
    public Transform goalPos;
    public float speed;
    [HideInInspector] public bool isMoving;
}


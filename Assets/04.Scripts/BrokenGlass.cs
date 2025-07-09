using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BrokenGlass : MonoBehaviour
{
    [Header("Glass Settings")]
    [SerializeField] private GameObject _originalGlass;
    [SerializeField] private GameObject _brokenGlassPrefab;

    [Header("Physics Settings")]
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _spread = 2f;

    private GameObject _brokenInstance;
    [SerializeField] private Camera mainCamera;
    
    public void StartBreakSequence()
    {
        if (_originalGlass == null || _brokenGlassPrefab == null)
        {
            return;
        }

        _originalGlass.SetActive(false);
        _brokenInstance = Instantiate(_brokenGlassPrefab);

    }

    public void BrokenGlasses()
    {
        if (_brokenInstance == null)
        {
            return;
        }
        ExplodeShards();
    }

    private void ExplodeShards()
    {
        Rigidbody[] shardRigidbodies = _brokenInstance.transform.GetComponentsInChildren<Rigidbody>();

        Vector3 forward = mainCamera.transform.forward;
        
        foreach (Rigidbody rb in shardRigidbodies)
        {
            Vector3 randomDir = (-forward + Random.insideUnitSphere * _spread).normalized;
            rb.AddForce(randomDir * _explosionForce, ForceMode.Impulse);
            Destroy(rb.gameObject, 2f);
        }
    }
}

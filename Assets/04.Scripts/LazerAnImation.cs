using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAnImation : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDuration = 0.2f;
    [SerializeField] private float laserLength = 10f;

    [SerializeField] private Transform firePoint;

    public void PlayLaser()
    {
        StartCoroutine(FireLaser());
    }

    private IEnumerator FireLaser()
    {
        Vector3 start = firePoint.position;
        Vector3 end = firePoint.position + firePoint.forward * laserLength;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(laserDuration);

        lineRenderer.enabled = false;
    }
    
}

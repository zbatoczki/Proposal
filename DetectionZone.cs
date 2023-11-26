using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public Collider2D detectedCollider = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollider = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ClearDetectionCollider();
    }

    public void ClearDetectionCollider()
    {
        detectedCollider = null;
    }
}

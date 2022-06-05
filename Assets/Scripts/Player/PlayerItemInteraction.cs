using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{

    public Transform detectionPoint;
    private const float detectionCircleRadius = 0.4f;
    public LayerMask detectionLayer;

    // Cache the detected item
    public GameObject detectedItem;

    void Update()
    {
        if (DetectObject())
        {
            detectedItem.GetComponent<Item>().beingInteracted();
        }
    }

    bool DetectObject()
    {
        Collider2D currentObj = Physics2D.OverlapCircle(detectionPoint.position, 
            detectionCircleRadius, detectionLayer);
        if (currentObj != null)
        {
            detectedItem = currentObj.gameObject;
            return true;
        }
        else
        {
            detectedItem = null;
            return false;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(detectionPoint.position, detectionCircleRadius);
    }
}

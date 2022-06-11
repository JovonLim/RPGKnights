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

    // Item to be dropped by player
    public GameObject dropItem;

    private void Update()
    {
        if (DetectObject() && !FindObjectOfType<Inventory>().IsInventoryFull())
        {
            detectedItem.GetComponent<Item>().BeingPickedUp();
        }
    }

    private bool DetectObject()
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

    public void DropItem(GameObject item)
    {
        dropItem = item;
        dropItem.gameObject.SetActive(true);
        var obj = Instantiate(dropItem, 
            new Vector3(detectionPoint.localScale.x, detectionPoint.localScale.y, detectionPoint.localScale.z), 
            transform.rotation);
        obj.name = dropItem.name;
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenOrderUI : MonoBehaviour
{
    private OrderUI orderUI; 
    private LayerMask buyersMask;
    private const int maxDistance = 8;

    private void Start()
    {
        buyersMask = LayerMask.GetMask("Buyer");
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, buyersMask) && hit.collider.gameObject.TryGetComponent(out Buyer buyer))
        {
                orderUI = hit.collider.gameObject.GetComponent<OrderUI>();
                orderUI.OrderPanel.transform.rotation = Quaternion.LookRotation(orderUI.transform.position - transform.position,Vector3.up);
                orderUI.OrderPanel.transform.rotation = new Quaternion(0f,orderUI.OrderPanel.transform.rotation.y , 0f, orderUI.OrderPanel.transform.rotation.w);
                orderUI.OpenPanel();
        }
        else if(orderUI)
        {              
                orderUI.ClosePanel();
                orderUI = null;
        }
    }
}

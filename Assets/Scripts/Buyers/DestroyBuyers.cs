using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBuyers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Buyer buyer))
        {
            Destroy(other.gameObject);
        }
    }
}

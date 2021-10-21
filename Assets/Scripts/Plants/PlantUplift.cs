using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantUplift : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlantAI plantAI))
        {
            if(other.gameObject.transform.position.y >= 2.8f)
            {
                Destroy(other.gameObject);
            }
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 1f * Time.deltaTime, other.gameObject.transform.position.z);
            plantAI.IsRise = true;
        }
    }
}

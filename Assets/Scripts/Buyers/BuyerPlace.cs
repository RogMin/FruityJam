using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerPlace : MonoBehaviour
{
    [HideInInspector] public bool IsEmpty;
    [HideInInspector] public GameObject BuyerInPlace;
    [HideInInspector] public GameObject LookPoint;
    private void Awake()
    {
        IsEmpty = true;
    }
    public void BuyerInPlaceAdd(GameObject gameobject)
    {
        BuyerInPlace = gameobject;
        BuyerInPlace.GetComponent<BuyerAnim>().SwitchAnimation(true);
    }
    public void TakePlace(GameObject buyer)
    {
        IsEmpty = false;
        BuyerInPlace = buyer;
    }
    public void FreeSpace()
    {
        BuyerInPlace.GetComponent<BuyerAnim>().SwitchAnimation(true);
        IsEmpty = true;
        BuyerInPlace = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == BuyerInPlace)
        {
            BuyerInPlace.transform.rotation = Quaternion.FromToRotation(BuyerInPlace.transform.rotation.eulerAngles, LookPoint.transform.position);
            BuyerInPlace.GetComponent<BuyerAnim>().SwitchAnimation(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == BuyerInPlace)
        {
            BuyerInPlace.GetComponent<BuyerAnim>().SwitchAnimation(true);
        }
    }
}

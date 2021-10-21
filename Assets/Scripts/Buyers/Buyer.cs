using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Buyer : MonoBehaviour
{
    [SerializeField] private Transform PointOfDestruction;//Set in inspector
    [SerializeField] private NavMeshAgent navmesh; //Set in inspector
    private Buyers buyersManager;
    private Transform placeCoord;
    void Start()
    {
        buyersManager = Buyers._instance;
        GoAtPoint();
    }
    private void GoAtPoint()
    {
        placeCoord = buyersManager.GetFreePoint(this.gameObject);
        navmesh.SetDestination(placeCoord.position);
        GetComponent<BuyerAnim>().SwitchAnimation(true);
    }
    public void GoAtNextPoint()
    {
        placeCoord.gameObject.GetComponent<BuyerPlace>().FreeSpace();
        placeCoord = buyersManager.GetFreePoint(this.gameObject);
        navmesh.SetDestination(placeCoord.position);
        GetComponent<BuyerAnim>().SwitchAnimation(true);
    }
    public void FreeSpace(bool orderIsComplete) //Вызывается для того, чтобы human шёл к точке уничтожения
    {
        buyersManager.BuyersList.Remove(this);
        placeCoord.gameObject.GetComponent<BuyerPlace>().FreeSpace();
        navmesh.SetDestination(PointOfDestruction.position);
        buyersManager.mainQuestion.PlayerChangeOrder(orderIsComplete);
        buyersManager.UpdateBuyersPoints();
        GetComponent<BuyerAnim>().SwitchAnimation(true);
    }
}

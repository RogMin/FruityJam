using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyers : MonoBehaviour
{
    public static Buyers _instance;
    [SerializeField] private GameObject[] BuyerPrefabs; //set in inspector
    [SerializeField] private float MinTimeSpawn = 25f;
    [SerializeField] private float MaxTimeSpawn = 45f;    
    [HideInInspector]public MainQuestion mainQuestion;
    [HideInInspector] public List<Buyer> BuyersList = new List<Buyer>();
    private int MaximumBuyers;
    private List<BuyerPlace> placesForBuyer = new List<BuyerPlace>();   

    void Awake()
    {
        _instance = this;
        foreach (var place in GameObject.FindGameObjectWithTag("BuyersPlaces").GetComponentsInChildren<BuyerPlace>())
        {
            placesForBuyer.Add(place);
        }
        mainQuestion = GameObject.FindObjectOfType<MainQuestion>();
        MaximumBuyers = placesForBuyer.Count;
        StartCoroutine(SpawnBuyers());
    }
    public static Buyers Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Buyers>();
                return _instance;
            }
            return _instance;
        }
    }
    public void NextBuyer()
    {
        if(BuyersList.Count > 0)
        {
            BuyersList[0].FreeSpace(false);
        }
    }
    public void UpdateBuyersPoints()
    {
        foreach (var buyer in BuyersList)
        {
            buyer.GoAtNextPoint();
        }
    }
    public Transform GetFreePoint(GameObject buyer)
    {
        foreach (var place in placesForBuyer)
        {
            if (place.IsEmpty)
            {
                place.TakePlace(buyer);
                return place.gameObject.transform;
            }
        }
        return null;
    }
    private IEnumerator SpawnBuyers()
    {
        while (true)
        {
            if (BuyersList.Count < MaximumBuyers)
            {
                BuyersList.Add(Instantiate(BuyerPrefabs[Random.Range(0, BuyerPrefabs.Length)], gameObject.transform.position, Quaternion.identity).GetComponent<Buyer>());
                yield return new WaitForSeconds(Random.Range(MinTimeSpawn, MaxTimeSpawn));
            }
            else yield return new WaitForEndOfFrame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    [SerializeField] private float GrowTime; //set in inspector
    private GameObject[] PlantsPrefabs;
    private void Awake()
    {
        PlantsPrefabs = Resources.LoadAll<GameObject>("Plants");
    }
    private void Start()
    {     
        StartCoroutine(Spawn());
    }
    public void SpawnRandomPlant()
    {
        Instantiate(PlantsPrefabs[Random.Range(0, PlantsPrefabs.Length)], gameObject.transform.position, Quaternion.identity);
    }
    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(GrowTime);
            SpawnRandomPlant();
        } 
    }
 }



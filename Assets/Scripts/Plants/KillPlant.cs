using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlant : MonoBehaviour
{
    public AudioClip KillSound;
    public AudioSource audioSource;
    public float MaxDistance;
    private Inventory inventory;
    private Camera mainCamera;
    private Vector3 ScreeCenter;
   
    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        mainCamera = Camera.main;
        ScreeCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && inventory.Item && inventory.Item.CompareTag("Knife"))
        {
            Ray ray = mainCamera.ScreenPointToRay(ScreeCenter);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, MaxDistance))
            {
                if (hit.collider.gameObject.TryGetComponent(out PlantAI plantAI))
                {
                    Instantiate(plantAI.DeathPrefab, hit.collider.transform.position, Quaternion.identity);
                    audioSource.PlayOneShot(KillSound);
                    Destroy(hit.collider.gameObject);                    
                }
            } 
        }
    }
}

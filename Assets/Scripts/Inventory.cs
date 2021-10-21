using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector] public GameObject Item;
    [SerializeField] private float maxDistance = 30f;
    [SerializeField] private Camera ItemCamera; //set in inspector   
    [SerializeField] private GameObject DefaultHandTransform; //set in inspector
    private Vector3 ScreeCenter;
    private int itemLayer = 7;
    private LayerMask DropItemLayer;
    private NextButton nextButton;

    private void Start()
    {
        nextButton = GameObject.FindObjectOfType<NextButton>();
        ScreeCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        itemLayer = LayerMask.GetMask("Item");
        DropItemLayer = LayerMask.GetMask("ItemInFloor");
    }
    public void PickupItem(Item item)
    {
        if (!Item)
        {
            Item = item.gameObject;
            if (item.HandTransform)
            {
                DefaultHandTransform.SetActive(false);
                item.HandTransform.SetActive(true);
            }
            item.gameObject.layer = LayerMask.NameToLayer("Item");
            foreach (Transform child in item.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Item");
            }
            item.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.parent = ItemCamera.transform;
            item.GetComponent<Collider>().enabled = false;
            if (item.TransformInPlayerHand)
            {
                item.transform.localPosition = item.GetComponent<Item>().TransformInPlayerHand.localPosition;
                item.transform.localRotation = item.GetComponent<Item>().TransformInPlayerHand.localRotation;
            }
            else Debug.LogError("None transform");     
        }
    }
    public void DropItem()
    {
        if (Item.GetComponent<Item>().HandTransform)
        {
            Item.GetComponent<Item>().HandTransform.SetActive(false);
        }
        DefaultHandTransform.SetActive(true);
        Item.gameObject.layer = LayerMask.NameToLayer("ItemInFloor");
        foreach (Transform child in Item.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        Item.GetComponent<Collider>().enabled = true;
        Item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Item.gameObject.transform.position = new Vector3(Item.gameObject.transform.position.x, gameObject.transform.position.y, Item.gameObject.transform.position.z);
        Item.transform.parent = null;
        Item = null;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Item)
        {
            DropItem();
        }
    }
    private void FixedUpdate()
    {
        Ray ray = ItemCamera.ScreenPointToRay(ScreeCenter);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxDistance, DropItemLayer))
        {
            if (hit.collider.gameObject.TryGetComponent(out Item item) && !Item)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    PickupItem(hit.collider.gameObject.GetComponent<Item>());
                }
            }
            else if (hit.collider.gameObject.TryGetComponent(out NextButton nextButton))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    nextButton.ButtonClick();
                }
            }
        }
    }
}

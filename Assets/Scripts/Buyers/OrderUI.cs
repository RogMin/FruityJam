using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PriceText; //set in inspector
    [SerializeField] private GameObject LinePrefab; //set in inspector (it's object in panel)
    [SerializeField] public GameObject OrderPanel; //set in inspector
    private const float UIspacing = 0.15f;
    private Order order;
    private List<GameObject> Lines = new List<GameObject>();

    private void Start()
    {
        order = GetComponent<Order>();
        PriceText.text = order.GenerateOrder(Random.Range(1,3)) + " $";
        DrawOrder();
        ClosePanel();
    }
    public void OpenPanel()
    {
        OrderPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        OrderPanel.SetActive(false);
    }
    public void UpdateOrderList()
    {
        if(OrderIsReady())
        {
            return;
        }
        ClearOrder();
        DrawOrder();
    }
    private void ClearOrder()
    {
        foreach (var line in Lines)
        {
            Destroy(line);
        }
        Lines = new List<GameObject>();
        OrderIsReady();
    }
    private bool OrderIsReady()
    {
        if(GetComponent<Order>().DishesOrder.Count <= 0)
        {
            OrderReady();
            return true;
        }
        return false;
    }
    private void OrderReady()
    {
        LinePrefab.SetActive(false);
        GetComponent<Buyer>().FreeSpace(true);
    }
    private void DrawOrder()
    {
        Lines = new List<GameObject>();
        var line = LinePrefab.transform.GetChild(0);
        int i = 1;
        foreach (KeyValuePair<Dish,int> keyValue in order.DishesOrder)
        {
            LinePrefab.SetActive(true);
            if(i != 1)
            {
                Vector3 LinePosition = new Vector3(LinePrefab.transform.position.x, LinePrefab.transform.position.y - UIspacing * i, LinePrefab.transform.position.z);
                var newLine = Instantiate(LinePrefab, LinePosition, Quaternion.identity, OrderPanel.transform);
                Lines.Add(newLine);
                line = newLine.transform.GetChild(0);
            }
            line.GetChild(0).GetComponent<RawImage>().texture = keyValue.Key.Image;
            line.GetChild(1).GetComponent<TextMeshProUGUI>().text = keyValue.Key.Name;
            line.GetChild(2).GetComponent<TextMeshProUGUI>().text = keyValue.Value.ToString();
            i++;
        }
    }
}

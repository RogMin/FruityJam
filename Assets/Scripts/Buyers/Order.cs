using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Order : MonoBehaviour
{
    [HideInInspector] public Dictionary<Dish, int> DishesOrder; // Dish and amount
    private List<Dish> dishesPrefabs;
    private int Price; 
    void Start()
    {
        startMethod();
    }
    private void startMethod()
    {
        DishesOrder = new Dictionary<Dish, int>();
        dishesPrefabs = Resources.LoadAll<Dish>("Dishes").ToList();
    }
    private void OnEnable()
    {
        startMethod(); //the order is on each human
    }
    public string GenerateOrder(int quantity)
	{
        for (int i = 0; i < quantity; i++)
		{
            int random = Random.Range(0, dishesPrefabs.Count -1);
            if (DishesOrder.ContainsKey(dishesPrefabs[random]))
            {
                quantity--;
                continue;
            }
            int dishAmount = Random.Range(1, dishesPrefabs[random].MaxOrderQuantity + 1);
            DishesOrder.Add(dishesPrefabs[random], dishAmount);
            Price += dishesPrefabs[random].Price * dishAmount;
            dishesPrefabs.Remove(dishesPrefabs[random]); //это блюдо уже есть, переходим к следующему
            if (dishesPrefabs.Count <= 0) break;
		}
        return Price.ToString();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDropZone : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; //set in inspector
    [SerializeField] private AudioClip foodCompleteSound; //set in inspector
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.TryGetComponent(out Item item) && item.product && Buyers.Instance.BuyersList.Count > 0)
            {
                    foreach (KeyValuePair<Dish, int> dishInList in Buyers.Instance.BuyersList[0].gameObject.GetComponent<Order>().DishesOrder)
                    {
                        if (dishInList.Key == other.gameObject.GetComponent<Item>().product)
                        {
                            if (dishInList.Value > 1) //≈сли заказано больше одного продукта, вычитаем.
                            {
                                Buyers.Instance.BuyersList[0].gameObject.GetComponent<Order>().DishesOrder[dishInList.Key] = dishInList.Value - 1;
                            }
                            else if (dishInList.Value == 1)
                            {
                                Buyers.Instance.BuyersList[0].gameObject.GetComponent<Order>().DishesOrder.Remove(dishInList.Key);
                            }
                            audioSource.PlayOneShot(foodCompleteSound);
                            Buyers.Instance.BuyersList[0].gameObject.GetComponent<OrderUI>().UpdateOrderList();
                            Destroy(item.gameObject);
                        }
                    }                         
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookingUtensils : MonoBehaviour
{
    public typeOfCooking CookingType; //set in inspector
    [SerializeField] private AudioClip cookingSound; //set in inspector
    [SerializeField] private const float ListUpdateTime = 0.015f;
    private List<GameObject> currentGameObjectInside = new List<GameObject>(); 
    private List<ScriptableObject> currentProductInside = new List<ScriptableObject>();
    private List<Recipe> Recipes = new List<Recipe>();
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (var recipe in Resources.LoadAll<Recipe>("Recipes"))
        {
            if (recipe.CookingType == CookingType)
            {
                Recipes.Add(recipe);
            }
        }
        StartCoroutine(UpdateList());
    }
    IEnumerator UpdateList()
    {
        while (true)
        {
            if(currentGameObjectInside.Count > 0 || currentProductInside.Count > 0)
            {
                foreach (var product in currentGameObjectInside)
                {
                   if(product.layer == LayerMask.NameToLayer("Item"))
                    {
                        RemoveProductFromList(product.gameObject);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(ListUpdateTime);
        }
    }
    private void CheckRecipesEqual()
    {
        foreach (var Recipe in Recipes)
        {
            if (currentProductInside.OrderBy(m => m).SequenceEqual(Recipe.Ingredients.OrderBy(m => m))) //если содержание листа соответствует рецепту
            {
                Instantiate(Recipe.Result, gameObject.transform.position, Quaternion.identity);
                audioSource.PlayOneShot(cookingSound);
                foreach (var product in currentGameObjectInside)
                {
                    Destroy(product);
                }
                ReinitializeLists();
                Debug.Log("Рецепт " + Recipe.name);
            }
        }
    }
    private void ReinitializeLists()
    {
        currentGameObjectInside = new List<GameObject>();
        currentProductInside = new List<ScriptableObject>();
    }
    private void AddProductInList(Item item)
    {
        currentGameObjectInside.Add(item.gameObject);
        currentProductInside.Add(item.product);
        CheckRecipesEqual();
    }
    private void RemoveProductFromList(GameObject product)
    {
        currentGameObjectInside.Remove(product);
        currentProductInside.Remove(product.GetComponent<Item>().product);
        CheckRecipesEqual();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Item item) && item.product && other.gameObject.layer == LayerMask.NameToLayer("ItemInFloor") )
        {
            foreach (var itemInside in currentGameObjectInside)
            {
                if (other.gameObject == itemInside.gameObject)
                {
                    return;
                }
            }
            AddProductInList(item);                 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Products") || other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            RemoveProductFromList(other.gameObject);
        }
    }
}

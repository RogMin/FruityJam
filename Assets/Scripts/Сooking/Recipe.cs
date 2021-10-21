using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum typeOfCooking {Boiling, Frying};
[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipes")]
public class Recipe : ScriptableObject
{
    public List<ScriptableObject> Ingredients = new List<ScriptableObject>();
    public GameObject Result;
    public typeOfCooking CookingType;
}

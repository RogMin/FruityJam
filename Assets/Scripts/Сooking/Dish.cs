using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dish", menuName = "ScriptableObjects/Dish")]
public class Dish : Product
{
    public int Price;
    [Tooltip("The maximum number of such dishes per order")] public int MaxOrderQuantity;
    public Texture Image;
}

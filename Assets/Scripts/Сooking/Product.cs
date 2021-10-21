using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Product", menuName ="ScriptableObjects/Product")]
public class Product : ScriptableObject, IComparable
{
    public string Name;
    public int CompareTo(object obj) //Сравнение в скрипте CookingUtensils
    {
       return ((Product)obj).Name == Name ? 0 : 1;
    }
}

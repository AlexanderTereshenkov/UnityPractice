using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipiesDishSO : ScriptableObject
{
    public List<FoodTypeSO> ingredients;
    public GameObject readyDish;
}


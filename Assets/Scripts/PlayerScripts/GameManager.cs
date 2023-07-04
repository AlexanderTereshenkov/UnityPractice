using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float secondsToEnd = 300;
    [SerializeField] private RecipiesDishSO[] recipiesBook;
    private void Update()
    {
        secondsToEnd -= Time.deltaTime;
    }

    public RecipiesDishSO[] GetRecipiesDishes()
    {
        return recipiesBook;
    }
}

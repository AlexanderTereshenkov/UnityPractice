using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float secondsToEnd = 300;
    private List<RecipiesDishSO> dishesInGame = new List<RecipiesDishSO>();
    [SerializeField] private RecipiesDishSO[] recipiesBook;
    private void Start()
    {

        for(int i = 0; i < recipiesBook.Length; i++)
        {
            dishesInGame.Add(recipiesBook[i]);
        }
        Debug.Log(dishesInGame[0].ingredients[0].name);
    }
    private void Update()
    {
        secondsToEnd -= Time.deltaTime;
    }

    public RecipiesDishSO[] GetRecipiesDishes()
    {
        return recipiesBook;
    }
}

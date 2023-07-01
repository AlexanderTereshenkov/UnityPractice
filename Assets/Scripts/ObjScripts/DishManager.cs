using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    [SerializeField] private Transform foodPlace;
    [SerializeField] private Transform plateSpawn;
    [SerializeField] private GameObject readyDish;

    private List<GameObject> ingridiens = new List<GameObject>();
    private RecipiesDishSO[] gameManagerDish;

    private void Start()
    {
        gameManagerDish = GameObject.FindWithTag("GameController").GetComponent<GameManager>().GetRecipiesDishes();
    }

    public void PutFoodInPlate(GameObject playerObject)
    {
        playerObject.transform.SetParent(foodPlace);
        playerObject.SetActive(false);
        ingridiens.Add(playerObject);
        for (int i = 0; i < gameManagerDish.Length; i++)
        {
            bool isDishRight = true;
            if(ingridiens.Count == gameManagerDish[i].ingredients.Count)
            {
                for (int j = 0; j < gameManagerDish[i].ingredients.Count; j++)
                {
                    if (!gameManagerDish[i].ingredients.Contains(ingridiens[j].GetComponent<PickFood>().GetFoodTypeSO()))
                    {
                        isDishRight = false;
                    }
                }
                if (isDishRight)
                {
                    Debug.Log("You made ficking salad");
                }
            }

        }
    }
}

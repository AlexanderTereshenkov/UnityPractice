using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    [SerializeField] private Transform foodPlace;
    [SerializeField] private Transform plateSpawn;
    //[SerializeField] private GameObject readyDish;
   
    private List<GameObject> currentIngrediens = new List<GameObject>();
    private RecipiesDishSO[] recipiesBook;

    private void Start()
    {
        recipiesBook = GameObject.FindWithTag("GameController").GetComponent<GameManager>().GetRecipiesDishes();
    }

    public void PutFoodInPlate(GameObject playerObject)
    {
        playerObject.transform.SetParent(foodPlace);
        playerObject.SetActive(false);
        currentIngrediens.Add(playerObject);
        for (int i = 0; i < recipiesBook.Length; i++)
        {
            bool isDishRight = true;
            if(currentIngrediens.Count == recipiesBook[i].ingredients.Count)
            {
                for (int j = 0; j < recipiesBook[i].ingredients.Count; j++)
                {
                    if (!recipiesBook[i].ingredients.Contains(currentIngrediens[j].GetComponent<PickFood>().GetFoodTypeSO()))
                    {
                        isDishRight = false;
                    }
                }
                if (isDishRight)
                {
                    var readyDish = Instantiate(recipiesBook[i].readyDish, foodPlace.transform);
                    readyDish.transform.SetParent(null);
                    Destroy(this.gameObject);
                    break;
                }
            }

        }
    }
}

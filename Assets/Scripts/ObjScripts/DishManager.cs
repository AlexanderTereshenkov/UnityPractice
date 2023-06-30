using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    [SerializeField] private Transform foodPlace;
    [SerializeField] private Transform plateSpawn;
    [SerializeField] private GameObject readyDish;

    private List<GameObject> ingridiens = new List<GameObject>();

    public void PutFoodInPlate(GameObject playerObject)
    {
        playerObject.transform.SetParent(foodPlace);
        playerObject.SetActive(false);
        ingridiens.Add(playerObject);
        if(ingridiens.Count == 3)
        {
            var readyPlate = Instantiate(readyDish, plateSpawn.transform);
            readyPlate.transform.SetParent(null);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private Inventory inventory;
    private MoneyManager moneyManager;
    private float percentToReturn = 0.3f;

    private void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        moneyManager = GameObject.FindWithTag("Player").GetComponent<MoneyManager>();
    }
    public void DestroyGameObject(GameObject objectToDestroy)
    {
        if(objectToDestroy.TryGetComponent<PickFood>(out PickFood pickFood))
        {
            moneyManager.ChangeMoneyValue((int)Mathf.Floor(pickFood.GetFoodTypeSO().price * percentToReturn));
            DestroyObject(objectToDestroy);
        }
        else if(objectToDestroy.TryGetComponent<PickObject>(out PickObject pickObject))
        {
            if(pickObject.GetObjectName() != "Knife" && pickObject.GetObjectName() != "Lopatka")
            {
                if(pickObject.GetComponent<DishManager>() != null)
                {
                    moneyManager.ChangeMoneyValue((int)Mathf.Floor(pickObject.GetComponent<DishManager>().GetCurrentPrice() * percentToReturn));
                }
                DestroyObject(objectToDestroy);
            }
        }
    }

    private void DestroyObject(GameObject gameObject)
    {
        inventory.GetAllObjects()[inventory.GetActiveSlot()].transform.SetParent(null);
        inventory.RemoveFromInventory(inventory.GetActiveSlot());
        Destroy(gameObject);
    }

}

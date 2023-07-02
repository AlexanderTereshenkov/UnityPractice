using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }
    public void DestroyGameObject(GameObject objectToDestroy)
    {
        if(objectToDestroy.GetComponent<PickFood>() != null || objectToDestroy.GetComponent<PickObject>() != null)
        {
            if(objectToDestroy.GetComponent<PickObject>() != null)
            {
                if(objectToDestroy.GetComponent<PickObject>().GetObjectName() != "Knife" && objectToDestroy.GetComponent<PickObject>().GetObjectName() != "Lopatka")
                {
                    inventory.GetAllObjects()[inventory.GetActiveSlot()].transform.SetParent(null);
                    inventory.RemoveFromInventory(inventory.GetActiveSlot());
                    Destroy(objectToDestroy);
                }
            }
            else
            {
                inventory.GetAllObjects()[inventory.GetActiveSlot()].transform.SetParent(null);
                inventory.RemoveFromInventory(inventory.GetActiveSlot());
                Destroy(objectToDestroy);
            }

        }

    }

}

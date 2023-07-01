using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInteraction : MonoBehaviour, IInteractible
{
    [SerializeField] private GameObject boardFoodPlace;
    [SerializeField] private RecipeSO[] recipies;
    private GameObject playerFoodObject;
    private bool isBoardFull;
    private bool isObjectBusy;


    public void SpawnFoodObject()
    {

    }

    public void MakeAction(string objName)
    {
        if(objName == "Knife")
        {
            for (int i = 0; i < recipies.Length; i++)
            {
                if (recipies[i].input == playerFoodObject.GetComponent<PickFood>().GetFoodTypeSO())
                {
                    Instantiate(recipies[i].output.prefab, boardFoodPlace.transform);
                    Destroy(playerFoodObject);
                    StopAction();
                }
            }
        }
    }

    public void StartAction(GameObject gameObject)
    {
        isBoardFull = true;
        playerFoodObject = gameObject;
        playerFoodObject.GetComponent<PickFood>().SetCurrentInteractibleObject(this.gameObject);
        playerFoodObject.transform.SetParent(boardFoodPlace.transform);
        playerFoodObject.transform.position = boardFoodPlace.transform.position;
        playerFoodObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void StopAction()
    {
        isBoardFull = false;
        playerFoodObject.GetComponent<PickFood>().SetCurrentInteractibleObject(null);
        playerFoodObject = null;
    }

    public bool IsObjectBusy()
    {
        return isObjectBusy;
    }

    public bool IsObjectFull()
    {
        return isBoardFull;
    }

}

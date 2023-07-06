using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotInteraction : MonoBehaviour, IInteractible
{
    [SerializeField] private Transform foodPlace;
    [SerializeField] private RecipeSO[] recipiesBook;
    [SerializeField] private Image progressBar;
    [SerializeField] private Color uncookedPBColor;
    [SerializeField] private Color almostCookedPBColor;
    [SerializeField] private Color cookedPBColor;

    private bool isObjectBusy;
    private bool isObjectFull;
    private GameObject playerFoodObject;
    private float cookingTime = 20;
    private float countTime;

    private void Update()
    {
        if (isObjectBusy)
        {
            countTime += Time.deltaTime;
            progressBar.fillAmount = countTime / cookingTime;
            if (countTime / cookingTime >= 0.8f)
            {
                progressBar.color = cookedPBColor;
            }
            else if (countTime / cookingTime < 0.8f && countTime / cookingTime >= 0.5f)
            {
                progressBar.color = almostCookedPBColor;
            }
            else
            {
                progressBar.color = uncookedPBColor;
            }
            if (countTime >= cookingTime)
            {
                SpawnFoodObject();
            }
        }
    }

    public bool IsObjectBusy()
    {
        return isObjectBusy;
    }

    public bool IsObjectFull()
    {
        return isObjectFull;
    }

    public bool IsPossibleToInteract(GameObject gameObject)
    {
        return true;
    }

    public void MakeAction(string objName)
    {
        
    }

    public void SpawnFoodObject()
    {
        GameObject cookedDish = null;
        for (int i = 0; i < recipiesBook.Length; i++)
        {
            if (playerFoodObject.GetComponent<PickFood>().GetFoodTypeSO() == recipiesBook[i].input)
            {

                cookedDish = recipiesBook[i].output.prefab;
                Destroy(playerFoodObject);
                playerFoodObject = Instantiate(cookedDish, foodPlace.transform);
                playerFoodObject.GetComponent<PickFood>().SetCurrentInteractibleObject(this.gameObject);
                playerFoodObject.transform.SetParent(foodPlace.transform);
                playerFoodObject.transform.position = foodPlace.transform.position;
                playerFoodObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        isObjectBusy = false;
    }

    public void StartAction(GameObject gameObject)
    {
        isObjectFull = true;
        isObjectBusy = true;
        playerFoodObject = gameObject;
        playerFoodObject.GetComponent<PickFood>().SetCurrentInteractibleObject(this.gameObject);
        playerFoodObject.transform.SetParent(foodPlace.transform);
        playerFoodObject.transform.position = foodPlace.transform.position;
        playerFoodObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void StopAction()
    {
        isObjectFull = false;
        isObjectBusy = false;
        progressBar.fillAmount = 0f;
        countTime = 0;
        playerFoodObject.GetComponent<PickFood>().SetCurrentInteractibleObject(null);
        playerFoodObject = null;
    }

}

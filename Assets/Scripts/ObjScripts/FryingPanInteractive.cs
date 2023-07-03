using UnityEngine;
using UnityEngine.UI;

public class FryingPanInteractive : MonoBehaviour, IInteractible
{
    [SerializeField] private GameObject foodPlace;
    [SerializeField] private RecipeSO[] recipies;
    [SerializeField] private Image progressBar;
    [SerializeField] private Color uncookedPBColor;
    [SerializeField] private Color almostCookedPBColor;
    [SerializeField] private Color cookedPBColor;
    private float fryingTime = 30f;
    private float countTime;
    private GameObject playerFoodObject;
    private bool isBoardFull;
    private bool isObjectBusy;
    public void SpawnFoodObject()
    {

        GameObject cookedDish = null;
        for (int i = 0; i < recipies.Length; i++)
        {
            if (playerFoodObject.GetComponent<PickFood>().GetFoodTypeSO() == recipies[i].input)
            {

                cookedDish = recipies[i].output.prefab;
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

    private void Update()
    {
        if (isObjectBusy)
        {
            countTime += Time.deltaTime;
            progressBar.fillAmount = countTime / fryingTime;
            if(countTime / fryingTime >= 0.8f)
            {
                progressBar.color = cookedPBColor;
            }
            else if(countTime / fryingTime < 0.8f && countTime / fryingTime >= 0.5f)
            {
                progressBar.color = almostCookedPBColor;
            }
            else
            {
                progressBar.color = uncookedPBColor;
            }
            Debug.Log(countTime);
            if(countTime >= fryingTime)
            {
                SpawnFoodObject();
            }
        }
    }

    public void StartAction(GameObject gameObject)
    {
            isBoardFull = true;
            isObjectBusy = true;
            playerFoodObject = gameObject;
            playerFoodObject.GetComponent<PickFood>().SetCurrentInteractibleObject(this.gameObject);
            playerFoodObject.transform.SetParent(foodPlace.transform);
            playerFoodObject.transform.position = foodPlace.transform.position;
            playerFoodObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void StopAction()
    {
        isBoardFull = false;
        isObjectBusy = false;
        progressBar.fillAmount = 0f;
        countTime = 0;
        playerFoodObject.GetComponent<PickFood>().SetCurrentInteractibleObject(null);
        playerFoodObject = null;
    }

    public void MakeAction(string objName)
    {

    }

    public bool IsObjectBusy()
    {
        return isObjectBusy;
    }

    public bool IsObjectFull()
    {
        return isBoardFull;
    }

    public bool IsPossibleToInteract(GameObject gameObject)
    {
        bool isPossibleToInteract = true;
        for (int i = 0; i < recipies.Length; i++)
        {
            if (recipies[i].input != gameObject.GetComponent<PickFood>().GetFoodTypeSO()) isPossibleToInteract = false;
        }
        return isPossibleToInteract;
    }

}

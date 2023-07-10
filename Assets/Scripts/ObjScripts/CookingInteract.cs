using UnityEngine;
using UnityEngine.UI;

public class CookingInteract : MonoBehaviour, IInteractible
{
    public enum CookwareType
    {
        Pot, FryingPen
    }

    [SerializeField] private GameDataSO gameData;
    [SerializeField] private Transform foodPlace;
    [SerializeField] private GameObject coalPrefab;
    [SerializeField] private GameObject lid;
    [SerializeField] private RecipeSO[] recipies;
    [SerializeField] private Image progressBar;
    [SerializeField] private Color uncookedPBColor;
    [SerializeField] private Color almostCookedPBColor;
    [SerializeField] private Color cookedPBColor;
    [SerializeField] private CookwareType cookwareType;

    private float cookingTime;
    private float countTime;
    private float commonCookingTime;
    private GameObject playerFoodObject;
    private bool isBoardFull;
    private bool isObjectBusy;
    private bool isCoalSpawned;

    private void Start()
    {
        switch (cookwareType)
        {
            case CookwareType.Pot:
                cookingTime = gameData.boilingTime;
                break;
            case CookwareType.FryingPen:
                cookingTime = gameData.fryingTime;
                break;
        }
    }

    public void ReplaceGameObject(GameObject newObject)
    {
        Destroy(playerFoodObject);
        playerFoodObject = Instantiate(newObject, foodPlace);
        if (playerFoodObject.TryGetComponent<PickFood>(out PickFood pickFood))
        {
            pickFood.SetCurrentInteractibleObject(this.gameObject);
        }
        playerFoodObject.transform.SetParent(foodPlace);
        playerFoodObject.transform.position = foodPlace.position;
        playerFoodObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public void SpawnFoodObject()
    {
        lid.SetActive(false);
        GameObject cookedDish = null;
        for (int i = 0; i < recipies.Length; i++)
        {
            if (playerFoodObject.GetComponent<PickFood>().GetFoodTypeSO() == recipies[i].input)
            {

                cookedDish = recipies[i].output.prefab;
                ReplaceGameObject(cookedDish);
            }
        }
        isObjectBusy = false;

    }

    public void SpawnCoalEnstedOfFood()
    {
        ReplaceGameObject(coalPrefab);
    }

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
        if (isBoardFull)
        {
            commonCookingTime += Time.deltaTime;
            if(commonCookingTime >= 1.5f * cookingTime && !isCoalSpawned)
            {
                isCoalSpawned = true;
                SpawnCoalEnstedOfFood();
            }
        }
    }

    public void StartAction(GameObject gameObject)
    {
        isBoardFull = true;
        isObjectBusy = true;
        playerFoodObject = gameObject;
        playerFoodObject.GetComponent<PickFood>().SetCurrentInteractibleObject(this.gameObject);
        playerFoodObject.SetActive(false);
        lid.SetActive(true);
        /*
        playerFoodObject.transform.SetParent(foodPlace);
        playerFoodObject.transform.position = foodPlace.position;
        playerFoodObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        */
    }

    public void StopAction()
    {
        isBoardFull = false;
        isObjectBusy = false;
        isCoalSpawned = false;
        progressBar.fillAmount = 0f;
        countTime = 0;
        commonCookingTime = 0;
        if (playerFoodObject.TryGetComponent<PickFood>(out PickFood pickFood))
        {
            pickFood.SetCurrentInteractibleObject(null);
        }
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
        bool isPossibleToInteract = false;
        for (int i = 0; i < recipies.Length; i++)
        {
            if (recipies[i].input == gameObject.GetComponent<PickFood>().GetFoodTypeSO()) isPossibleToInteract = true;
        }
        return isPossibleToInteract;
    }

}

using UnityEngine;

public class FryingPanInteractive : MonoBehaviour, IInteractible
{
    [SerializeField] private GameObject foodPlace;
    private float fryingTime = 15f;
    private float countTime;
    private GameObject playerFoodObject;
    private bool isBoardFull;
    private bool isObjectBusy;
    public void SpawnFoodObject()
    {

    }

    private void Update()
    {
        if (isObjectBusy)
        {
            countTime += Time.deltaTime;
            Debug.Log(countTime);
            if(countTime >= fryingTime)
            {
                SpawnFoodObject();
                StopAction();
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

}

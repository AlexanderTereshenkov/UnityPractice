using UnityEngine;

public class FridjeInteraction : MonoBehaviour
{
    [SerializeField] private GameObject buyingMenu;
    [SerializeField] private Transform foodSpawnPoint;
    [SerializeField] private FoodTypeSO[] foodTypes;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void SpawnFoodObject(int pos)
    {
        Instantiate(foodTypes[pos].prefab, foodSpawnPoint);
    }

    public void StartAction()
    {
        buyingMenu.SetActive(true);
        player.GetComponent<PlayerController>().SetIsMovingPossible(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StopAction()
    {
        buyingMenu.SetActive(false);
        player.GetComponent<PlayerController>().SetIsMovingPossible(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

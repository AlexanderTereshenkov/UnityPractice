using UnityEngine;

public class GiveOrder : MonoBehaviour
{
    private float previousTime = 0;
    private float orderCoolDown = 3;
    private RecipiesDishSO[] recipiesBook;
    private GameManager gameManager;
    [SerializeField] private GameObject checkPrefab;
    [SerializeField] private Transform orderPlace;
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        recipiesBook = gameManager.GetRecipiesDishes();
    }

    public void GiveOrderToPlayer(float time)
    {
        if(time - previousTime >= orderCoolDown)
        {
            string currentOrder = recipiesBook[Random.Range(0, recipiesBook.Length)].readyDish.GetComponent<PickObject>().GetObjectName();
            var check = Instantiate(checkPrefab, orderPlace);
            check.GetComponent<Order>().SetCurrentRecipie(currentOrder);
            gameManager.SetActiveCheques(1);
            previousTime = time;
        }
    }
}

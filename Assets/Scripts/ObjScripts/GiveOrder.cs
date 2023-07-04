using UnityEngine;

public class GiveOrder : MonoBehaviour
{
    private float previousTime = 0;
    private float orderCoolDown = 3;
    private RecipiesDishSO[] recipiesBook;
    [SerializeField] private GameObject saladPrefab;
    [SerializeField] private GameObject pastaPrefab;
    [SerializeField] private Transform orderPlace;
    private void Start()
    {
        recipiesBook = GameObject.FindWithTag("GameController").GetComponent<GameManager>().GetRecipiesDishes();
    }

    public void GiveOrderToPlayer(float time)
    {
        if(time - previousTime >= orderCoolDown)
        {
            string currentOrder = recipiesBook[Random.Range(0, recipiesBook.Length)].readyDish.GetComponent<PickObject>().GetObjectName();
            if (currentOrder == "Pasta")
            {
                GameObject currObj = Instantiate(pastaPrefab, orderPlace);
                currObj.GetComponent<Order>().SetCurrentRecipie(currentOrder);
            }
            else
            {
                GameObject currObj = Instantiate(saladPrefab, orderPlace);
                currObj.GetComponent<Order>().SetCurrentRecipie(currentOrder);
            }
            previousTime = time;
        }
    }
}

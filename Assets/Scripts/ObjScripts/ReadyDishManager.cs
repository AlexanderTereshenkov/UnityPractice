using UnityEngine;

public class ReadyDishManager : MonoBehaviour
{
    [SerializeField] private int priceForDish;

    public int GetPrice()
    {
        return priceForDish;
    }
}

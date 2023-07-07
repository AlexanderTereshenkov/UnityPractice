using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    private int money;
    private int previousAmount;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameDataSO gameDataSO;


    void Start()
    {
        money = gameDataSO.money;
        previousAmount = money;
        moneyText.text = money.ToString();
    }

    public void ChangeMoneyValue(int value)
    {
        money += value;
        moneyText.text = money.ToString();
    }
    

    public int GetPreviousAmount()
    {
        return previousAmount;
    }

    public int GetMoney()
    {
        return money;
    }

}

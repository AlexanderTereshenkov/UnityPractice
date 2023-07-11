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
        money = Mathf.Clamp(money, -9999, 9999);
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

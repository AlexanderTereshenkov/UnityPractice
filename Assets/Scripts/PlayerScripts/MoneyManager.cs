using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    private int money = 150;
    [SerializeField] TextMeshProUGUI moneyText;


    void Start()
    {
        moneyText.text = money.ToString();
    }

<<<<<<< HEAD
    public void ChangeMoneyValue(int value)
    {
        money += value;
        moneyText.text = money.ToString();
    }

=======
>>>>>>> 966b6339140bef42075a9b1dcceaf8d52d6c1439
}

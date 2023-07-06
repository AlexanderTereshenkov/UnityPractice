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

}

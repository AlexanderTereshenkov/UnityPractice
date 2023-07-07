using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int cheque = 0;
    private int allCheques;
    private MoneyManager moneyManager;
    [SerializeField] private RecipiesDishSO[] recipiesBook;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float secondsToEnd = 180;
    [SerializeField] private GameDataSO gamaData;
    [SerializeField] private Color redTextColor;

    private void Start()
    {
        moneyManager = GameObject.FindWithTag("Player").GetComponent<MoneyManager>();
    }
    private void Update()
    {
        if (secondsToEnd < 0)
        {
            gamaData.money = moneyManager.GetMoney();
            gamaData.uncookedCheques = cheque;
            gamaData.cookedCheques = allCheques - cheque;
            gamaData.previousAmount = moneyManager.GetPreviousAmount();
            SceneManager.LoadScene(1);
        }
        if(secondsToEnd > 0 && secondsToEnd < 15)
        {
            timer.color = redTextColor;
        }
        secondsToEnd -= Time.deltaTime;
        timer.text = (int)secondsToEnd / 60 + ":" + ((int)secondsToEnd % 60 < 10 ? "0" + (int)secondsToEnd % 60 : (int)secondsToEnd % 60);
    }

    public RecipiesDishSO[] GetRecipiesDishes()
    {
        return recipiesBook;
    }

    public void SetActiveCheques(int value)
    {
        cheque += value;
        allCheques++;
        Debug.Log(cheque);
    }
}

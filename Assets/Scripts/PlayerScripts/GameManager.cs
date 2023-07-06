using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float secondsToEnd = 120;
    private int cheque = 0;
    [SerializeField] private RecipiesDishSO[] recipiesBook;
    [SerializeField] private TextMeshProUGUI timer;
    private void Update()
    {
        if (secondsToEnd < 0) return;
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
        Debug.Log(cheque);
    }
}

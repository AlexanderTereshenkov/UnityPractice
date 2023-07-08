using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameResults : MonoBehaviour
{
    [SerializeField] private GameObject[] hideObj;
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private GameDataUI gameDataUI;

    public void ShowResults()
    {
        for(int i = 0; i < hideObj.Length; i++)
        {
            hideObj[i].SetActive(false);
        }
        resultsScreen.SetActive(true);
        gameDataUI.SetResultsUI();
    }
}

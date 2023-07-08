using UnityEngine;
using TMPro;

public class GameDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cookedText;
    [SerializeField] private TextMeshProUGUI uncookedText;
    [SerializeField] private TextMeshProUGUI fineText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private PlayerController playerController;


    public void SetResultsUI()
    {
        playerController.SetIsMovingPossible(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cookedText.text = "�������� �������: " + gameData.cookedCheques.ToString();
        uncookedText.text = "������������� �������: " + gameData.uncookedCheques.ToString();
        fineText.text = "�����: " + (gameData.uncookedCheques * 20).ToString();
        gameData.money -= gameData.uncookedCheques * 20;
        incomeText.text = "������� �� ����: " + (gameData.money - gameData.previousAmount).ToString();

        gameData.cookedCheques = 0;
        gameData.uncookedCheques = 0;
        gameData.previousAmount = 0;
    }

}

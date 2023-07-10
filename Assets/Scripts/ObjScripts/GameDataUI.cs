using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cookedText;
    [SerializeField] private TextMeshProUGUI uncookedText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private PlayerController playerController;


    public void SetResultsUI()
    {
        playerController.SetIsMovingPossible(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cookedText.text = "Успешных заказов: " + gameData.cookedCheques.ToString();
        uncookedText.text = "Невыполненных заказов: " + gameData.uncookedCheques.ToString() + " (" + "штраф: " + (gameData.uncookedCheques * 20).ToString() + ")";
        gameData.money -= gameData.uncookedCheques * 20;
        incomeText.text = "Прибыль за день: " + (gameData.money - gameData.previousAmount).ToString();

        gameData.cookedCheques = 0;
        gameData.uncookedCheques = 0;
        gameData.previousAmount = 0;
    }

    public void LoadMainMune()
    {
        SceneManager.LoadScene(0);
    }

    public void UpgradesScreen()
    {
        SceneManager.LoadScene(2);
    }

}

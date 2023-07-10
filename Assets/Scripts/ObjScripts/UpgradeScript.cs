using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeScript : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private TextMeshProUGUI[] btnText;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText.text = gameData.money.ToString();
        btnText[0].text = "Время жарки: " + gameData.fryingTime + "с." + " Уровень: " + (gameData.fryingTimeLevel == 11 ? "МАКС" : gameData.fryingTimeLevel) + "\n" + "След. уровень " + gameData.fryingTimeLevel * 100;
        btnText[1].text = "Время варки: " + gameData.boilingTime + "с." + " Уровень: " + (gameData.boilingTimeLevel == 11 ? "МАКС" : gameData.boilingTimeLevel) + "\n" + "След. уровень " + gameData.boilingTimeLevel * 100;
    }

    public void UpdateCookingTime(int buttonPosition)
    {
        int money = gameData.money;
        switch (buttonPosition)
        {
            case 0:
                int upgradeCost = gameData.fryingTimeLevel * 100;
                if (money - upgradeCost >= 0 && gameData.fryingTimeLevel < 11)
                {
                    money -= upgradeCost;
                    gameData.fryingTimeLevel += 1;
                    gameData.fryingTime -= 1;
                }
                gameData.money = money;
                moneyText.text = gameData.money.ToString();
                btnText[0].text = "Время жарки: " + gameData.fryingTime + "с." + " Уровень: " + (gameData.fryingTimeLevel == 11 ? "МАКС" : gameData.fryingTimeLevel) + "\n" + "След. уровень " + gameData.fryingTimeLevel * 100;
                break;
            case 1:
                int upgradeCostB = gameData.boilingTimeLevel * 100;
                if (money - upgradeCostB >= 0 && gameData.boilingTimeLevel < 11)
                {
                    money -= upgradeCostB;
                    gameData.boilingTimeLevel += 1;
                    gameData.boilingTime -= 1;
                }
                gameData.money = money;
                moneyText.text = gameData.money.ToString();
                btnText[1].text = "Время варки: " + gameData.boilingTime + "с." + " Уровень: " + (gameData.boilingTimeLevel == 11 ? "МАКС" : gameData.boilingTimeLevel) + "\n" + "След. уровень " + gameData.boilingTimeLevel * 100;
                break;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

}

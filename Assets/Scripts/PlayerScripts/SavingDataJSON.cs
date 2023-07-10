using System.IO;
using UnityEngine;

public class SavingDataJSON : MonoBehaviour
{
    private string filePath = Application.persistentDataPath + "/" + "playerData.json";

    public void SaveData(PlayerData playerData)
    {
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, data);
    }

    public PlayerData LoadData()
    {
        return JsonUtility.FromJson<PlayerData>(File.ReadAllText(filePath));
    }
}

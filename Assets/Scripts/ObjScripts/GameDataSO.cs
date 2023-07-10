using UnityEngine;

[CreateAssetMenu()]
public class GameDataSO : ScriptableObject
{
    public int money;
    public int previousAmount;
    public int uncookedCheques;
    public int cookedCheques;

    public int fryingTime;
    public int boilingTime;
    public int fryingTimeLevel;
    public int boilingTimeLevel;

}

using UnityEngine;
using TMPro;

public class Order : MonoBehaviour
{
    private string currentRecipie;
    [SerializeField] private TextMeshProUGUI text;

    public void SetCurrentRecipie(string recipiesDish)
    {
        currentRecipie = recipiesDish;
        text.text = currentRecipie;
    }

    public string GetCurrentRecipie()
    {
        return currentRecipie;
    }

}

using UnityEngine;

public class Order : MonoBehaviour
{
    private string currentRecipie;

    public void SetCurrentRecipie(string recipiesDish)
    {
        currentRecipie = recipiesDish;
    }

    public string GetCurrentRecipie()
    {
        return currentRecipie;
    }

}

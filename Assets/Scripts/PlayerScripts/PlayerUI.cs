using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Text screenText;
    public void SetScreenText(string text)
    {
        screenText.text = text;
    }
}

using UnityEngine;
using TMPro;

public class MoneyCount : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string newText)
    {
        textComponent.text = newText;
    }
}
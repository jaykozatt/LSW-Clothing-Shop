using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyGUI : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    
    private void Awake() {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerInventory.Instance.OnMoneyChanged += UpdateDisplay;
    }

    // Update is called once per frame
    void UpdateDisplay(int amount)
    {
        textMesh.text = $"{amount}";
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlotGUI : MonoBehaviour 
{
    private Equipable _item;

    private Image placeholder;
    private Image icon;

    private void Awake() {
        placeholder = GetComponentsInChildren<Image>()[1];
        icon = GetComponentsInChildren<Image>()[2];
    }

    private void Start() {
        UpdateDisplay();
    }

    private void UpdateDisplay() 
    {
        if (_item != null)
        {
            placeholder.enabled = false;
            icon.enabled = true;
            icon.sprite = _item.icon;
            icon.color = _item.color;
        }
        else
        {
            placeholder.enabled = true;
            icon.enabled = false;
        }
    }

    public void SetItem(Equipable item) 
    {
        _item = item;
        UpdateDisplay();
    }

    public void ClearItem()
    {
        _item = null;
        UpdateDisplay();
    }

    public void SelectItemFromShop()
    {
        int index = transform.GetSiblingIndex()-1;
        // TODO: Send index to ShopManager
    }

    public void SelectItemFromInventory()
    {
        int index = transform.GetSiblingIndex();
        // TODO: Send index to InventoryManagerGUI
    }
}
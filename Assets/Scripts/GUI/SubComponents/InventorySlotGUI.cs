using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotGUI : MonoBehaviour 
{
    private Equipable _item;

    // private Image background;
    private Image icon;
    private TextMeshProUGUI price;

    private void Awake() {
        // background = GetComponentsInChildren<Image>()[0];
        icon = GetComponentsInChildren<Image>()[1];
        price = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start() {
        UpdateDisplay();
    }

    private void UpdateDisplay() 
    {
        if (_item != null)
        {
            icon.enabled = true;
            icon.sprite = _item.icon;
            icon.color = _item.color;
            price.text = _item.price.ToString();
            
            price.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            icon.enabled = false;
            price.transform.parent.gameObject.SetActive(false);
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
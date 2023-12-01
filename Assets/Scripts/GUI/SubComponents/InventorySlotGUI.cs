using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotGUI : MonoBehaviour 
{
    private Equipable _item;

    // private Image background;
    private Image icon;
    private TextMeshProUGUI price;


    private void Start() {
        Init();
        UpdateDisplay();
    }

    private void LateUpdate() {
        if (_item != null && ShopGUI.Instance.IsActive)
            price.transform.parent.gameObject.SetActive(true);
        else
            price.transform.parent.gameObject.SetActive(false);
    }

    public void Init()
    {
        // background = GetComponentsInChildren<Image>()[0];
        icon = GetComponentsInChildren<Image>()[1];
        price = GetComponentInChildren<TextMeshProUGUI>();
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

    public void EquipItem()
    {
        if (!ShopGUI.Instance.IsActive && _item != null)
        {
            int index = transform.GetSiblingIndex();
            PlayerInventory.Instance.EquipItemByIndex(index);
        }
    }

    public void BuyItem()
    {
        if (ShopGUI.Instance.IsActive && _item != null)
        {
            int index = transform.GetSiblingIndex();
            Shopkeeper.Instance.SellItemByIndex(index-1); // Minus one, to account for the non-active template object
        }
    }

    public void SellItem()
    {
        if (ShopGUI.Instance.IsActive && _item != null)
        {
            int index = transform.GetSiblingIndex();
            PlayerInventory.Instance.SellItemByIndex(index);
        }
    }
}
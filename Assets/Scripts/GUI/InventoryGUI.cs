using System.Collections.Generic;
using UnityEngine;
using KozUtils;

public class InventoryGUI : StaticInstance<InventoryGUI> 
{   
    [Header("Equipment Slots")]
    [SerializeField] EquipmentSlotGUI _upperSlot;
    [SerializeField] EquipmentSlotGUI _lowerSlot;
    [SerializeField] EquipmentSlotGUI _footwearSlot;

    InventorySlotGUI[] _slots;

    protected override void Awake() 
    {
        base.Awake();
        _slots = GetComponentsInChildren<InventorySlotGUI>();    
    }

    private void Start() 
    {
        PlayerInventory.Instance.OnInventoryChanged += UpdateSlots;
        PlayerInventory.Instance.OnEquipmentChanged += UpdateEquipment;

        CloseInterface();
    }

    private void UpdateEquipment(EquipmentSlot slot, Equipable item)
    {
        switch (slot)
        {
            case EquipmentSlot.Upper:
                _upperSlot.SetItem(item);
                break;
            case EquipmentSlot.Lower:
                _lowerSlot.SetItem(item);
                break;
            case EquipmentSlot.Footwear:
                _footwearSlot.SetItem(item);
                break;
            default: break;
        }
    }

    private void UpdateSlots(List<Equipable> inventory)
    {
        for (int i=0; i < inventory.Count; i++)
        {
            _slots[i].SetItem(inventory[i]);
        }
    }

    public void OpenInterface()
    {
        transform.parent.parent.gameObject.SetActive(true);
    }

    public void CloseInterface()
    {
        transform.parent.parent.gameObject.SetActive(false);
        ShopGUI.Instance.CloseInterface();
    }
}
using System.Collections;
using System.Collections.Generic;
using KozUtils;
using UnityEngine;

public class PlayerInventory : StaticInstance<PlayerInventory> 
{
    public struct Equipment 
    {
        public Equipable upper;
        public Equipable lower;
        public Equipable footwear;
    }
    [SerializeField] int startingMoney;
    public int capacity;
    public List<Equipable> inventory;
    private int _currentMoney = 0;
    private Equipment _currentEquipment;
    public Equipment CurrentEquipment {get => _currentEquipment;}

    public event System.Action<int> OnMoneyChanged;
    public event System.Action<List<Equipable>> OnInventoryChanged;
    public event System.Action<EquipmentSlot, Equipable> OnEquipmentChanged;

    private void Start() {
        _currentMoney = startingMoney;
    }

    public void EquipItem(int index)
    {
        // Get the item out of the inventory
        Equipable item = inventory[index];
        inventory.Remove(item);

        // Set the item as currently equipped
        item.currentlyEquipped = true;

        // Equip the item, or swap it if there's already an item on the slot
        Equipable previousItem = null;
        switch (item.slot)
        {
            case EquipmentSlot.Upper:
                if (_currentEquipment.upper != null) 
                    previousItem = _currentEquipment.upper;
                _currentEquipment.upper = item;
                break;
            case EquipmentSlot.Lower:
                if (_currentEquipment.lower != null) 
                    previousItem = _currentEquipment.lower;
                _currentEquipment.lower = item;
                break;
            case EquipmentSlot.Footwear:
                if (_currentEquipment.footwear != null) 
                    previousItem = _currentEquipment.footwear;
                _currentEquipment.footwear = item;
                break;
            default:
                throw new System.Exception("Unexpected attempt to equip an item with unknown item slot.");
        }

        if (previousItem != null)
            inventory.Add(previousItem);

        OnEquipmentChanged?.Invoke(item.slot, item);
        OnInventoryChanged?.Invoke(inventory);
    }

    public void UnequipItem(EquipmentSlot slot)
    {
        Equipable item;
        switch (slot)
        {
            case EquipmentSlot.Upper:
                item = _currentEquipment.upper;
                _currentEquipment.upper = null;
                OnEquipmentChanged?.Invoke(EquipmentSlot.Upper, null);
                break;
            case EquipmentSlot.Lower:
                item = _currentEquipment.lower;
                _currentEquipment.lower = null;
                OnEquipmentChanged?.Invoke(EquipmentSlot.Lower, null);
                break;
            case EquipmentSlot.Footwear:
                item = _currentEquipment.footwear;
                _currentEquipment.footwear = null;
                OnEquipmentChanged?.Invoke(EquipmentSlot.Footwear, null);
                break;
            default:
                throw new System.Exception("Cannot unequip item from an invalid equipment slot.");
        }

        item.currentlyEquipped = false;
        inventory.Add(item);

        OnInventoryChanged?.Invoke(inventory);
    }

    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        OnMoneyChanged?.Invoke(_currentMoney);
    }

    public bool TrySpendMoney(int amount)
    {
        if (_currentMoney >= amount)
        {
            _currentMoney -= amount;
            OnMoneyChanged?.Invoke(_currentMoney);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryBuyItem(Equipable item)
    {
        if (inventory.Count < capacity && TrySpendMoney(item.price))
        {
            inventory.Add(item);

            OnInventoryChanged?.Invoke(inventory);
            return true;
        }
        else
        {
            return false;
        }
    }

    public Equipable SellItem(int index)
    {
        Equipable item = inventory[index];
        
        inventory.Remove(item);
        AddMoney(item.price);

        return item;
    }

}
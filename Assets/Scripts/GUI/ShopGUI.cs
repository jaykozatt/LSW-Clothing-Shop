using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KozUtils;

public class ShopGUI : StaticInstance<ShopGUI>
{
    InventorySlotGUI _template;
    List<InventorySlotGUI> _slots;
    public bool IsInitialized {get; private set;}

    public bool IsActive {
        get => gameObject.activeInHierarchy;
    }

    public void Init()
    {
        _template = GetComponentInChildren<InventorySlotGUI>();
        _template.gameObject.SetActive(false);
        _slots = new List<InventorySlotGUI>();

        Shopkeeper.Instance.OnStockChanged += UpdateSlots;
        IsInitialized = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!IsInitialized) Init();
    }

    private void UpdateSlots(List<Equipable> inventory)
    {
        Debug.Log($"Inventory Count: {inventory.Count}, Slots Count: {_slots.Count}");

        for (int i=0; i < Mathf.Max(inventory.Count,_slots.Count); i++)
        {
            if (i >= _slots.Count) 
            {
                InventorySlotGUI slot = Instantiate(_template, _template.transform.parent);
                slot.Init();
                slot.gameObject.SetActive(true);

                _slots.Add(slot);
            }

            if (i < inventory.Count)
                _slots[i].SetItem(inventory[i]);
            else
            {
                GameObject slot = _slots[i].gameObject;
                _slots.RemoveAt(i);
                Destroy(slot);
            }
        }
    }

    public void OpenInterface()
    {
        gameObject.SetActive(true);
        InventoryGUI.Instance.OpenInterface();
    }

    public void CloseInterface()
    {
        gameObject.SetActive(false);
    }
}

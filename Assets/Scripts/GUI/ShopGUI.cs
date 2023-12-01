using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KozUtils;

public class ShopGUI : StaticInstance<ShopGUI>
{
    InventorySlotGUI _template;
    List<InventorySlotGUI> _slots;

    // Start is called before the first frame update
    void Start()
    {
        _template = GetComponentInChildren<InventorySlotGUI>();
        _template.gameObject.SetActive(false);
        _slots = new List<InventorySlotGUI>();

        Shopkeeper.Instance.OnNewStock += UpdateSlots;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateSlots(List<Equipable> inventory)
    {
        for (int i=0; i < inventory.Count; i++)
        {
            if (i >= _slots.Count) 
            {
                InventorySlotGUI slot = Instantiate(_template, _template.transform.parent);
                slot.Init();
                slot.gameObject.SetActive(true);
                
                _slots.Add(slot);
            }

            _slots[i].SetItem(inventory[i]);
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

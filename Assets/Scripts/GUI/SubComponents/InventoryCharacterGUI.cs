using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCharacterGUI : MonoBehaviour
{
    public Image upperSlot;
    public Image lowerSlot;
    public Image footwearSlot;

    // Update is called once per frame
    void LateUpdate()
    {
        if (PlayerInventory.Instance.CurrentEquipment.upper != null)
        {
            upperSlot.enabled = true;
            upperSlot.sprite = PlayerInventory.Instance.CurrentEquipment.upper.animationSheets.idle.down[0];
            upperSlot.color = PlayerInventory.Instance.CurrentEquipment.upper.color;
        }
        else
        {
            upperSlot.enabled = false;
        }

        if (PlayerInventory.Instance.CurrentEquipment.lower != null)
        {
            lowerSlot.enabled = true;
            lowerSlot.sprite = PlayerInventory.Instance.CurrentEquipment.lower.animationSheets.idle.down[0];
            lowerSlot.color = PlayerInventory.Instance.CurrentEquipment.lower.color;
        }
        else
        {
            lowerSlot.enabled = false;
        }

        if (PlayerInventory.Instance.CurrentEquipment.footwear != null)
        {
            footwearSlot.enabled = true;
            footwearSlot.sprite = PlayerInventory.Instance.CurrentEquipment.footwear.animationSheets.idle.down[0];
            footwearSlot.color = PlayerInventory.Instance.CurrentEquipment.footwear.color;
        }
        else
        {
            footwearSlot.enabled = false;
        }
    }
}

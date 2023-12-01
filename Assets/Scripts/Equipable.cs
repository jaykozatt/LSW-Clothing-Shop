using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot {
    Upper, Lower, Footwear
}

[CreateAssetMenu(fileName = "New Equipable", menuName = "Equipable Item", order = 0)]
public class Equipable : ScriptableObject 
{
    internal bool currentlyEquipped = false;
    public int price;
    public Color color;
    public Sprite icon;
    public EquipmentSlot slot;
    public AnimationChart animationSheets;

    public void SetRandomColor()
    {
        color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }
}
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteSheet
{
    public List<Sprite> up;
    public List<Sprite> down;
    public List<Sprite> left;
    public List<Sprite> right;

    public SpriteSheet()
    {
        up = new List<Sprite>();
        down = new List<Sprite>();
        left = new List<Sprite>();
        right = new List<Sprite>();
    }

    public void Clear()
    {
        up.Clear();
        down.Clear();
        left.Clear();
        right.Clear();
    }

    public int Count {
        get {
            if (up.Count == down.Count && up.Count == left.Count && up.Count == right.Count)
                return up.Count;
            else
                throw new System.Exception("There's a mismatch in the frame count of at least one of this spritesheet's directions");
        }
    }

    public int Capacity {
        get {
            if (up.Capacity == down.Capacity && up.Capacity == left.Capacity && up.Capacity == right.Capacity)
                return up.Capacity;
            else
                throw new System.Exception("There's a mismatch in the frame count of at least one of this spritesheet's directions");
        }
    }
}


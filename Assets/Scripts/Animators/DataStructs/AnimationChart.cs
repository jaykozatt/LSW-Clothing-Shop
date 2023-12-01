using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AnimationChart", menuName = "Animation Chart", order = 0)]
public class AnimationChart : ScriptableObject
{
    [Serializable]
    public struct SourceSheet
    {
        public int framesPerDirection;
        public List<Sprite> source;
    }
    [SerializeField] SourceSheet sourceIdle;
    [SerializeField] SourceSheet sourceWalking;

    internal SpriteSheet idle;
    internal SpriteSheet walking;

    // I'm using this method to do some quick and dirty auto-sorting in the Editor
    private void OnValidate() 
    {

        idle ??= new SpriteSheet();
        walking ??= new SpriteSheet();
        
        Sort();
    }

    void Sort()
    {
        idle.Clear();
        if (sourceIdle.source != null && sourceIdle.framesPerDirection != 0 && sourceIdle.source.Count % sourceIdle.framesPerDirection == 0)
            for (int i=0; i < sourceIdle.framesPerDirection; i++)
            {
                idle.up.Add(sourceIdle.source[i]);
                idle.left.Add(sourceIdle.source[i+sourceIdle.framesPerDirection]);
                idle.down.Add(sourceIdle.source[i+sourceIdle.framesPerDirection*2]);
                idle.right.Add(sourceIdle.source[i+sourceIdle.framesPerDirection*3]);
            }
        else if (sourceIdle.source != null && sourceIdle.framesPerDirection != 0 && sourceIdle.source.Count % sourceIdle.framesPerDirection != 0)
            Debug.LogWarning("Idle animation couldn't be sorted");

        walking.Clear();
        if (sourceWalking.source != null && sourceWalking.framesPerDirection != 0 && sourceWalking.source.Count % sourceWalking.framesPerDirection == 0)
            for (int i=0; i < sourceWalking.framesPerDirection; i++)
            {
                walking.up.Add(sourceWalking.source[i]);
                walking.left.Add(sourceWalking.source[i+sourceWalking.framesPerDirection]);
                walking.down.Add(sourceWalking.source[i+sourceWalking.framesPerDirection*2]);
                walking.right.Add(sourceWalking.source[i+sourceWalking.framesPerDirection*3]);
            }
        else if (sourceWalking.source != null && sourceWalking.framesPerDirection != 0 && sourceWalking.source.Count % sourceWalking.framesPerDirection != 0)
            Debug.LogWarning("Walking animation couldn't be sorted");
    }
}

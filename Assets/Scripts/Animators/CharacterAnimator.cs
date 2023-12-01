using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
public class CharacterAnimator : MonoBehaviour
{
    #region Data Structures
        public enum AnimationState
        {
            Idle, Walking
        }
    #endregion

    #region Variables & Switches
        [Tooltip("Lower is faster. Higher is slower")]
        public int animationRate = 1;
        private int _currentFrame = 0;
        private AnimationState _currentState = AnimationState.Idle;
        private Vector2 _lastDirection;
        private SpriteSheet _currentHead;
        private SpriteSheet _currentBody;
        private SpriteSheet _currentHair;
        private SpriteSheet _currentUpper;
        private SpriteSheet _currentLower;
        private SpriteSheet _currentShoes;
    #endregion

    #region References
        [Header("References")]
        [SerializeField] SpriteRenderer _head;
        [SerializeField] SpriteRenderer _body;

        [SerializeField] SpriteRenderer _hair;
        [SerializeField] SpriteRenderer _upper;
        [SerializeField] SpriteRenderer _lower;
        [SerializeField] SpriteRenderer _shoes;
    #endregion

    #region Animation Slots
        [Header("Animation Slots")]
        [SerializeField] AnimationChart headBase;
        [SerializeField] AnimationChart bodyBase;
        [SerializeField] AnimationChart hair;
        [SerializeField] AnimationChart upperClothes;
        [SerializeField] AnimationChart lowerClothes;
        [SerializeField] AnimationChart shoes;
    #endregion


    private void Awake() {
        DOTween.Init();
        _lastDirection = Vector2.down;
    }

    private void FixedUpdate() 
    {
        ProgressFrame();
    }

    public void LookTowards(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        UpdateSprite(direction);
    }
    public void SetAnimationState(AnimationState state)
    {
        _currentState = state;
        UpdateAnimationsSheets();        
    }
    private void UpdateAnimationsSheets()
    {
        switch (_currentState)
        {
            case AnimationState.Idle:
                _currentHead = headBase.idle;
                _currentBody = bodyBase.idle;
                _currentHair = hair.idle;
                _currentUpper = upperClothes?.idle;
                _currentLower = lowerClothes?.idle;
                _currentShoes = shoes?.idle;
                break;
            case AnimationState.Walking:
                _currentHead = headBase.walking;
                _currentBody = bodyBase.walking;
                _currentHair = hair.walking;
                _currentUpper = upperClothes?.walking;
                _currentLower = lowerClothes?.walking;
                _currentShoes = shoes?.walking;
                break;
            default: break;
        }
    }
    public void UpdateSprite(Vector2 direction)
    {
        // If vector magnitude is zero, use the previous direction instead
        // else, cache the current direction for future frames
        if (direction.sqrMagnitude == 0) 
        {
            direction = _lastDirection;
            SetAnimationState(AnimationState.Idle);
        }
        else 
        {
            _lastDirection = direction;
            SetAnimationState(AnimationState.Walking);
        }
        
        // Translate current frame value to the current frame range.
        _currentFrame = _currentFrame % (_currentBody.Count * animationRate);
        
        // Translate the currentFrame to its actual frame range value.
        int animationFrame = _currentFrame / animationRate; 

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0)
            {
                _head.sprite = _currentHead.up[animationFrame];
                _body.sprite = _currentBody.up[animationFrame];
                _hair.sprite = _currentHair.up[animationFrame];
                _upper.sprite = _currentUpper?.up[animationFrame];
                _lower.sprite = _currentLower?.up[animationFrame];
                _shoes.sprite = _currentShoes?.up[animationFrame];
            }
            else
            {
                _head.sprite = _currentHead.down[animationFrame];
                _body.sprite = _currentBody.down[animationFrame];
                _hair.sprite = _currentHair.down[animationFrame];
                _upper.sprite = _currentUpper?.down[animationFrame];
                _lower.sprite = _currentLower?.down[animationFrame];
                _shoes.sprite = _currentShoes?.down[animationFrame];
            }
        }
        else
        {
            if (direction.x > 0)
            {
                _head.sprite = _currentHead.right[animationFrame];
                _body.sprite = _currentBody.right[animationFrame];
                _hair.sprite = _currentHair.right[animationFrame];
                _upper.sprite = _currentUpper?.right[animationFrame];
                _lower.sprite = _currentLower?.right[animationFrame];
                _shoes.sprite = _currentShoes?.right[animationFrame];
            }
            else
            {
                _head.sprite = _currentHead.left[animationFrame];
                _body.sprite = _currentBody.left[animationFrame];
                _hair.sprite = _currentHair.left[animationFrame];
                _upper.sprite = _currentUpper?.left[animationFrame];
                _lower.sprite = _currentLower?.left[animationFrame];
                _shoes.sprite = _currentShoes?.left[animationFrame];
            }
        }

    }

    void ProgressFrame() 
    {
        // Do not advance to the next frame unless there's a body animation
        if (_currentBody != null)
            _currentFrame = (_currentFrame+1) % (_currentBody.Count * animationRate);
    }

}
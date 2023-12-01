using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KozUtils;
using System.Threading.Tasks;

public class PlayerController : StaticInstance<PlayerController>
{
    #region Data Structures
        public enum State 
        {
            Playing,
            Paused
        }
        
    #endregion

    #region Settings
        [SerializeField] float speed = 10;
    #endregion

    #region Variables & Switches
        bool _hasInteractedRecently = false;
        State _currentState = State.Playing;
    #endregion

    #region References
        Rigidbody2D rb;
        Vector2 input;
        CharacterAnimator animator;
    #endregion

    public bool HasInteractedRecently { get => _hasInteractedRecently; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        ExecuteMovement();
    }

    void ProcessInput() 
    {
        if (_currentState == State.Playing)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
        }
        else 
        {
            input = Vector2.zero;
        }
        
        animator.UpdateSprite(input);

        if (Input.GetButtonDown("Interact") && !_hasInteractedRecently)
        {
            _hasInteractedRecently = true;
            BufferInteraction(delayMiliseconds: 100);
        }

        // if (Input.GetKeyDown(KeyCode.Escape))
        //     switch (_currentState)
        //     {
        //         case State.Playing:
        //             // Puase game
        //             break;
        //         case State.Paused:
        //             // Return control
        //             break;
        //         default: break;
        //     }
    }

    void ExecuteMovement()
    {
        rb.velocity = input * speed;
    }

    async void BufferInteraction(int delayMiliseconds=0)
    {
        await Task.Delay(delayMiliseconds);
        _hasInteractedRecently = false;
    }
}

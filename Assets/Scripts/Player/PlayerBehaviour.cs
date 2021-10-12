using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private InputControls input;

    private Vector2 move;
    private Vector2 aim;
    private bool aiming;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;

    private float angle;

    private void Awake() {
        input = new InputControls();

        input.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => move = Vector2.zero;
        input.Player.StartAim.performed += ctx => aiming = true;
        input.Player.StartAim.canceled += ctx => aiming = false;
        input.Player.Aim.performed += ctx => aim = ctx.ReadValue<Vector2>();
        input.Player.Aim.canceled += ctx => aim = Vector2.zero;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        rb.velocity = move * speed;
    }

    private void Aim() {
        Vector2 lookdir;
        if (!aiming) {
            if(move != Vector2.zero) {
                angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
                aim = move;
            }
        }
    }
    private void OnEnable() {
        input.Enable();
    }
    private void OnDisable() {
        input.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private InputControls input;

    [SerializeField]
    private PlayerInput pl;

    [SerializeField]
    private PlayerStats stats;

    private Vector2 move;
    private Vector2 aim;
    private bool aiming;

    [SerializeField]
    private GameObject aimindicator;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    private float aimdec;

    [SerializeField]
    private Transform firepoint;

    private float angle;

    

    private void Awake() {
        input = new InputControls();

        input.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => move = Vector2.zero;
        input.Player.StartAim.performed += ctx => aiming = true;
        input.Player.StartAim.canceled += ctx => aiming = false;
        input.Player.Aim.performed += ctx => aim = ctx.ReadValue<Vector2>();
       // input.Player.Aim.canceled += ctx => aim = Vector2.zero;

    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }
    private void FixedUpdate() {
        rb.velocity = move * stats.speed * aimdec;
    }

    private void Aim() {
        Vector2 lookdir;

        if (!aiming) {
            if (aimindicator.activeSelf) {
                aimindicator.SetActive(false);
            }
            aimdec = 1;
            if (move != Vector2.zero) {
                angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg - 90;
                aim = move;
            }
        } else {
            if (!aimindicator.activeSelf) {
                aimindicator.SetActive(true);
            }
            aimdec = 0.1f;
            //if(aim != Vector2.zero) {
                if(pl.currentControlScheme == "Gamepad") {
                    angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 90;
                } else {
                    Mouse mb = InputSystem.GetDevice<Mouse>();
                    lookdir = Camera.main.ScreenToWorldPoint(mb.position.ReadValue()) - firepoint.transform.position;
                    Debug.Log(lookdir);
                    angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90;
                }
            //}
        }

        firepoint.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void OnEnable() {
        input.Enable();
    }
    private void OnDisable() {
        input.Disable();
    }
}

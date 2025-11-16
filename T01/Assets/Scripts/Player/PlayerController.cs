using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayInputControl inputControl;

    private Rigidbody2D rb;
    
    public Vector2 inputDirection;

    public float speed;

    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayInputControl();

        // += 事件注册，把这个函数方法添加到按键按下的那一刻来执行
        inputControl.Gameplay.Jump.started += Jump;
        print("yubo");
        
    }



    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //任务翻转
        int facedir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            facedir = 1;
        if (inputDirection.x < 0)
            facedir = -1;
        transform.localScale = new Vector3(facedir, 1, 1);
    }

        private void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("JUMP");
    }
    

}

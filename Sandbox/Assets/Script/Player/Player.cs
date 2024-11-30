using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    PlayerStatus myStatus;
    Vector2 inputVec;
    float sprint;
    public bool isRoll;
    public bool isMove;
    [Header("# Player Info")]
    int currentHp;

    private StaminaController staminaController;
        
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        staminaController = GetComponent<StaminaController>();
    }
    void Start()
    {
        myStatus = StatusManager.Instance.myStatus;
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (isRoll) return;
        Vector2 next = (myStatus.mSpeed + sprint) * Time.fixedDeltaTime * inputVec;
        rigid.MovePosition(rigid.position + next);
        rigid.velocity = Vector2.zero;
    }
    void LateUpdate()
    {
        if(isRoll) return;
        anim.SetFloat("Speed", inputVec.magnitude);


        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
    public void SetSprint(float value)
    {
        sprint = value;
    }
    public void PlayRoll(Vector2 target)
    {
        Debug.Log(target.x + " " + target.y);
        isRoll = true;
        var dir = target - rigid.position;
        spriter.flipX = dir.x < 0;
        rigid.AddForce(1.5f * myStatus.mSpeed * dir.normalized, ForceMode2D.Impulse);
    }
    public void StopRoll()
    {
        rigid.velocity = Vector2.zero;
        isRoll = false;
    }
    public void StopMove()
    {
        isMove = false;
    }
    void OnMove(InputValue value)
    {
        isMove = true;
        inputVec = value.Get<Vector2>();
    }
    
}

﻿using UnityEngine;                  
using UnityEngine.Events;           
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 欄位
    // 成員：欄位、屬性、方法、事件
    // 修飾詞 類型 名稱 指定 值；
    [Header("移動速度"), Range(0, 1000)]
    public int speed = 50;                  // 整數
    [Header("跳躍高度"), Range(0, 2000)]
    public float jump = 2.5f;               // 浮點數
    [Header("血量"), Range(0, 200)]
    public float hp = 100;
    [Header("血量吧條")]
    public Image hpBar;
    [Header("結束畫面")]
    public GameObject final;
    [Header("音效區域")]

    [Header("吃東西事件")]
    public UnityEvent onEat;

    //public string foxName = "狐狸";         // 字串
    //public bool pass = false;               // 布林值 - true/false

    //private Transform tra;
    private Rigidbody2D r2d;
    public Transform tran;
    private float hpMax;
    public bool isGround;
    #endregion

    #region 事件
    // 事件：在特定時間點會以指定頻率執行的方法
    // 開始事件：遊戲開始時執行一次
    private void Start()
    {
        // 泛型 <T>
        //tra = GetComponent<Transform>();
        r2d = GetComponent<Rigidbody2D>();
        tran = GetComponent<Transform>();

        hpMax = hp;
    }

    // 更新事件：每秒執行約 60 次
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn(0);
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
        if ( tran.position.y <= -6)
        {
            Dead();
        }
    }

    // 固定更新事件：每禎 0.002 秒
    private void FixedUpdate()
    {
        Walk(); // 呼叫方法
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            isGround = true;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "icon")
        {
            
            Destroy(collision.gameObject);  // 刪除
            onEat.Invoke();                 // 呼叫事件
        }

        if (collision.name == "死亡區域") Dead();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 走路
    /// </summary>
    private void Walk()
    {
        
            r2d.AddForce(new Vector2(speed * Input.GetAxisRaw("Horizontal"), 0));
        
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
            
        }
    }

    // 參數語法：類型 名稱
    /// <summary>
    /// 轉彎
    /// </summary>
    /// <param name="direction">方向，左轉為 180，右轉為 0</param>
    private void Turn(int direction = 0)
    {
        transform.eulerAngles = new Vector3(0, direction, 0);
    }

    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;

        if (hp <= 0) Dead();
    }

    private void Dead()
    {
        final.SetActive(true);
    }
    #endregion
}

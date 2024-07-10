using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbody;
    private float axisH = 0.0f;
    public float speed = 5.0f;
    public float jumpPw = 5.0f;
    private bool goJump = false;
    public LayerMask groundLayer;
    private SpriteRenderer spriteRenderer;
    private MusicController musicController;

    private bool isBattle;
    private bool isBattleRireki;

    void Start()
    {
        isBattle = false;
        isBattleRireki = isBattle;
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        musicController = Camera.main.GetComponent<MusicController>();

        // musicControllerが正しく取得できているか確認
        if (musicController == null)
        {
            Debug.LogError("MusicControllerがメインカメラにアタッチされていません。");
        }
    }

    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        Move();

        if (Input.GetButton("Jump"))
        {
            Jump();
        }

        // 前回の状況と異なっているかつバトル時
        if (isBattle && isBattleRireki != isBattle)
        {
            // 音楽の音量切り替え
            musicController.BattleBgmControl();
        }
        // 前回の状況と異なっているかつバトルじゃないとき
        else if (!isBattle && isBattleRireki != isBattle)
        {
            // 音楽の音量切り替え
            musicController.NormalBgmControl();
        }
        isBattleRireki = isBattle;
    }

    private void FixedUpdate()
    {
        if (goJump)
        {
            Vector2 jumpVec = new Vector2(0, jumpPw);
            rbody.velocity = new Vector2(rbody.velocity.x, 0);
            rbody.AddForce(jumpVec, ForceMode2D.Impulse);
            goJump = false;
        }
    }

private void OnTriggerEnter2D(Collider2D other)
{
    if (other.tag == "BattleLine")
    {
        isBattle = true;
        Debug.Log("Entered Battle: isBattle=" + isBattle);
    }
}

private void OnTriggerExit2D(Collider2D other)
{
    if (other.tag == "BattleLine")
    {
        isBattle = false;
        Debug.Log("Exited Battle: isBattle=" + isBattle);
    }
}

    bool getIsBattle()
    {
        return isBattle;
    }

    public bool onGroundCheck(GameObject obj)
    {
        return Physics2D.CircleCast(transform.position, 0.2f, Vector2.down, 0.0f, groundLayer);
    }

    public void Move()
    {
        if (axisH > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (axisH < 0)
        {
            spriteRenderer.flipX = false;
        }

        if (onGroundCheck(gameObject) || axisH != 0)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
    }

    public void Jump()
    {
        if (onGroundCheck(gameObject))
        {
            goJump = true;
        }
    }
}
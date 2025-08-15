using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //대미지======================
    public int HP = 100;
    public void TakeDamage(int damage)
    {
        HP -= damage;
        DamageUI.Instance.ShowDamage(transform.position, damage);
        if (HP <= 0)
        {
            Spawner.Instance.OnDead(this);
            Destroy(gameObject);
        }
    }

    //움직임========================
    [SerializeField] private TriggerEvent upTrigger;
    [SerializeField] private TriggerEvent downTrigger;
    [SerializeField] private TriggerEvent leftTrigger;
    [SerializeField] private TriggerEvent rightTrigger;

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float jumpAddForce;
    [SerializeField] private float moveLeftForce;
    [SerializeField] private float moveRightForce;

    [SerializeField] private Vector2 velocityX;
    [SerializeField] private Vector2 velocityY;

    private bool isJumping = false;
    private bool isBacking = false;

    private void FixedUpdate()
    {
        //점프===============================
        if (!isJumping && LeftZombie() && RightNoZombie())
        {
            isJumping = true;
            rb2d.velocity = Vector2.zero;
        }

        if (isJumping)
        {
            if (LeftZombie() && !UpZombie() && RightNoZombie())
            {
                rb2d.AddForce(Vector2.up * jumpAddForce, ForceMode2D.Impulse);
            }
            else
            {
                SetLimitVelocity(0.3f * velocityX, 0.3f * velocityY);
                isJumping = false;
            }
        }


        //좌우===============================
        if (DownGround() && UpZombie())
        {
            isBacking = true;
            rb2d.velocity = Vector2.zero;
        }

        if (isBacking)
        {
            if (UpZombie())
            {
                MoveRight();
            }
            else
            {
                SetLimitVelocity(0.1f * velocityX, 0.1f * velocityY);
                isBacking = false;
            }
        }

        else
        {
            MoveLeft();
        }


        //조정===============================
        SetLimitVelocity(velocityX, velocityY);
    }

    void MoveLeft()
    {
        rb2d.AddForce(Vector2.left * moveLeftForce);
    }

    void MoveRight()
    {
        rb2d.AddForce(Vector2.right * moveRightForce);
    }

    bool LeftZombie()
    {
        return leftTrigger.col.Count > 0;
    }

    bool UpZombie()
    {
        return upTrigger.col.Count > 0;
    }

    bool DownGround()
    {
        return downTrigger.col.Count > 0;
    }

    bool RightNoZombie()
    {
        return rightTrigger.col.Count == 0;
    }

    void SetLimitVelocity(Vector2 minmaxX, Vector2 minmaxY)
    {
        var temp = rb2d.velocity;
        temp.x = Mathf.Clamp(temp.x, minmaxX.x, minmaxX.y);
        temp.y = Mathf.Clamp(temp.y, minmaxY.x, minmaxY.y);
        rb2d.velocity = temp;
    }
}
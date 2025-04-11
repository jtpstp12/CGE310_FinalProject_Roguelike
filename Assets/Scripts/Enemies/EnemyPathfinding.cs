using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();

        // ตรวจสอบการตั้งค่าของ Rigidbody2D
        if (rb.isKinematic)
        {
            Debug.LogError("Rigidbody2D is set to Kinematic, which may prevent movement.");
        }
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack)
        {
            // หยุดเคลื่อนที่ระหว่างการผลักจาก Knockback
            return;
        }

        // เคลื่อนที่ตาม moveDir
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        // พลิกการหมุน sprite ให้เหมาะสมกับทิศทางการเคลื่อนที่
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Debug: ตรวจสอบทิศทางการเคลื่อนที่
        Debug.Log("moveDir: " + moveDir);
    }

    // คำนวณทิศทางไปยังตำแหน่ง targetPosition
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = (targetPosition - (Vector2)transform.position).normalized;

        // Debug: ตรวจสอบค่าของ moveDir ที่คำนวณได้
        Debug.Log("Move To: " + targetPosition + " | moveDir: " + moveDir);
    }

    // หยุดการเคลื่อนที่
    public void StopMoving()
    {
        moveDir = Vector2.zero;
    }
}

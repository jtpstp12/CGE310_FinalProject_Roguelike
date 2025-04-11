using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่
    [SerializeField] private float attackRange = 1.5f; // ระยะโจมตี
    [SerializeField] private float attackCooldown = 1f; // คูลดาวน์ในการโจมตี
    private bool canAttack = true;

    private Rigidbody2D rb;
    private Transform playerTransform; // ตัวแปรเพื่อเก็บตำแหน่งของผู้เล่น
    private Knockback knockback; // ตัวแปรอ้างอิงไปยังสคริปต์ Knockback

    private void Awake()
    {
        // ค้นหาผู้เล่นโดยใช้ tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform; // ตั้งค่าตำแหน่งของผู้เล่น
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene.");
        }

        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>(); // อ้างอิงไปยังสคริปต์ Knockback
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            MoveTowardsPlayer();
            CheckAttack();
        }
    }

    private void MoveTowardsPlayer()
    {
        // ตรวจสอบว่าไม่ได้กำลังโดน Knockback
        if (knockback.GettingKnockedBack)
        {
            return; // ถ้าโดน Knockback ไม่ให้เคลื่อนที่
        }

        // คำนวณทิศทางไปยังผู้เล่น
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // เคลื่อนที่ไปตามทิศทางที่คำนวณ
        rb.velocity = direction * moveSpeed;

        // พลิกการหมุน sprite ให้เหมาะสมกับทิศทางที่เดิน
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // พลิกไปทางซ้าย
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // พลิกไปทางขวา
        }
    }

    private void CheckAttack()
    {
        // ถ้าศัตรูอยู่ในระยะโจมตีและสามารถโจมตีได้
        if (Vector2.Distance(transform.position, playerTransform.position) < attackRange && canAttack)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;

        // ทำการโจมตี (คุณสามารถใส่ฟังก์ชันการโจมตีที่นี่)
        // ตัวอย่าง: playerHealth.TakeDamage(damageAmount);

        // เริ่มคูลดาวน์การโจมตี
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}

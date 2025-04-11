using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 1.5f;   // ใช้สำหรับการพุ่งชน
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private int damage = 2; // Damage ของ Boss

    private bool canDash = true;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private Knockback knockback;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }  // เพิ่ม Accessor สำหรับ AttackRange
    public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }  // เพิ่ม Accessor สำหรับ AttackCooldown
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public float DashCooldown { get => dashCooldown; set => dashCooldown = value; }

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene.");
        }

        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // คำนวณระยะห่างจากผู้เล่น
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > attackRange)
            {
                // เมื่ออยู่ห่างจากผู้เล่นเกินระยะโจมตี ให้เคลื่อนที่ไปหาผู้เล่น
                MoveTowardsPlayer();
            }
            else
            {
                // เมื่อเข้าใกล้ผู้เล่น จะพุ่งเข้าหาผู้เล่นแทน
                DashAttack();
            }

            // เช็คการโจมตีปกติ
            CheckAttack();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (knockback.GettingKnockedBack)
        {
            return;
        }

        // คำนวณทิศทางไปยังผู้เล่น
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // พลิกการหมุน sprite ให้เหมาะสมกับทิศทางที่เดิน
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void CheckAttack()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < attackRange && canDash)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        canDash = false;
        // ทำการโจมตี (ลดเลือดผู้เล่น)
        PlayerHealth.Instance.TakeDamage(damage, transform);  // เรียกฟังก์ชัน TakeDamage จาก PlayerHealth เพื่อทำการลด HP ของผู้เล่น
        StartCoroutine(DashCooldownRoutine());
    }

    private IEnumerator DashCooldownRoutine()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void DashAttack()
    {
        // เมื่อบอสเข้าใกล้ผู้เล่นในระยะโจมตี ให้พุ่งไปชน
        if (Vector2.Distance(transform.position, playerTransform.position) < attackRange && canDash)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * dashSpeed;
            canDash = false;
            // เมื่อพุ่งไปชนผู้เล่นก็ทำดาเมจ
            StartCoroutine(DashCooldownRoutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ทำการโจมตีเมื่อพุ่งชนผู้เล่น
            PlayerHealth.Instance.TakeDamage(damage, transform);
        }
    }

    // ฟังก์ชันสำหรับให้บอสแข็งแกร่งขึ้นเมื่อเกิดใหม่
    public void IncreaseStats()
    {
        moveSpeed += 0.5f;           // เพิ่มความเร็ว
        attackCooldown -= 0.1f;      // ลดคูลดาวน์การโจมตี
        dashCooldown -= 0.5f;        // ลดคูลดาวน์การพุ่ง
        attackRange += 0.5f;         // เพิ่มระยะโจมตี
    }
}

using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 1.5f;   // ������Ѻ��þ�觪�
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private int damage = 2; // Damage �ͧ Boss

    private bool canDash = true;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private Knockback knockback;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }  // ���� Accessor ����Ѻ AttackRange
    public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }  // ���� Accessor ����Ѻ AttackCooldown
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
            // �ӹǳ������ҧ�ҡ������
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > attackRange)
            {
                // �����������ҧ�ҡ�������Թ�������� �������͹�����Ҽ�����
                MoveTowardsPlayer();
            }
            else
            {
                // ���������������� �о������Ҽ�����᷹
                DashAttack();
            }

            // �礡�����ջ���
            CheckAttack();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (knockback.GettingKnockedBack)
        {
            return;
        }

        // �ӹǳ��ȷҧ��ѧ������
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // ��ԡ�����ع sprite �����������Ѻ��ȷҧ����Թ
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
        // �ӡ������ (Ŵ���ʹ������)
        PlayerHealth.Instance.TakeDamage(damage, transform);  // ���¡�ѧ��ѹ TakeDamage �ҡ PlayerHealth ���ͷӡ��Ŵ HP �ͧ������
        StartCoroutine(DashCooldownRoutine());
    }

    private IEnumerator DashCooldownRoutine()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void DashAttack()
    {
        // ����ͺ���������������������� �����仪�
        if (Vector2.Distance(transform.position, playerTransform.position) < attackRange && canDash)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * dashSpeed;
            canDash = false;
            // ����;��仪������蹡�Ӵ����
            StartCoroutine(DashCooldownRoutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �ӡ����������;�觪�������
            PlayerHealth.Instance.TakeDamage(damage, transform);
        }
    }

    // �ѧ��ѹ����Ѻ���������觢��������Դ����
    public void IncreaseStats()
    {
        moveSpeed += 0.5f;           // ������������
        attackCooldown -= 0.1f;      // Ŵ��Ŵ�ǹ�������
        dashCooldown -= 0.5f;        // Ŵ��Ŵ�ǹ��þ��
        attackRange += 0.5f;         // ������������
    }
}

using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 2f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1f;
    private bool canAttack = true;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private Knockback knockback;

    private float moveSpeed;
    private int level = 1;

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

    private void Start()
    {
        moveSpeed = baseMoveSpeed + level * 0.2f;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        moveSpeed = baseMoveSpeed + level * 0.2f;
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
        if (knockback != null && knockback.GettingKnockedBack)
            return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

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
        if (Vector2.Distance(transform.position, playerTransform.position) < attackRange && canAttack)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;
        // TODO: ãÊè¿Ñ§¡ìªÑ¹â¨ÁµÕ¼ÙéàÅè¹ àªè¹ playerHealth.TakeDamage(damageAmount);
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}

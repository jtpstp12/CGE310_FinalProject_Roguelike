using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // ��������㹡������͹���
    [SerializeField] private float attackRange = 1.5f; // ��������
    [SerializeField] private float attackCooldown = 1f; // ��Ŵ�ǹ�㹡������
    private bool canAttack = true;

    private Rigidbody2D rb;
    private Transform playerTransform; // ����������纵��˹觢ͧ������
    private Knockback knockback; // �������ҧ�ԧ��ѧʤ�Ի�� Knockback

    private void Awake()
    {
        // ���Ҽ��������� tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform; // ��駤�ҵ��˹觢ͧ������
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene.");
        }

        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>(); // ��ҧ�ԧ��ѧʤ�Ի�� Knockback
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
        // ��Ǩ�ͺ����������ѧⴹ Knockback
        if (knockback.GettingKnockedBack)
        {
            return; // ���ⴹ Knockback ����������͹���
        }

        // �ӹǳ��ȷҧ��ѧ������
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // ����͹���仵����ȷҧ���ӹǳ
        rb.velocity = direction * moveSpeed;

        // ��ԡ�����ع sprite �����������Ѻ��ȷҧ����Թ
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // ��ԡ价ҧ����
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // ��ԡ价ҧ���
        }
    }

    private void CheckAttack()
    {
        // ����ѵ����������������������ö������
        if (Vector2.Distance(transform.position, playerTransform.position) < attackRange && canAttack)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;

        // �ӡ������ (�س����ö���ѧ��ѹ������շ����)
        // ������ҧ: playerHealth.TakeDamage(damageAmount);

        // �������Ŵ�ǹ�������
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}

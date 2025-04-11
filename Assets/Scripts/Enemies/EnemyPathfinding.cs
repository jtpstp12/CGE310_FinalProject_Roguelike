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

        // ��Ǩ�ͺ��õ�駤�Ңͧ Rigidbody2D
        if (rb.isKinematic)
        {
            Debug.LogError("Rigidbody2D is set to Kinematic, which may prevent movement.");
        }
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack)
        {
            // ��ش����͹��������ҧ��ü�ѡ�ҡ Knockback
            return;
        }

        // ����͹����� moveDir
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        // ��ԡ�����ع sprite �����������Ѻ��ȷҧ�������͹���
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Debug: ��Ǩ�ͺ��ȷҧ�������͹���
        Debug.Log("moveDir: " + moveDir);
    }

    // �ӹǳ��ȷҧ��ѧ���˹� targetPosition
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = (targetPosition - (Vector2)transform.position).normalized;

        // Debug: ��Ǩ�ͺ��Ңͧ moveDir ���ӹǳ��
        Debug.Log("Move To: " + targetPosition + " | moveDir: " + moveDir);
    }

    // ��ش�������͹���
    public void StopMoving()
    {
        moveDir = Vector2.zero;
    }
}

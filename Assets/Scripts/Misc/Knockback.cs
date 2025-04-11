using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true;

        // หยุดการเคลื่อนที่ก่อน
        rb.velocity = Vector2.zero;

        // คำนวณทิศทางที่โดนผลัก
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;

        // เพิ่มแรงผลัก
        rb.AddForce(difference, ForceMode2D.Impulse);

        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero; // หยุดการเคลื่อนที่หลังจากหมดเวลา
        GettingKnockedBack = false; // ทำให้สามารถเคลื่อนที่ได้อีกครั้ง
    }
}

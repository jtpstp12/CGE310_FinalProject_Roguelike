using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    private float spawnInterval = 120f; // ทุก 2 นาที
    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBoss();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);

        // เพิ่มความเร็ว และความแรงให้กับ Boss
        BossAI bossAI = boss.GetComponent<BossAI>();
        bossAI.MoveSpeed += 0.5f; // เพิ่มความเร็วในการเคลื่อนที่
        bossAI.AttackCooldown -= 0.1f; // ลดคูลดาวน์การโจมตี
        bossAI.DashCooldown -= 0.5f; // ลดคูลดาวน์การพุ่ง

        // เพิ่มระยะโจมตีให้กับ Boss
        bossAI.AttackRange += 0.5f;
    }
}

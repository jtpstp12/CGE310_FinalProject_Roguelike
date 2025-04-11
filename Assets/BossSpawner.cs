using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    private float spawnInterval = 120f; // �ء 2 �ҷ�
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

        // ������������ ��Ф����ç���Ѻ Boss
        BossAI bossAI = boss.GetComponent<BossAI>();
        bossAI.MoveSpeed += 0.5f; // ������������㹡������͹���
        bossAI.AttackCooldown -= 0.1f; // Ŵ��Ŵ�ǹ�������
        bossAI.DashCooldown -= 0.5f; // Ŵ��Ŵ�ǹ��þ��

        // ���������������Ѻ Boss
        bossAI.AttackRange += 0.5f;
    }
}

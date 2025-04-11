using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;  // ��ҧ�ԧ��ѧ Prefab �ͧ�ѵ��
    [SerializeField] private float spawnRadius = 5f;  // ����շ���ѵ�٨��Դ�ͺ � ������
    [SerializeField] private float initialSpawnInterval = 3f;  // ���������ҧ����Դ�ѵ���������
    private float spawnInterval;  // ���������Ѻ������㹡���Դ�ѵ��
    private float timePassed = 0f;  // �����������

    [SerializeField] private Transform player;  // ���������Ѻ��ҧ�ԧ��ѧ������

    private void Start()
    {
        spawnInterval = initialSpawnInterval;  // ��駤�����������ҧ����Դ�ѵ���������
        StartCoroutine(SpawnEnemyRoutine());  // ������� Coroutine
    }

    private void Update()
    {
        // �������Ҽ�ҹ�
        timePassed += Time.deltaTime;

        // �ء � 60 �Թҷ� �����ѵ�ҡ���Դ�ѵ��
        if (timePassed >= 60f)
        {
            timePassed = 0f;  // ��������
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.5f);  // Ŵ����㹡���Դ�ѵ�� (��鹵�� 1 �Թҷ�)
            Debug.Log("Enemies will spawn faster!");
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);  // �ͨ����Ҩж֧���ҷ���˹�

            // �������˹��Դ��������
            Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;

            // ���ҧ�ѵ��㹵��˹觷������
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

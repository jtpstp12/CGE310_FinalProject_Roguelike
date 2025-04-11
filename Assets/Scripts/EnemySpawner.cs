using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;  // อ้างอิงไปยัง Prefab ของศัตรู
    [SerializeField] private float spawnRadius = 5f;  // รัศมีที่ศัตรูจะเกิดรอบ ๆ ผู้เล่น
    [SerializeField] private float initialSpawnInterval = 3f;  // เวลาระหว่างการเกิดศัตรูเริ่มต้น
    private float spawnInterval;  // ตัวแปรสำหรับเก็บเวลาในการเกิดศัตรู
    private float timePassed = 0f;  // ตัวแปรเก็บเวลา

    [SerializeField] private Transform player;  // ตัวแปรสำหรับอ้างอิงไปยังผู้เล่น

    private void Start()
    {
        spawnInterval = initialSpawnInterval;  // ตั้งค่าเวลาระหว่างการเกิดศัตรูเริ่มต้น
        StartCoroutine(SpawnEnemyRoutine());  // เริ่มต้น Coroutine
    }

    private void Update()
    {
        // เพิ่มเวลาผ่านไป
        timePassed += Time.deltaTime;

        // ทุก ๆ 60 วินาที เพิ่มอัตราการเกิดศัตรู
        if (timePassed >= 60f)
        {
            timePassed = 0f;  // รีเซ็ตเวลา
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.5f);  // ลดเวลาในการเกิดศัตรู (ขั้นต่ำ 1 วินาที)
            Debug.Log("Enemies will spawn faster!");
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);  // รอจนกว่าจะถึงเวลาที่กำหนด

            // สุ่มตำแหน่งเกิดใกล้ผู้เล่น
            Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;

            // สร้างศัตรูในตำแหน่งที่สุ่ม
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

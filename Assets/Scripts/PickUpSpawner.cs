using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject expObjectPrefab; // EXP Object Prefab
    [SerializeField] private GameObject healthGlobe, staminaGlobe;

    // ฟังก์ชันที่จะถูกเรียกเมื่อศัตรูตาย
    public void DropItems()
    {
        // ดรอป EXP Object ทุกครั้งที่ศัตรูตาย
        DropEXP();

        // ดรอปไอเทมอื่นๆ
        int randomNum = Random.Range(1, 10);

        if (randomNum == 1)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 0)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }
    }

    // ฟังก์ชันดรอป EXP Object
    private void DropEXP()
    {
        Instantiate(expObjectPrefab, transform.position, Quaternion.identity); // ดรอป EXP Object ที่ตำแหน่งของศัตรู
    }
}

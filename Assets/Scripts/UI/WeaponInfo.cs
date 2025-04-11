using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public float weaponCooldown;
    public float weaponDamage;  // เปลี่ยนจาก int เป็น float
    public float weaponRange;

    // ฟังก์ชันรีเซ็ตค่าอาวุธ
    public void ResetWeaponStats()
    {
        // กำหนดค่าคงที่ที่ต้องการให้รีเซ็ต
        if (this.name == "Bow")
        {
            weaponCooldown = 1f;
            weaponDamage = 3f;  // เปลี่ยนเป็น float
            weaponRange = 10f;
        }
        else if (this.name == "Sword")
        {
            weaponCooldown = 1f;
            weaponDamage = 1.5f;  // เปลี่ยนเป็น float
            weaponRange = 1f;
        }
        else if (this.name == "Staff")
        {
            weaponCooldown = 1.2f;
            weaponDamage = 1.5f;  // เปลี่ยนเป็น float
            weaponRange = 6f;
        }
    }
}

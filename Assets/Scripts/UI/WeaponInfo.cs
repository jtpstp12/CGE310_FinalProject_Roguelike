using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public float weaponCooldown;
    public float weaponDamage;  // ����¹�ҡ int �� float
    public float weaponRange;

    // �ѧ��ѹ���絤�����ظ
    public void ResetWeaponStats()
    {
        // ��˹���Ҥ�������ͧ����������
        if (this.name == "Bow")
        {
            weaponCooldown = 1f;
            weaponDamage = 3f;  // ����¹�� float
            weaponRange = 10f;
        }
        else if (this.name == "Sword")
        {
            weaponCooldown = 1f;
            weaponDamage = 1.5f;  // ����¹�� float
            weaponRange = 1f;
        }
        else if (this.name == "Staff")
        {
            weaponCooldown = 1.2f;
            weaponDamage = 1.5f;  // ����¹�� float
            weaponRange = 6f;
        }
    }
}

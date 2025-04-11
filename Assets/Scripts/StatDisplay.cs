using TMPro;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI healthText;  // สำหรับแสดง HP
    public TextMeshProUGUI damageText;  // สำหรับแสดง Damage
    public TextMeshProUGUI speedText;   // สำหรับแสดง Speed

    private void Update()
    {
        UpdateUI();
    }

    // ฟังก์ชันเพื่ออัปเดต UI
    private void UpdateUI()
    {
        // อัปเดตค่า HP
        healthText.text = "HP: " + PlayerHealth.Instance.CurrentHealth + "/" + PlayerHealth.Instance.MaxHealth;

        // อัปเดตค่า Damage
        damageText.text = "Damage: " + (ActiveWeapon.Instance.CurrentActiveWeapon.GetComponent<IWeapon>().GetWeaponInfo().weaponDamage);

        // อัปเดตค่า Speed
        speedText.text = "Speed: " + PlayerController.Instance.MoveSpeed;
    }
}

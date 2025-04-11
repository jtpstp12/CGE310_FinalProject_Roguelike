using TMPro;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI healthText;  // ����Ѻ�ʴ� HP
    public TextMeshProUGUI damageText;  // ����Ѻ�ʴ� Damage
    public TextMeshProUGUI speedText;   // ����Ѻ�ʴ� Speed

    private void Update()
    {
        UpdateUI();
    }

    // �ѧ��ѹ�����ѻവ UI
    private void UpdateUI()
    {
        // �ѻവ��� HP
        healthText.text = "HP: " + PlayerHealth.Instance.CurrentHealth + "/" + PlayerHealth.Instance.MaxHealth;

        // �ѻവ��� Damage
        damageText.text = "Damage: " + (ActiveWeapon.Instance.CurrentActiveWeapon.GetComponent<IWeapon>().GetWeaponInfo().weaponDamage);

        // �ѻവ��� Speed
        speedText.text = "Speed: " + PlayerController.Instance.MoveSpeed;
    }
}

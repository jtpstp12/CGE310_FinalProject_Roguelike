using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpWindow : Singleton<LevelUpWindow>
{
    public GameObject levelUpPanel;
    public Button hpButton;
    public Button attackButton;
    public Button speedButton;
    public Button rangeButton;

    private void Start()
    {
        levelUpPanel.SetActive(false);

        hpButton.onClick.AddListener(() => Upgrade("HP"));
        attackButton.onClick.AddListener(() => Upgrade("Attack"));
        speedButton.onClick.AddListener(() => Upgrade("Speed"));
        rangeButton.onClick.AddListener(() => Upgrade("Range"));
    }

    public void ShowLevelUpWindow(int level)
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0;  // หยุดเวลาในเกม
    }

    private void Upgrade(string upgradeType)
    {
        if (upgradeType == "HP")
        {
            PlayerHealth.Instance.MaxHealth += 1;  // เพิ่ม HP
        }
        else if (upgradeType == "Attack")
        {
            ActiveWeapon.Instance.CurrentActiveWeapon.GetComponent<IWeapon>().GetWeaponInfo().weaponDamage += Mathf.RoundToInt(0.3f);  // เพิ่ม damage
        }
        else if (upgradeType == "Speed")
        {
            PlayerController.Instance.MoveSpeed += 0.2f;  // เพิ่มความเร็ว
        }
        else if (upgradeType == "Range")
        {
            ActiveWeapon.Instance.CurrentActiveWeapon.GetComponent<IWeapon>().GetWeaponInfo().weaponRange += (0.3f);  // เพิ่ม range
        }

        CloseLevelUpWindow();
    }

    public void CloseLevelUpWindow()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1;  // เริ่มเวลาในเกมอีกครั้ง
    }
}

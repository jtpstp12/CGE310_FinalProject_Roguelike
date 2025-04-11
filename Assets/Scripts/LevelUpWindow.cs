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
    public Button cooldownButton;

    private List<Button> buttons = new List<Button>();
    private List<string> availableUpgrades = new List<string> { "HP", "Attack", "Speed", "Range", "Cooldown" };

    private void Start()
    {
        levelUpPanel.SetActive(false);

        // ผูกปุ่มกับฟังก์ชันอัปเกรด
        hpButton.onClick.AddListener(() => Upgrade("HP"));
        attackButton.onClick.AddListener(() => Upgrade("Attack"));
        speedButton.onClick.AddListener(() => Upgrade("Speed"));
        rangeButton.onClick.AddListener(() => Upgrade("Range"));
        cooldownButton.onClick.AddListener(() => Upgrade("Cooldown"));

        buttons.Add(hpButton);
        buttons.Add(attackButton);
        buttons.Add(speedButton);
        buttons.Add(rangeButton);
        buttons.Add(cooldownButton);

        HideAllButtons();
    }

    public void ShowLevelUpWindow(int level)
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0;
        ShowAllButtons();
    }

    private void ShowAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
            button.interactable = true;
            button.GetComponent<Image>().color = Color.white;
        }
    }

    private void UpdateButtonsWithDisabled(List<string> selectedUpgrades)
    {
        foreach (Button button in buttons)
        {
            if (selectedUpgrades.Contains(button.name))
            {
                button.interactable = true;
                button.GetComponent<Image>().color = Color.white;
            }
            else
            {
                button.interactable = false;
                button.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }

    public void Upgrade(string upgradeType)
    {
        if (upgradeType == "HP")
        {
            PlayerHealth.Instance.MaxHealth += 1;
        }
        else if (upgradeType == "Attack" || upgradeType == "Range" || upgradeType == "Cooldown")
        {
            // ✅ อัปเกรดทุกอาวุธใน inventory
            foreach (Transform slot in ActiveInventory.Instance.transform)
            {
                InventorySlot inventorySlot = slot.GetComponentInChildren<InventorySlot>();
                if (inventorySlot != null)
                {
                    WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
                    if (weaponInfo != null)
                    {
                        if (upgradeType == "Attack")
                            weaponInfo.weaponDamage += 0.5f;

                        else if (upgradeType == "Range")
                            weaponInfo.weaponRange += 1f;

                        else if (upgradeType == "Cooldown")
                            weaponInfo.weaponCooldown = Mathf.Max(0.1f, weaponInfo.weaponCooldown - 0.05f);
                    }
                }
            }
        }
        else if (upgradeType == "Speed")
        {
            PlayerController.Instance.MoveSpeed += 0.2f;
        }

        CloseLevelUpWindow();
    }

    private void HideAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void CloseLevelUpWindow()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
}

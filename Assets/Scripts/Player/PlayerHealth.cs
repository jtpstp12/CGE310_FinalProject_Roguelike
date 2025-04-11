using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public int currentLevel = 1;
    public int currentEXP = 0;
    public int maxEXP = 100;
    public Slider expSlider;
    public TextMeshProUGUI levelText;

    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Getter/Setter for currentHealth and maxHealth
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public void AddEXP(int expAmount)
    {
        currentEXP += expAmount;
        if (currentEXP >= maxEXP)
        {
            LevelUp();
        }
        UpdateUI();
    }

    private void LevelUp()
    {
        currentLevel++;
        currentEXP = 0;  // รีเซ็ต EXP
        maxEXP += 50;  // เพิ่ม EXP ที่ต้องการสำหรับเลเวลถัดไป

        ShowLevelUpUI();
    }

    private void ShowLevelUpUI()
    {
        LevelUpWindow.Instance.ShowLevelUpWindow(currentLevel);
    }

    private void UpdateUI()
    {
        levelText.text = currentLevel.ToString();
        if (expSlider != null)
        {
            expSlider.value = (float)currentEXP / maxEXP;  // ปรับแถบ EXP ตามค่าของ EXP ปัจจุบัน
        }
    }
}

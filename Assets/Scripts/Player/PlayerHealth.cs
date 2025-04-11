using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    // ระบบอาวุธ
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private WeaponInfo weaponInfo1;
    [SerializeField] private WeaponInfo weaponInfo2;// 👉 อ้างอิงไปยัง ScriptableObject ของอาวุธ

    // ระบบเลเวล & EXP
    public int currentLevel = 1;
    public int currentEXP = 0;
    public int maxEXP = 100;
    public Slider expSlider;
    public TextMeshProUGUI levelText;

    // สถานะ
    public bool isDead { get; private set; }

    // พลังชีวิต & ดาเมจ
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    // ตัวจับเวลา
    private float timeAlive = 0f;
    public TextMeshProUGUI timerText;

    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    // UI สำหรับ Game Over
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverTimeText;
    public Button restartButton;
    public Button mainMenuButton;

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

        // 🔄 รีเซ็ตค่าอาวุธตอนเริ่มเกม
        if (weaponInfo != null)
        {
            weaponInfo.ResetWeaponStats();
            Debug.Log($"Reset weapon stats for: {weaponInfo.name}");
        }
        if (weaponInfo1 != null)
        {
            weaponInfo1.ResetWeaponStats();
            Debug.Log($"Reset weapon stats for: {weaponInfo1.name}");
        }

        if (weaponInfo2 != null)
        {
            weaponInfo2.ResetWeaponStats();
            Debug.Log($"Reset weapon stats for: {weaponInfo2.name}");
        }


        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isDead)
        {
            timeAlive += Time.deltaTime;
            UpdateTimerUI();
        }
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
        if (!canTakeDamage) return;

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
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
        gameOverTimeText.text = "Time Played: " + FormatTime(timeAlive);
        restartButton.onClick.AddListener(GameManager.Instance.RestartGame);
        mainMenuButton.onClick.AddListener(GameManager.Instance.GoToMainMenu);
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

    private void UpdateTimerUI()
    {
        timerText.text = FormatTime(timeAlive);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

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
        currentEXP = 0;
        maxEXP += 20;
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
            expSlider.value = (float)currentEXP / maxEXP;
        }
    }
}

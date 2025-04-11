using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int baseHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private int level = 1;

    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        if (currentHealth == 0)
        {
            currentHealth = baseHealth + (level * 2);
        }
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        currentHealth = baseHealth + (level * 2);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (knockback != null)
            knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);

        if (flash != null)
            StartCoroutine(flash.FlashRoutine());

        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash != null ? flash.GetRestoreMatTime() : 0.1f);
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            if (deathVFXPrefab != null)
                Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);

            GetComponent<PickUpSpawner>()?.DropItems();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Slider staminaSlider;  // Slider แทนที่จะเป็น Image
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private int startingStamina = 3;
    private int maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

    private void Start()
    {
        // ตั้งค่า Slider
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = CurrentStamina;
    }

    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaSlider();
    }

    public void RefreshStamina()
    {
        if (CurrentStamina < maxStamina)
        {
            CurrentStamina++;
        }
        UpdateStaminaSlider();
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    private void UpdateStaminaSlider()
    {
        staminaSlider.value = CurrentStamina;

        if (CurrentStamina < maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}

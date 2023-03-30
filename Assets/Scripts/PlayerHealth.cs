using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Range(0f, 100f)][SerializeField] private int maxPlayerHealth;
    private int currentHealth;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private float timeBeforeRegenerationStarts = 3f;
    [SerializeField] private int healthRegeneartionPoints = 1;
    [SerializeField] private float healthIncrementTime = 0.1f;
    private Coroutine regenrationCoroutine;

    public static Action<int> OnTakeDamage;
    public static Action<int> OnDamage;
    public static Action<int> OnHeal;

    private void OnEnable()
    {
        OnTakeDamage += ApplyDamage;
    }

    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxPlayerHealth;
    }

    private void Update()
    {
        slider.value = currentHealth / 100f;
        healthText.SetText(currentHealth + "/100");
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        OnDamage?.Invoke(damage);

        if(currentHealth <= 0)
        {
            KillPlayer();
        }
        else if(regenrationCoroutine != null)
        {
            StopCoroutine(regenrationCoroutine);
        }

        regenrationCoroutine = StartCoroutine(RegenrationCoroutine());
    }

    private void KillPlayer()
    {
        currentHealth = 0;
        if(regenrationCoroutine != null)
        {
            StopCoroutine(regenrationCoroutine);
        }
        Debug.Log("Player has died");
    }

    private IEnumerator RegenrationCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeRegenerationStarts);
        WaitForSeconds incrementHealthTime = new WaitForSeconds(healthIncrementTime);

        while (currentHealth <= 100)
        {
            currentHealth += healthRegeneartionPoints;

            if(currentHealth > maxPlayerHealth)
            {
                currentHealth = maxPlayerHealth;
            }

            OnHeal?.Invoke(currentHealth);
            yield return incrementHealthTime;
        }

        regenrationCoroutine = null;
    }
}

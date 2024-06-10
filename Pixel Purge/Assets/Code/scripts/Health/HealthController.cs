using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied = new UnityEvent();
    public UnityEvent OnDamaged = new UnityEvent();
    public UnityEvent OnHealthChanged = new UnityEvent();

    private void Awake()
    {
        // Debug log to check if the script is loaded correctly
        Debug.Log("HealthController Awake");
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("TakeDamage called");

        if (_currentHealth == 0 || IsInvincible)
        {
            Debug.Log("No damage: either invincible or health is zero");
            return;
        }

        _currentHealth -= damageAmount;
        Debug.Log("Health reduced, OnHealthChanged invoked");
        OnHealthChanged.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            Debug.Log("Health is zero, OnDied invoked");
            OnDied.Invoke();
        }
        else
        {
            Debug.Log("OnDamaged invoked");
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        Debug.Log("AddHealth called");

        if (_currentHealth == _maximumHealth)
        {
            Debug.Log("Health is already at maximum");
            return;
        }

        _currentHealth += amountToAdd;
        Debug.Log("Health increased, OnHealthChanged invoked");
        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }
}

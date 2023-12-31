using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] bool _debug;
    [Tooltip("Health value is set to Max Health at Start (Unless debug value is checked)")]
    [SerializeField] private int _maxHealth;
    [Tooltip("Enable debug to set custom health value at Start")]
    [SerializeField] private int _health;
    [Space]
    [Tooltip("Optional health bar.")]
    [SerializeField] private Image _healthBar;


    // events for future use
    public event Action OnHealthChangeAction;
    public event Action OnDeathAction;

    [Header("Events")]
    public UnityEvent OnHealthChange;
    public UnityEvent OnDeath;

    private void Awake()
    {
        if (!_debug)
        {
            //sets the current health to max
            _health = _maxHealth;
        }

        OnHealthChangeAction += RefreshHealthBar;
    }
    private void Start()
    {
        RefreshHealthBar();
    }

    //returns health value
    public int GetHealth()
    {
        return _health;
    }
    public int GetMissingHealth()
    {
        return _maxHealth - _health;
    }

    //returns health as a percent of maxHealth
    public float GetHealthPercent()
    {
        return (float)_health / _maxHealth;
    }

    public void Damage(int damage)
    {
        _health -= damage;
        if (_health < 0) _health = 0;

        OnHealthChangeAction?.Invoke();
        OnHealthChange?.Invoke();

        if (_health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _health += amount;
        if (_health > _maxHealth) _health = _maxHealth;

        OnHealthChangeAction?.Invoke();
        OnHealthChange?.Invoke();
    }

    public void Die()
    {
        OnDeathAction?.Invoke();
        OnDeath?.Invoke();

        Debug.Log(gameObject + " Die()");
    }

    private void RefreshHealthBar()
    {
        if (_healthBar != null)
        {
            _healthBar.fillAmount = GetHealthPercent();
        }
    }
}

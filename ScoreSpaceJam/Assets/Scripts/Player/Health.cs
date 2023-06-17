using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IDamagable{
    void Damage(int value);
}

public class Health : MonoBehaviour, IDamagable
{
    private int _currentHealth = 5;
    private int _currentMaxHealth = 5;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public int CurrentMaxHealth
    {
        get { return _currentMaxHealth; }
        set { _currentMaxHealth = value; }
    }

    public UnityEvent OnDamaged;
    public UnityEvent OnHealthDepleted;

    public UnityEvent OnHealed;

    void Start()
    {
        _currentHealth = _currentMaxHealth;
    }

    public void AddMaxHealth(int value)
    {
        _currentMaxHealth += value;
    }

    public virtual void Damage(int dmgValue)
    {
        if(_currentHealth > 0) {
            _currentHealth -= dmgValue;

            if(_currentHealth <= 0) {
                OnHealthDepleted.Invoke();          
            } else 
                OnDamaged.Invoke();

            
        }      
    }

    public virtual void Heal(int healValue)
    {
        if(_currentHealth < _currentMaxHealth) {
            if(_currentHealth + healValue > _currentMaxHealth) {
                _currentHealth = _currentMaxHealth;
            } else {
                _currentHealth += healValue;
            }
            OnHealed.Invoke();
        } 
    }
}

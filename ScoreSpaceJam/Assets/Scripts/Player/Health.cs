using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IDamagable{
    void Damage(int value);
}

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private ParticleSystem sparks;
    [SerializeField] private AudioSource _takeDamageSound;
    public Slider healthSlider;
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
        healthSlider.maxValue = _currentMaxHealth;
        healthSlider.value = _currentHealth;
    }

    public void AddMaxHealth(int value)
    {
        _currentMaxHealth += value;
        healthSlider.maxValue = _currentMaxHealth;
    }

    public virtual void Damage(int dmgValue)
    {
        _takeDamageSound.Play();
        Instantiate(sparks, this.transform.position, this.transform.rotation);
        if (_currentHealth > 0) {
            _currentHealth -= dmgValue;

            if(_currentHealth <= 0) {
                OnHealthDepleted.Invoke();          
            } else 
                OnDamaged.Invoke();

            healthSlider.value = _currentHealth;

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
            healthSlider.value = _currentHealth;
        } 
    }
}

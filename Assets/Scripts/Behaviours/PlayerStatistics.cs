using System;
using TMPro;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{

    private int _currentHealth;
    
    public int maxHealth = 100;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void Update()
    {
        healthText.text = $"{_currentHealth} HP";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
            GameEvents.Instance.ToggleWinView();
    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            GameEvents.Instance.ToggleLoseView();
        }
    }

}
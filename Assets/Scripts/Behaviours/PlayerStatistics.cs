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
    
    public void Damage(int damage)
    {
        _currentHealth -= damage;
    }

}
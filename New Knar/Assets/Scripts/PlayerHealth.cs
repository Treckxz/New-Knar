using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;  // Cada segmento de vida representa una unidad de salud
    public int currentHealth;
    public Image[] healthSegments;  // Referencia a los 4 sprites de vida

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        // Solo para pruebas, se reducir� la salud cuando se presione la tecla H
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < healthSegments.Length; i++)
        {
            if (i < currentHealth)
            {
                healthSegments[i].enabled = true;
            }
            else
            {
                healthSegments[i].enabled = false;
            }
        }
    }
}

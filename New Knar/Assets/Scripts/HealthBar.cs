using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] healthSegments;  // Referencia a los 4 sprites de vida

    public void SetHealth(int health)
    {
        for (int i = 0; i < healthSegments.Length; i++)
        {
            if (i < health)
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

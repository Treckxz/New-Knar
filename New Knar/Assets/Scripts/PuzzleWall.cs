using UnityEngine;

public class PuzzleWall : MonoBehaviour
{
    public int requiredStatues = 3; // Cantidad de estatuillas necesarias para destruir la pared
    public GameObject wall; // Referencia a la pared a destruir

    void Update()
    {
        if (CollectibleStatue.statueCount >= requiredStatues)
        {
            Destroy(wall); // Destruye la pared
        }
    }
}

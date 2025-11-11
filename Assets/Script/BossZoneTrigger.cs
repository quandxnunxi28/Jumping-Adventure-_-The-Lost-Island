using UnityEngine;
using UnityEngine.UIElements;

public class BossZoneTrigger : MonoBehaviour
{
    public BossHealthBar bossHealthBar;

    public bool bossTriggered = false;
    public Vector3 position;
    public GameObject vungBoss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boss zone triggered`````!");

        if (!bossTriggered && other.CompareTag("Player"))
        {
            bossTriggered = true;
            bossHealthBar.Show(); // Hiện thanh máu boss
            Debug.Log("Boss zone triggered!");
            if (vungBoss != null)
            {
                Instantiate(vungBoss, position, Quaternion.identity);
            }
        }


    }
}

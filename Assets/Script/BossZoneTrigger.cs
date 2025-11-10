using UnityEngine;

public class BossZoneTrigger : MonoBehaviour
{
    public BossHealthBar bossHealthBar;

    public bool bossTriggered = false;
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
                Instantiate(vungBoss, new Vector3(118.3127f, -9.10671f, 0), Quaternion.identity);
            }
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;     // Player để follow
    public Transform bossZone;   // Vị trí ghim camera khi gặp boss
    public float smoothing = 5f; // Độ mượt

    private Vector3 offset;
    private bool isBossMode = false;
    public AudioClip hahaAudio; 
    private AudioSource audioSource;
    private bool isPlaySound = false;

    void Start()
    {
        offset = transform.position - player.position;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void FixedUpdate()
    {
        if (!isBossMode)
        {
            // Camera theo player
            Vector3 targetCamPos = player.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
        else
        {
            // Camera ghim ở boss
            if (!isPlaySound)
            {
                audioSource.PlayOneShot(hahaAudio);
                isPlaySound = true;
            }
            Vector3 targetCamPos = bossZone.position;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }

    public void EnterBossZone()
    {
        isBossMode = true;
    }

    public void ExitBossZone()
    {
        isBossMode = false;
    }
}

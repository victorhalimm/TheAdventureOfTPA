using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioClip[] footstepSound;
    private AudioManager manager;

    private void Awake()
    {
        manager = AudioManager.instance;
    }

    public void footSteps()
    {
        manager.source.PlayOneShot(footstepSound[Random.Range(0, footstepSound.Length)]);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float loadDelay = 0.5f;
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private AudioClip crashSound;

    private bool _hasCrashed = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground") && !_hasCrashed)
        {
            _hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSound);
            Invoke(nameof(ReloadScene), loadDelay);            
        }
    }
    
    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
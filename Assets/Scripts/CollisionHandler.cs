using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    PlayerController playerController;
    [SerializeField] float reloadDelayTime = 1f;
    [SerializeField] ParticleSystem explosionParticles;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other != null)
        {
            Debug.Log($"{this.name} ** Triggered by ** {other.gameObject.name}");
            StartCrashSequence();

        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     // Debug.Log($"{this.name} ** Triggered by ** {other.gameObject.name}");
    // }

    void StartCrashSequence()
    {
        playerController.enabled = false;
        if (explosionParticles != null)
        {
            explosionParticles.Play();
        }
        Invoke("ReloadLevel", reloadDelayTime);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

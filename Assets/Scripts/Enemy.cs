using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int score = 30;

    ScoreBoard scoreBoard;

    void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();    
    }

    void OnParticleCollision(GameObject other)
    {
        AddScore();
        DeathSequence();
    }

    private void AddScore()
    {
        scoreBoard.IncreaseScore(score);
    }

    private void DeathSequence()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }
}

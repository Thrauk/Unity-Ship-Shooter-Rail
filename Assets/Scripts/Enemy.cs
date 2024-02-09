using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject takeHitVFX;
    GameObject parent;
    [SerializeField] int score = 30;
    [SerializeField] int hitsToKill = 2;

    ScoreBoard scoreBoard;

    int hitsTaken = 0;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindWithTag("SpawnAtRuntime");
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        TakeHit();
    }

    private void AddScore(int score)
    {
        scoreBoard.IncreaseScore(score);
    }

    void TakeHit()
    {
        hitsTaken++;
        if (hitsTaken >= hitsToKill)
        {
            DeathSequence();
        }
        else
        {
            HitSequence();
        }
    }

    private void DeathSequence()
    {
        AddScore(score * 2);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(gameObject);
    }

    private void HitSequence()
    {
        AddScore(score);
        GameObject vfx = Instantiate(takeHitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
    }
}

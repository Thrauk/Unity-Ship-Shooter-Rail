using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnParticleCollision(GameObject other) {
        print("Enemy hit by laser");
        Destroy(gameObject);
    }
}

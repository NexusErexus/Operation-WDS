using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 2;

    int currentHitPoints;
    int damage = 1;

    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    public void DecreaseHealth(int damage)
    {
        currentHitPoints--;
        Debug.Log(currentHitPoints);

        if (currentHitPoints <= 0)
        {
            KillEnemy();
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        Debug.Log(currentHitPoints);
        DecreaseHealth(damage);
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5; //full hp

    private int currentHitPoints; //current hp
    private int damage = 1; //damage value

    // Start is called before the first frame update
    private void Start()
    {
        currentHitPoints = maxHitPoints; //assign full hp to the enemy
    }


    public void DecreaseHealth(int damage) //take damage
    {
        currentHitPoints -= damage;

        if (currentHitPoints <= 0)
        {
            KillEnemy();
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        DecreaseHealth(damage);
    }

    public void KillEnemy() //destroy when enemy hp is 0
    {
        Destroy(gameObject);
    }
}

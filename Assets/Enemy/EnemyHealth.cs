using UnityEngine;

[RequireComponent (typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints = 5; //full hp

    [Tooltip("Adds additional health point when enemy dies")]
    [SerializeField] private int additionalHealthPoints = 1;
    private int currentHealthPoints = 0; //current hp
    private int damage = 1; //damage value

    Enemy enemy;
    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHealthPoints = maxHealthPoints; //add full hp to the enemy
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    public void DecreaseHealth(int damage) //take damage
    {

        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            KillEnemy();
            maxHealthPoints += additionalHealthPoints;
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        DecreaseHealth(damage);
    }

    public void KillEnemy() //destroy when enemy hp is 0
    {
        enemy.RewardMoney();
        gameObject.SetActive(false);
    }
}

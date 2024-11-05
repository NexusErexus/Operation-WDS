using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform[] gun; //top turret parts for moving
    [SerializeField] private float range = 15f;
    [SerializeField] private float gunRotationSpeed = 6f;
    [SerializeField] ParticleSystem projectileParticles;
    private Transform target; //coordinates for enemy
                              // Start is called before the first frame update

    private void Start()
    {
        //projectileParticles.Stop();
    }
    // Update is called once per frame
    private void Update()
    {

        FindClosestTargetEnemy();
        AimGun();
    }

    public void FindClosestTargetEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }
    public void AimGun() //top parts of turret follow the enemy
    {
        if (target == null)
        {
            Attack(false);
            return;
        }
        
        float targetDistance = Vector3.Distance(transform.position, target.position);
        foreach (Transform t in gun)
        {
            //Vector3 direction = target.position - t.position;
            //t.rotation = Quaternion.LookRotation(direction);
            Quaternion startRotation = t.transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(target.position - t.transform.position);
            t.transform.rotation = Quaternion.Lerp(startRotation, endRotation, gunRotationSpeed * Time.deltaTime);
        }
        /*foreach (Transform t in gun)
        {
            t.LookAt(target);
        }*/
        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
        
            

        
    }

    public void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

}

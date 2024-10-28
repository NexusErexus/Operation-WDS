using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform[] gun; //top turret parts for moving
    private Transform target; //coordinates for enemy
    // Start is called before the first frame update
    
    private void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform; //access to enemy transform
    }

    // Update is called once per frame
    private void Update()
    {
        if (target != null)
        {
            AimGun();
        }
    }

    public void AimGun() //top parts of turret follow the enemy
    {
        foreach (Transform t in gun)
        {
            Vector3 direction = target.position - t.position;
            t.rotation = Quaternion.LookRotation(direction);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform[] gun;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            AimGun();
        }
    }

    void AimGun()
    {
        foreach (Transform t in gun)
        {
            Vector3 direction = target.position - t.position;
            t.rotation = Quaternion.LookRotation(direction);
        }
    }

}

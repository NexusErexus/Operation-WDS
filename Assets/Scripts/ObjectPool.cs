using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.2f, 40f)] float spawnTimer = 1f;
    [SerializeField] private int enemyOrder = 0;

    GameObject[] pool;

    private void Awake()
    {
        AddToPool();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void AddToPool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    public void EnableObjectInPool()
    {
        if (enemyOrder >= pool.Length)
        {
            enemyOrder = 0;
        }
        /*for (int i = enemyOrder; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                
                return;
            }
            
        }*/
        foreach (GameObject p in pool)
        {
            if(!p.activeInHierarchy)
            {
                p.SetActive(true);
                return;
            }
        }
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private int firstBalance = 200;
    [SerializeField] private int currentBalance;
    public int CurrentBalance
    {
        get
        {
            return currentBalance;
        }
    }

    private void Awake()
    {
        currentBalance = firstBalance;
    }

    private void Start()
    {
        StartCoroutine(IncrementMoney());
    }
    public void AddMoney(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }
    public void SpendMoney(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        if (currentBalance < 0)
        {
            currentBalance = 0;
        }
    }

    public IEnumerator IncrementMoney()
    {        
        currentBalance++;
        yield return new WaitForSeconds(1f);
        StartCoroutine(IncrementMoney());
    }
}

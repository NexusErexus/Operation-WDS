using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private int firstBalance = 200; // init balance
    [SerializeField] private int currentBalance;
    [SerializeField] private TextMeshProUGUI displayGoldBalance;
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
        UpdateGoldBalanceDisplay();
    }
    public void SpendMoney(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        if (currentBalance < 0)
        {
            currentBalance = 0;
        }
        UpdateGoldBalanceDisplay();
    }

    public IEnumerator IncrementMoney() //adding 1 coin ever second
    {
        while (true)
        {
            UpdateGoldBalanceDisplay();
            currentBalance++;
            yield return new WaitForSeconds(1f);
        }
        //StartCoroutine(IncrementMoney());
    }

    public void UpdateGoldBalanceDisplay()
    {
        displayGoldBalance.text = $"Gold: {currentBalance}";
    }
}

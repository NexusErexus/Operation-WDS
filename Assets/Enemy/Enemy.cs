using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int moneyReward = 20;
    [SerializeField] private int moneyPenalty = 20;

    Bank bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardMoney()
    {
        if (bank == null) { return; }
        bank.AddMoney(moneyReward);
    }

    public void StealMoney()
    {
        if (bank == null) { return; }
        bank.SpendMoney(moneyPenalty);
    }
}

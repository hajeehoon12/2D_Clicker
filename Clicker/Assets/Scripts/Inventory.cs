using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float Money = 0f;
    public float UpgradeCost = 10f;
    public float TotalScore = 0f;
    public Text totalText;
    public Text moneyText;
    public Text upgradeText;
    PlayerController playerController;
    

    private void Update()
    {
        totalText.text = "Total Score : " + TotalScore.ToString();
        moneyText.text = "Money : " + Money.ToString("N0");
        upgradeText.text = "Need :" + UpgradeCost.ToString("N0"); 
        playerController = GetComponent<PlayerController>();
    }


    public void UpgradeAttack()
    {
        if (Money > UpgradeCost)
        {
            Money -= UpgradeCost;
            UpgradeCost *= 1.5f;
            playerController.LevelUp();
        }
    }



}

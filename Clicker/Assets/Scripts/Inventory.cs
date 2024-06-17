using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float Money = 0f;
    public float UpgradeCost = 10f;
    public float ItemUpgradeCost = 100f;
    public float TotalScore = 0f;
    public Text totalText;
    public Text moneyText;
    public Text upgradeText;
    public Text itemUpgradeText;
    PlayerController playerController;
    private int itemlevel = 0;

    public Image upgradeImage;
    Animator animator;

    public Sprite Image1;
    public Sprite Image2;
    public Sprite Image3;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        AudioManager.instance.PlayBGM("Phoenix", 0.2f);
    }

    private void Update()
    {
        totalText.text = "Total Score : " + TotalScore.ToString("N0");
        moneyText.text = "Money : " + Money.ToString("N0");
        upgradeText.text = "Need :" + UpgradeCost.ToString("N0");
        itemUpgradeText.text = "Need : " + ItemUpgradeCost.ToString("N0");
        playerController = GetComponent<PlayerController>();
    }


    public void UpgradeAttack()
    {
        if (Money > UpgradeCost)
        {
            Money -= UpgradeCost;
            UpgradeCost *= 1.5f;
            playerController.LevelUp();
            AudioManager.instance.PlaySFX("AttackUpgrade", 0.2f);

        }
    }

    public void ItemUpgrade()
    {
        switch (itemlevel)
        {
            case 0:
                if (Money > ItemUpgradeCost)
                {
                    Money -= ItemUpgradeCost;
                    ItemUpgradeCost *= 10f;

                    itemlevel++;
                    playerController.canRoll = true;
                    upgradeImage.sprite = Image1;
                    animator.SetTrigger("Upgrade");
                    AudioManager.instance.PlaySFX("ItemUpgrade", 0.2f);
                }
                
                break;
            case 1:
                if (Money > ItemUpgradeCost)
                {
                    Money -= ItemUpgradeCost;
                    ItemUpgradeCost *= 10f;

                    itemlevel++;
                    playerController.canDash = true;
                    upgradeImage.sprite = Image2;
                    animator.SetTrigger("Upgrade");
                    AudioManager.instance.PlaySFX("ItemUpgrade", 0.2f);
                }
                

                break;
            case 2:
                if (Money > ItemUpgradeCost)
                {
                    Money -= ItemUpgradeCost;
                    ItemUpgradeCost *= 10f;

                    itemlevel++;
                    playerController.canComboAttack = true;
                    upgradeImage.sprite = Image3;
                    animator.SetTrigger("Upgrade");
                    AudioManager.instance.PlaySFX("ItemUpgrade", 0.2f);

                }
                
                break;
            case 3:
                
                break;
        
        
        
        
        }
    
    
    
    }


}

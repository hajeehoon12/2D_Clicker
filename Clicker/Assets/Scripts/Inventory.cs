using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public enum AlphaBet
{ 
    a,
    b,
    c,
    d,
    e,
    f
}




public class Inventory : MonoBehaviour
{
    [SerializeField]
    public float Money = 0f;
    public float UpgradeCost = 10f;
    public float ItemUpgradeCost = 100f;
    public float TotalScore = 0f;
    public int itemlevel = 0;
    public float attackRate = 10f;

    public Text totalText;
    public Text moneyText;
    public Text upgradeText;
    public Text itemUpgradeText;
    PlayerController playerController;

    public static Inventory instance;

    public Image upgradeImage;
    Animator animator;

    public Sprite Image1;
    public Sprite Image2;
    public Sprite Image3;

    public ParticleSystem particle;
    public ParticleSystem particle2;
    public ParticleSystem particle3;

    

    

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
        totalText.text = "Total Score : " + BigInteger(TotalScore);
        moneyText.text = "Money : " + BigInteger(Money);
        upgradeText.text = "Need :" + BigInteger(UpgradeCost);
        itemUpgradeText.text = "Need : " + BigInteger(ItemUpgradeCost);
        playerController = GetComponent<PlayerController>();
    }

    public string BigInteger(float num)
    {

        AlphaBet alphabet = 0;
        int bigNum;
        double callnum;
        string resultString;


        callnum = num;
        while (callnum > 1000)
        {
            callnum = System.Math.Truncate(callnum / 1000);
            alphabet += 1;
        }
        
        
        bigNum = (int)callnum;
        resultString = bigNum.ToString();

        if (alphabet != 0)
        {
            alphabet -= 1;
            resultString += alphabet.ToString();
        }
        //Debug.Log(alphabet.ToString());


        return resultString;

    }

    


    public void UpgradeAttack()
    {
        if (Money > UpgradeCost)
        {
            Money -= UpgradeCost;
            UpgradeCost *= 1.5f;
            attackRate *= 1.4f;
            AudioManager.instance.PlaySFX("AttackUpgrade", 0.2f);
            particle.gameObject.SetActive(true);
            if (!particle.isPlaying) particle.Play();
            StartCoroutine(ParticleOff(particle));

        }
    }

    IEnumerator ParticleOff(ParticleSystem curParticle)
    {

        yield return new WaitForSeconds(1f);
        if (curParticle.isPlaying) curParticle.Stop();
        curParticle.gameObject.SetActive(false);
        
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

                    particle2.gameObject.SetActive(true);
                    if (!particle2.isPlaying) particle2.Play();
                    StartCoroutine(ParticleOff(particle2));


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


                    particle3.gameObject.SetActive(true);
                    if (!particle3.isPlaying) particle3.Play();
                    StartCoroutine(ParticleOff(particle3));

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

                    particle3.gameObject.SetActive(true);
                    if (!particle3.isPlaying) particle3.Play();
                    StartCoroutine(ParticleOff(particle3));

                }
                
                break;
            case 3:
                
                break;
        
        
        
        
        }
    
    
    
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{

    public Inventory inven;
    public PlayerController playerController;

    public void GivePlayerReward(float Damage)
    {
        Debug.Log($"Give Money!! + {Damage}");
        inven.Money += Damage;
        inven.TotalScore += Damage;
        
    }


}

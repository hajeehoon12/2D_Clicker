using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class Reward : MonoBehaviour
{

    public Inventory inven;
    public PlayerController playerController;
    public GameData gameData;

    public string GameDataFileName = ".json";

    public void GivePlayerReward(float Damage)
    {
        Debug.Log($"Give Money!! + {Damage}");
        inven.Money += Damage;
        inven.TotalScore += Damage;
        
    }

    public void LoadData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("불러오기 성공!");
            string FromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");

            gameData = new GameData();
        }

        Load();

    }

    public void SaveData()
    {
        Save();

        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 완료!");
    }

    public void Save()
    { 
        gameData.data_Money = inven.Money;
        gameData.data_attackRate = inven.attackRate;
        gameData.data_Itemlevel = inven.itemlevel;
        gameData.data_ItemUpgradeCost = inven.ItemUpgradeCost;
        gameData.data_TotalScore = inven.TotalScore;
        gameData.data_UpgradeCost = inven.UpgradeCost;
    }

    public void Load()
    {
        inven.Money = gameData.data_Money;
        inven.attackRate = gameData.data_attackRate;
        inven.itemlevel = gameData.data_Itemlevel;
        inven.ItemUpgradeCost = gameData.data_ItemUpgradeCost;
        inven.TotalScore = gameData.data_TotalScore;
        inven.UpgradeCost = gameData.data_UpgradeCost;
    }




}

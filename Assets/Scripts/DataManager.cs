using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float player_hp;
    public float player_mp;
    public float player_atk;
}
public class DataManager : MonoBehaviour
{
    PlayerManager playerManager;
    GameData gameData =  new GameData();
    string saveFilePath;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        saveFilePath = Application.persistentDataPath + "/gamedata.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        print("存檔");
        gameData.player_atk = playerManager.Atk;
        gameData.player_hp = playerManager.Hp;
        gameData.player_mp = playerManager.Mp;
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFilePath, json);
    }
    public void LoadData()
    {
        print("讀檔");
    }
}
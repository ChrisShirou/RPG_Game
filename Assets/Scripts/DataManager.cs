using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class GameData
{
    public int player_lv;
    public float player_hp;
    public float player_mp;
    public float player_atk;
    public float player_exp;
    public Vector3 player_pos;
}
public class DataManager : MonoBehaviour
{
    GameObject player;
    PlayerManager playerManager;
    GameData gameData =  new GameData();
    public List<int> LevelExp;
    string saveFilePath;
    // Start is called before the first frame update
    void Start()
    {
        LevelExp = GetComponent<ExcelManager>().LevelExp;
        player = GameObject.Find("Player");
        playerManager = player.GetComponent<PlayerManager>();
        playerManager.Max_Exp = LevelExp[playerManager.Lv];
        saveFilePath = Application.persistentDataPath + "/gamedata.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        print("存檔");
        gameData.player_lv = playerManager.Lv;
        gameData.player_atk = playerManager.Atk;
        gameData.player_hp = playerManager.Hp;
        gameData.player_mp = playerManager.Mp;
        gameData.player_exp = playerManager.Exp;
        gameData.player_pos = player.transform.position;
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFilePath, json);
    }
    public void LoadData()
    {
        print("讀檔");
        if (File.Exists(saveFilePath))
        { 
            string json = File.ReadAllText(saveFilePath);
            gameData = JsonUtility.FromJson<GameData>(json);
            playerManager.Lv = gameData.player_lv;
            playerManager.Atk = gameData.player_atk;
            playerManager.Hp = gameData.player_hp;
            playerManager.Mp = gameData.player_mp;
            playerManager.Exp = gameData.player_exp;
            player.transform.position = gameData.player_pos;
            playerManager.Max_Exp = LevelExp[playerManager.Lv];
            playerManager.CheckLevel();
        }
    }
}
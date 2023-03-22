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
    public float player_exp;
    public float player_hp;
    public float player_mp;
    public float player_atk;
    public float player_matk;
    public float player_str;
    public float player_int;
    public float player_con;
    public float player_spi;
    public float player_agi;
    public float player_avd;
    public float player_hrt;
    public float player_bkd;
    public float player_pnt;
    public float player_cti;
    public float player_ctd;
    public float player_hlb;
    public float player_def;
    public float player_mdef;
    public float player_ctid;
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
        gameData.player_exp = playerManager.Exp;
        gameData.player_hp = playerManager.Hp;
        gameData.player_mp = playerManager.Mp;
        gameData.player_atk = playerManager.Atk;
        gameData.player_matk = playerManager.Matk;
        gameData.player_str = playerManager.Str;
        gameData.player_int = playerManager.Int;
        gameData.player_con = playerManager.Con;
        gameData.player_spi = playerManager.Spi;
        gameData.player_agi = playerManager.Agi;
        gameData.player_avd = playerManager.Avd;
        gameData.player_hrt = playerManager.Hrt;
        gameData.player_bkd = playerManager.Bkd;
        gameData.player_pnt = playerManager.Pnt;
        gameData.player_cti = playerManager.Cti;
        gameData.player_ctd = playerManager.Ctd;
        gameData.player_hlb = playerManager.Hlb;
        gameData.player_def = playerManager.Def;
        gameData.player_mdef = playerManager.Mdef;
        gameData.player_ctid = playerManager.Ctid;
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
            playerManager.Exp = gameData.player_exp;
            playerManager.Hp = gameData.player_hp;
            playerManager.Mp = gameData.player_mp;
            playerManager.Atk = gameData.player_atk;
            playerManager.Matk = gameData.player_matk;
            playerManager.Str = gameData.player_str;
            playerManager.Int = gameData.player_int;
            playerManager.Con = gameData.player_con;
            playerManager.Spi = gameData.player_spi;
            playerManager.Agi = gameData.player_agi;
            playerManager.Avd = gameData.player_avd;
            playerManager.Hrt = gameData.player_hrt;
            playerManager.Bkd = gameData.player_bkd;
            playerManager.Pnt = gameData.player_pnt;
            playerManager.Cti = gameData.player_cti;
            playerManager.Ctd = gameData.player_ctd;
            playerManager.Hlb = gameData.player_hlb;
            playerManager.Def = gameData.player_def;
            playerManager.Mdef = gameData.player_mdef;
            playerManager.Ctid = gameData.player_ctid;
            player.transform.position = gameData.player_pos;
            playerManager.Max_Exp = LevelExp[playerManager.Lv];
            playerManager.CheckLevel();
        }
    }
}
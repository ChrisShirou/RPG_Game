using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.SqlServer.Server;
using UnityEditor;
using UnityEditor.Experimental.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Steps;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    [Header("角色資訊")]
    public GameObject PlayerInfo;
    [Header("角色介面")]
    public GameObject PropertyUI;
    public GameObject ItemUI;
    public GameObject Item_Content; //背包顯示範圍
    public GameObject Item_Bar; //背包拉條
    public GameObject lattice_pos; //背包格子生成位置
    public Image lattice;   //背包格子
    public int lattice_num; //背包數量
    [Header("基礎屬性")]
    public Text Lv_t;
    public Text Exp_t;
    public Text Hp_t;
    public Text Mp_t;
    public Text Atk_t;
    public Text Matk_t;
    public Text Str_t;
    public Text Int_t;
    public Text Con_t;
    public Text Spi_t;
    public Text Agi_t;
    [Header("其他屬性")]
    public Text Avd_t;
    public Text Hrt_t;
    public Text Bkd_t;
    public Text Pnt_t;
    public Text Cti_t;
    public Text Ctd_t;
    public Text Hlb_t;
    [Header("防禦屬性")]
    public Text Def_t;
    public Text Mdef_t;
    public Text Ctid_t;
    [Header("遊戲設定")]
    public GameObject gameList;
    [Header("攻擊數字")]
    public Text text;
    public PlayerManager playerManager;
    


    private GameManager gameManager;
    private DataManager dataManager;
    private Text playerLevel;
    private float speed = 25;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dataManager = gameManager.GetComponent<DataManager>();
        playerLevel = PlayerInfo.transform.GetChild(2).GetComponent<Text>();
        print(lattice_num / 4);
        print(lattice_num % 4);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetLevelUpUI(int lv)
    {
        playerLevel.text = lv.ToString();
    }
    
    public void SetPlayerProperty()
    {
        //設定屬性介面數值
        Lv_t.text = playerManager.Lv.ToString();
        Exp_t.text = playerManager.Exp.ToString() + " / " + playerManager.Max_Exp.ToString();
        Hp_t.text = playerManager.Hp.ToString() + " / " + playerManager.Max_Hp.ToString();
        Mp_t.text = playerManager.Mp.ToString() + " / " + playerManager.Max_Mp.ToString();
        Atk_t.text = playerManager.Atk.ToString();
        Matk_t.text = playerManager.Matk.ToString();
        Str_t.text = playerManager.Str.ToString();
        Int_t.text = playerManager.Int.ToString();
        Con_t.text = playerManager.Con.ToString();
        Spi_t.text = playerManager.Spi.ToString();
        Agi_t.text = playerManager.Agi.ToString();
        Avd_t.text = playerManager.Avd.ToString();
        Hrt_t.text = playerManager.Hrt.ToString();
        Bkd_t.text = playerManager.Bkd.ToString();
        Pnt_t.text = playerManager.Pnt.ToString();
        Cti_t.text = playerManager.Cti.ToString();
        Ctd_t.text = playerManager.Ctd.ToString();
        Hlb_t.text = playerManager.Hlb.ToString();
        Def_t.text = playerManager.Def.ToString();
        Mdef_t.text = playerManager.Mdef.ToString();
        Ctid_t.text = playerManager.Ctid.ToString();
    }
    public void SetPlayerItem()
    {
        int x;
        int y;
        //
        if (lattice_num / 4 > 5)
        {
            //Item_Content
            Item_Bar.transform.localScale = Vector3.one;
            Item_Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 590 + ((lattice_num / 4) - 5) * 110);
            if (lattice_num % 4 > 0)
            {
                Item_Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 590 + ((lattice_num / 4) - 5 + 1) * 110);
            }
        }
        else
        {
            if (lattice_num % 4 > 0)
            {
                Item_Bar.transform.localScale = Vector3.one;
                Item_Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 590 + 110);
            }
            else
            {
                Item_Bar.transform.localScale = Vector3.zero;
                Debug.Log("Item_Bar 隱藏");
            }
        }
        //生成背包格子
        for (y = 0; y < lattice_num / 4; y++)
        {
            for (x = 0; x < 4; x++)
            {
                Vector3 pos = new Vector3(lattice_pos.transform.position.x + x * 110, lattice_pos.transform.position.y - y * 110, 0);
                Instantiate(lattice, pos, lattice_pos.transform.rotation, Item_Content.transform);
            }
        }
        if (lattice_num % 4 > 0)
        {
            y = lattice_num / 4;
            for (x = 0; x < lattice_num % 4; x++)
            {
                Vector3 pos = new Vector3(lattice_pos.transform.position.x + x * 110, lattice_pos.transform.position.y - y * 110, 0);
                Instantiate(lattice, pos, lattice_pos.transform.rotation, Item_Content.transform);
            }
        }
    }
    //-----------------------------------------------------------------------------------------------------------------
    public void ShowGameList(bool active)
    {
        gameManager.isMouseVisible = active;
        gameList.SetActive(active);
    }
    public void ShowPlayerProperty(bool active)
    {
        gameManager.isMouseVisible = active;
        PropertyUI.SetActive(active);
        if (active)
        {
            SetPlayerProperty();
        }
    }
    public void ShowPlayerItem(bool active)
    {
        gameManager.isMouseVisible = active;
        ItemUI.SetActive(active);
        if (active)
        {
            SetPlayerItem();
        }
    }
    public void ShowDamage(Transform target, float number)
    {
        Vector3 UIpos = Camera.main.WorldToScreenPoint(target.position) + new Vector3(0, 10, 0); //往上調整
        Text damageText = Instantiate(text, UIpos, transform.rotation, transform);
        damageText.text = number.ToString();
        damageText.color = Color.red;
        //讓傷害數字有個向上飛的動畫
        StartCoroutine(DamageTextAdjust(damageText));
    }
    IEnumerator DamageTextAdjust(Text damageText)
    {
        //設定往上移的位移量，到達位置後就destroy掉
        Vector3 targetPos = damageText.transform.position + new Vector3(0,20,0);
        while (damageText.transform.position != targetPos)
        {
            damageText.transform.position = Vector3.MoveTowards(damageText.transform.position, targetPos, speed * Time.deltaTime);
            yield return 0;
        }
        Destroy(damageText);
    }
}

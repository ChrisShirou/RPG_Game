using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    [Header("角色資訊")]
    public GameObject PlayerInfo;
    [Header("角色屬性介面")]
    public GameObject PropertyUI;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetLevelUpUI(int lv)
    {
        playerLevel.text = lv.ToString();
    }
    public void SetGameList(bool active)
    {
        gameManager.isMouseVisible = active;
        gameList.SetActive(active);
    }
    public void SetPlayerProperty(bool active)
    {
        gameManager.isMouseVisible = active;
        PropertyUI.SetActive(active);
    }
    public void ShowDamage(Transform target, float number)
    {
        Vector3 UIpos = Camera.main.WorldToScreenPoint(target.position) + new Vector3(0, 10, 0); //往上調整
        Text damageText = Instantiate(text, UIpos, transform.rotation, transform);
        damageText.text = number.ToString();
        damageText.color = Color.red;
        StartCoroutine(DamageTextAdjust(damageText));
    }
    IEnumerator DamageTextAdjust(Text damageText)
    {
        Vector3 targetPos = damageText.transform.position + new Vector3(0,20,0);
        while (damageText.transform.position != targetPos)
        {
            damageText.transform.position = Vector3.MoveTowards(damageText.transform.position, targetPos, speed * Time.deltaTime);
            yield return 0;
        }
        Destroy(damageText);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public Text text;
    public GameObject gameList;
    public float speed;

    private GameManager gameManager;
    private DataManager dataManager;
    private Text playerLevel;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dataManager = gameManager.GetComponent<DataManager>();
        playerLevel = transform.GetChild(3).transform.GetChild(2).GetComponent<Text>();
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

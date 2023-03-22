using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerManager : Property
{
    [Space]
    public GameObject gameUI;
    private Animator animator;
    private UIManager uimanager;
    
    private DataManager dataManager;
    private Slider HpBar;
    private Slider MpBar;
    private Slider ExpBar;

    public float moveSpeed = 10;
    public float rotateSpeed = 10;

    public float Max_Hp;
    public float Max_Mp;
    public float Max_Exp;
    private float rotateValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dataManager = GameObject.Find("GameManager").GetComponent<DataManager>();
        HpBar = gameUI.transform.GetChild(1).gameObject.GetComponent<Slider>();
        MpBar = gameUI.transform.GetChild(2).gameObject.GetComponent<Slider>();
        ExpBar = gameUI.transform.GetChild(4).gameObject.GetComponent<Slider>();
        uimanager = gameUI.GetComponent<UIManager>();
        //產生血條，取得產生血條底下的圖片
        //HpBar = uimanager.ShowHpBar(gameObject.transform);
        Max_Hp = this.Hp;
        Max_Mp = this.Mp;
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 50; i >= -50; i -= 10) {
        //    CreateRayCast(transform.position, transform.forward * 100 + transform.right * i, Color.red);
        //}
        //根據目前的血量調整血條長度，調整血條面向
        //HpBar.transform.LookAt(Camera.main.transform);
        HpBar.value = this.Hp / Max_Hp;
        MpBar.value = this.Mp / Max_Mp;
        ExpBar.value = this.Exp / Max_Exp;
        CheckLevel();
    }
    void CreateRayCast(Vector3 pos, Vector3 direction, Color color)
    {
        Ray ray = new Ray(pos, direction);

        Debug.DrawLine(pos, pos + direction, color);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            print(hit.point);
            print(hit.transform.position);
            print(hit.collider.gameObject);
        }
    }
    public void CheckLevel()
    {
        if (this.Exp > Max_Exp)
        {
            //等級增加、最大經驗值更新、血量回復、更新玩家UI等級
            this.Lv++;
            this.Atk += 10;
            this.Exp -= Max_Exp;
            Max_Exp = dataManager.LevelExp[this.Lv];
            this.Hp = Max_Hp;
            uimanager.SetLevelUpUI(this.Lv);
        }
    }
    public void Move(bool isForward)
    {
        Vector3 move_pos;
        if (isForward)
        { move_pos = transform.forward * moveSpeed; }
        else
        { move_pos = -transform.forward * moveSpeed; }
        transform.position = transform.position + move_pos;
    }
    public void PlayerRotate(bool isRightTrun)
    {
        float value;
        if (isRightTrun)
        { rotateValue += 1 * rotateSpeed; }
        else 
        { rotateValue -= 1 * rotateSpeed; }
        value = Mathf.Repeat(rotateValue, 360);
        transform.rotation = UnityEngine.Quaternion.Euler(0, value, 0);
    }
    public void PlayerAnimation(string anim_name)
    {
        animator.Play(anim_name);
    }
    public void PlayerSkillCheck() 
    {

    }
    public void PlayerAttack(Transform target, float atkmult) 
    {
        if (target)
        {
            print("攻擊範圍內有目標");
            //玩家進行攻擊
            float enemy_hp = target.GetComponent<EnemyManager>().Hp;
            float damage = this.Atk * atkmult / 100;
            uimanager.ShowDamage(target, damage);
            enemy_hp -= damage;
            target.GetComponent<EnemyManager>().Hp = enemy_hp;
        }
        else 
        {
            print("攻擊範圍內無目標");
        }
    }

}

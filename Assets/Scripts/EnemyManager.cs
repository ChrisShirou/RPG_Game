using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using static Data.Util.KeywordDependentCollection;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyManager : Property
{
    public Slider HpBar;
    public float move_speed = 0.01f;
    public float rotate_speed = 0.01f;

    private GameObject Player;
    private Vector3 playerLastPos;

    //是否進入緊戒狀態
    private bool isAlert = false;
    //是否進入攻擊狀態
    private bool isAttack = false;
    //是否進入攻擊狀態
    private bool isAttackCD = false;

    private float Max_Hp;
    //是否到達玩家最後位置
    //private bool isArriveLastPos = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Max_Hp = this.Hp;
    }

    // Update is called once per frame
    void Update()
    {
        //進行偵測距離
        DetectArea();

        //檢查狀態
        CheckStatus();

        //進入緊戒狀態就追蹤玩家
        if (isAlert && !isAttack) {
            //根據玩家位置移動
            var move_step = move_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerLastPos, move_step);

            //根據玩家位置轉向
            var rotate_step = rotate_speed * Time.deltaTime;
            Vector3 playerDirection = playerLastPos - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, rotate_step, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            //計算與玩家最後位置的距離
            float playerLastPos_distance = Vector3.Distance(transform.position, playerLastPos);
            if (playerLastPos_distance <= 1 && isAttack == false) {
                print("已到最後發現玩家的位置，並沒發現玩家，解除緊戒狀態");
                isAlert = false;
            }
        }
        //進行攻擊，並記時一秒進入攻擊冷卻狀態
        if (isAttack && !isAttackCD) {
            StartCoroutine("AttackTime");
            Attack();
            isAttackCD = true;
        }
    }
    void DetectArea() {

        //敵人與玩家的位置
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance <= 7 && distance > 2)
        {
            //print("進入警戒範圍");
            EnemyActionStatus(0);
            isAttack = false;
        }
        if (distance <= 2)
        {
            //print("進入攻擊範圍");
            EnemyActionStatus(1);
        }
        
    }
    void EnemyActionStatus(int status) {
        if (status == 0)
        {
            //警戒模式，會追蹤玩家最後位置
            AlertMode();
        }
        if (status == 1)
        {
            //攻擊模式，會攻擊玩家
            AttackMode();
        }
    }
    void AlertMode()
    {
        //紀錄玩家最後位置
        playerLastPos = Player.transform.position;
        if (isAlert == false) {
            //切換警戒狀態
            isAlert = true;
            //print(playerLastPos);
        }
    }
    void AttackMode()
    {
        if (isAttack == false)
        {
            //切換攻擊狀態
            isAttack = true;
        }
    }
    //攻擊間隔時間 1秒
    IEnumerator AttackTime() {
        yield return new WaitForSeconds(1);
        isAttackCD = false;
    }
    void Attack()
    {
        float player_hp = Player.GetComponent<PlayerManager>().Hp;
        //print("敵人攻擊力為: " + this.Atk);
        //print("玩家生命值為: " + player_hp);
        //print("進行攻擊");
        player_hp -= this.Atk;
        Player.GetComponent<PlayerManager>().Hp = player_hp;
        //print("玩家剩餘生命值為: " + player_hp);
    }
    void CheckStatus() 
    {
        //更新血條
        HpBar.value = this.Hp / Max_Hp;
        HpBar.transform.LookAt(Camera.main.transform);

        //檢查血量
        if (this.Hp <= 0) 
        {
            Player.GetComponent<PlayerManager>().Exp += this.Exp;
            MonsterGenerate monsterGenerate = gameObject.transform.parent.
                                              gameObject.transform.parent.
                                              gameObject.GetComponent<MonsterGenerate>();
            monsterGenerate.slimeNumber--;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data.Util.KeywordDependentCollection;

public class EnemyManager : MonoBehaviour
{
    public GameObject Player;
    public float speed = 0.01f;
    
    private Vector3 playerLastPos;

    //是否進入緊戒狀態
    private bool isAlert = false;
    //是否進入攻擊狀態
    private bool isAttack = false;

    //是否到達玩家最後位置
    //private bool isArriveLastPos = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //進行偵測距離
        DetectArea();

        //進入緊戒狀態就追蹤玩家
        if (isAlert) {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerLastPos, step);

            //計算與玩家最後位置的距離
            float playerLastPos_distance = Vector3.Distance(transform.position, playerLastPos);
            if (playerLastPos_distance <= 1 && isAttack == false) {
                print("已到最後發現玩家的位置，並沒發現玩家");
                isAlert = false;
            }
        }
    }
    void DetectArea() {

        //敵人與玩家的位置
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance <= 7 && distance > 2)
        {
            //print("進入警戒範圍");
            EnemyActionStatus(0);
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
            print(playerLastPos);
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

}

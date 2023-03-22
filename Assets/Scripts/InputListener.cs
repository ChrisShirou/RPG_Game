using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class InputListener : MonoBehaviour
{
    public GameObject Player;
    public GameObject GUI;
    private PlayerManager playerManager;
    private SkillManager skillManager;
    private UIManager uiManager;
    private GameManager gameManager;
    private CameraController cameraController;
    //private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.Find("Player");
        playerManager = Player.GetComponent<PlayerManager>();
        cameraController = Player.transform.GetChild(0).GetComponent<CameraController>();
        skillManager = Player.GetComponent<SkillManager>();
        gameManager = GetComponent<GameManager>();
        //GUI = GameObject.Find("GUI");
        uiManager = GUI.GetComponent<UIManager>();

        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyBoardAction();
    }
    void KeyBoardAction()
    {
        if (Input.GetKey(KeyCode.W)) {
            playerManager.Move(true);
            //playerManager.PlayerAnimation("Male_Walk");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //playerManager.PlayerAnimation("Male Idle");
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerManager.PlayerRotate(false);
            //playerManager.PlayerAnimation("LeftTurn");
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerManager.Move(false);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerManager.PlayerRotate(true);
            //playerManager.PlayerAnimation("RightTurn");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            skillManager.CheckPlayerSkill(PlayerSkill.NormalAttack);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //滑鼠顯示、玩家視角暫停轉動
            gameManager.isMouseVisible = true;
            cameraController.isStopRoate = true;
            uiManager.ShowGameList(true);
        }
    }
}

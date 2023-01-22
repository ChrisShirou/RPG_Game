using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    public PlayerManager playerManager;
    public float moveSpeed = 10;
    public float rotateSpeed = 10;

    private Vector3 vector3;
    private float rotateValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyBoardAction();
    }
    void KeyBoardAction()
    {
        if (Input.GetKey(KeyCode.W)) {
            vector3 = transform.forward * moveSpeed;
            playerManager.Move(vector3);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotateValue -= 1 * rotateSpeed;
            playerManager.PlayerRotate(rotateValue);
        }
        if (Input.GetKey(KeyCode.S))
        {
            vector3 = -transform.forward * moveSpeed;
            playerManager.Move(vector3);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotateValue += 1 * rotateSpeed;
            playerManager.PlayerRotate(rotateValue);
        }
    }
}

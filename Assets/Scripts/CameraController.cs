using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;
    // Update is called once per frame
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {

        //Ray ray = new Ray(transform.position, Input.mousePosition);

        //Debug.DrawLine(transform.position, transform.position + Input.mousePosition, Color.red);

        //Vector3 targetDirection = Input.mousePosition - transform.position;
        //float step = rotateSpeed * Time.deltaTime;
        //Vector3 newDirection = Vector3.RotateTowards(this.gameObject.transform.forward, targetDirection, step, 0.0F);
        //this.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);

        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 10))
        //{

        //}
        if (!gameManager.isMouseVisible)
        {
            currentRotation.x += Input.GetAxis("Mouse X") * rotateSpeed;
            currentRotation.y -= Input.GetAxis("Mouse Y") * rotateSpeed;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }
        //transform.Rotate(0, 0, -Input.GetAxis("QandE") * 90 * Time.deltaTime);
        //if (Input.GetMouseButtonDown(0))
        //    Cursor.lockState = CursorLockMode.Locked;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isMouseVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = isMouseVisible;
    }
    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}

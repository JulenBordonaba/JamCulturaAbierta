using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public ConversationView view;
    // Start is called before the first frame update
    void Start()
    {
        view.OpenConversation();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
    }
    
}

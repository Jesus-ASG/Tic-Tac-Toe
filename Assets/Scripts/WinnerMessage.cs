using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerMessage : MonoBehaviour
{
    public GameObject messagePane;
    public Text message;
    void Start()
    {
        
    }

    public void onClick()
    {
        Destroy(messagePane);
    }

    /*
    public void reset()
    {
        MainGame.player = 1;
        for (int i = 0; i < 2; i++)
        {
            //MainGame.img[i].sprite = null;
            
            MainGame.array[i] = 0;
        }
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                MainGame.board[i, j] = 0;
        Destroy(messagePane);
    }*/
    
}

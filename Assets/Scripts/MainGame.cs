using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Image[] img;
    public Sprite imgO;
    public Sprite imgX;
    static public int turn = 1; //0 = empty, 1 = X, 2 = O
    private int[,] board = new int[3, 3];
    private int[] array = new int[9];

    private System.Random random = new System.Random();

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setImage(int n)
    {
        if (MainGame.turn == 1)
        {
            img[n].sprite = imgX;
            MainGame.turn = 2;
            array[n] = 1;
        }
        else
        {
            img[n].sprite = imgO;
            MainGame.turn = 1;
            array[n] = 2;
        }
    }

    private void arrayToBoard()
    {
        int row = 0;
        for(int i = 0; i < board.Length; i++)
        {
            if (i == 3 || i == 6)
                row++;
            
        }
    }
}

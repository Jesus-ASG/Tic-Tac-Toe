using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Image[] img;
    public Text score_text;
    public Sprite imgO;
    public Sprite imgX;
    public GameObject pnl_winner;
    public Transform parent_pane;

    public static int player = 1; //0 = empty, 1 = X, 2 = O
    public static int[,] board = new int[3, 3];
    public static int[] array = new int[9];

    public static int winner = 0;
    private int xPoints = 0;
    private int oPoints = 0;

    public void setImage(int n)
    {
        if (player == 1 && array[n] == 0)
        {
            img[n].sprite = imgX;
            array[n] = 1;
            updateBoard();
            checkWinner();
            player = 2;
        }
        else if (player == 2 && array[n] == 0)
        {
            img[n].sprite = imgO;
            array[n] = 2;
            updateBoard();
            checkWinner();
            player = 1;
        }
        if (fullBoard())
        {
            winner = 0;
            showWinner();
        }
    }

    public void sample()
    {
        img[0].sprite = null;
    }

    private bool fullBoard()
    {
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == 0)
            {
                return false;
            }
        }
        return true;
    }

    // Recieves player id# 1 = X, 2 = O 
    // There are 3 simple cases: horizontal, vertical and crossed (slash '/' or back slash '\')
    private void checkWinner()
    {
        if (findHorizontal() || findVertical() || findCrossed())
            score_text.text = "Score:\nX: " + xPoints + "\nO: " + oPoints;
        
    }

    private bool findHorizontal()
    {   
        for (int i = 0; i< 3; i++)
        {
            int counter = 0;
            for (int j = 0; j< 3; j++)
            {
                if (player == board[i, j])
                    counter++;
                else
                    break;
            }
            if(counter == 3)
            {
                if (player == 1)
                    winner = 1;
                else
                    winner = 2;
                showWinner();
                return true;
            }   
        }
        return false;
    }

    private bool findVertical()
    {
        for (int i = 0; i < 3; i++)
        {
            int counter = 0;
            for (int j = 0; j < 3; j++)
            {
                if (player == board[j, i])
                    counter++;
                else
                    break;
            }
            if (counter == 3)
            {
                if (player == 1)
                    winner = 1;
                else
                    winner = 2;
                showWinner();
                return true;
            }
        }
        return false;
    }

    private bool findCrossed()
    {
        // back slash form "\"
        if(player == board[0, 0] && player == board[1, 1] && player == board[2, 2])
        {
            if (player == 1)
                winner = 1;
            else
                winner = 2;
            showWinner();
            return true;
        }
        
        // slash form "/"
        if (player == board[2, 0] && player == board[1, 1] && player == board[0, 2])
        {
            if (player == 1)
                winner = 1;
            else
                winner = 2;
            showWinner();
            return true;
        }
        return false;
    }

    private void showWinner()
    {
        if (winner == 1)
            xPoints++;
        else if (winner == 2)
            oPoints++;
        
        GameObject t = Instantiate(pnl_winner, transform.position, transform.rotation, parent_pane);
        // when this object is created winner = ?, so we have to put in 0 again
        reset();
        
    }

    public void reset()
    {
        player = 1;
        for (int i = 0; i < img.Length; i++)
        {
            img[i].sprite = null;
            array[i] = 0;
        }
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = 0;
    }

    private void updateBoard()
    {
        int row = 0;
        int col = 0;
        for(int i = 0; i < array.Length; i++, col++)
        {
            if (i == 3 || i == 6)
            {
                row++;
                col = 0;
            }
            board[row, col] = array[i];
        }
    }



}

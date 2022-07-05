using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainGame : MonoBehaviour
{
    public Image[] img;
    public TextMeshProUGUI score;
    public Sprite imgO;
    public Sprite imgX;

    public GameObject pnl_winner;
    public TextMeshProUGUI txt_winner;
    public Image line;

    private int player = 1; //0 = empty, 1 = X, 2 = O
    private int[,] board = new int[3, 3];
    private int[] array = new int[9];

    private int winner = 0;
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
        }/*
        if (fullBoard())
        {
            checkWinner();
        }*/
    }

    private bool fullBoard()
    {
        for(int i = 0; i < array.Length; i++)
            if(array[i] == 0)
                return false;
        return true;
    }

    // Recieves player id# 1 = X, 2 = O 
    // There are 3 simple cases: horizontal, vertical and crossed (slash '/' or back slash '\')
    private void checkWinner()
    {
        
        if (findHorizontal() || findVertical() || findCrossed()) 
        {
            showWinner();
            score.text = "X - " + xPoints + "\nO - " + oPoints;
        }
        else if (fullBoard())
        {
            showWinner();
        }
    }

    private bool findHorizontal()
    {
        for (int i = 0; i< board.GetLength(0); i++)
        {
            int counter = 0;
            for (int j = 0; j< board.GetLength(1); j++)
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
                return true;
            }   
        }
        return false;
    }

    private bool findVertical()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            int counter = 0;
            for (int j = 0; j < board.GetLength(1); j++)
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
                /*
                var rotationVector = line.transform.rotation.eulerAngles;
                rotationVector.z = 90;
                line.transform.rotation = Quaternion.Euler(rotationVector);
                switch (i)
                {
                    case 0:
                        line.transform.position = new Vector3(210, 0, 0);
                        break;
                    case 1:
                        line.transform.position = new Vector3(0, 0, 0);
                        break;
                    case 2:
                        line.transform.position = new Vector3(-210, 0, 0);
                        break;
                }*/

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
            var rotationVector = line.transform.rotation.eulerAngles;
            rotationVector.z = -45;
            line.transform.rotation = Quaternion.Euler(rotationVector);
            line.transform.localScale = new Vector3(1.35f, 1, 1);

            return true;
        }
        
        // slash form "/"
        if (player == board[2, 0] && player == board[1, 1] && player == board[0, 2])
        {
            if (player == 1)
                winner = 1;
            else
                winner = 2;
            var rotationVector = line.transform.rotation.eulerAngles;
            rotationVector.z = 45;
            line.transform.rotation = Quaternion.Euler(rotationVector);
            line.transform.localScale = new Vector3(1.35f, 1, 1);

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

        string win_message = "";
        switch (winner)
        {
            case 0:
                win_message = "Tie!";
                break;
            case 1:
                win_message = "X\nWins!";
                break;
            case 2:
                win_message = "O\nWins!";
                break;

        }
        txt_winner.text = win_message;
        winner = 0;

        pnl_winner.SetActive(true);



        //Instantiate(pnl_winner, transform.position, transform.rotation, parent_pane);
        // when this object is created winner = ?, so we have to put in 0 again
        //reset();
        
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
        
        var rotationVector = line.transform.rotation.eulerAngles;
        rotationVector.z = 0;
        line.transform.rotation = Quaternion.Euler(rotationVector);
        line.transform.localScale = new Vector3(1, 1, 1);
        line.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        pnl_winner.SetActive(false);
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

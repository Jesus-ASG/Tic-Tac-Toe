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
    public Transform line_transform;

    private int player = 1; //0 = empty, 1 = X, 2 = O
    private int[,] board = new int[3, 3];
    private int[] array = new int[9];

    private int winner = 0;
    private int xPoints = 0;
    private int oPoints = 0;

    public void Start()
    {
    }

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
    }

    private bool fullBoard()
    {
        for(int i = 0; i < array.Length; i++)
            if(array[i] == 0)
                return false;
        return true;
    }

    private void checkWinner()
    {
        if (findHorizontal() || findVertical() || findCrossed()) 
        {
            showWinner();
            score.text = "X - " + xPoints + "\nO - " + oPoints;
        }
        else if (fullBoard())
            showWinner();
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

                switch (i)
                {
                    case 0:
                        line.transform.position = new Vector3(line_transform.position.x,
            line_transform.position.y + 210.0f, line_transform.position.z);
                        break;
                    case 1:
                        line.transform.position = new Vector3(line_transform.position.x,
            line_transform.position.y, line_transform.position.z);
                        break;
                    case 2:
                        line.transform.position = new Vector3(line_transform.position.x,
            line_transform.position.y - 210.0f, line_transform.position.z);
                        break;
                }

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

                var rotationVector = line.transform.rotation.eulerAngles;
                rotationVector.z = 90;
                line.transform.rotation = Quaternion.Euler(rotationVector);
                switch (i)
                {
                    case 0:
                        line.transform.position = new Vector3(line_transform.position.x - 210.0f,
            line_transform.position.y, line_transform.position.z);
                        break;
                    case 1:
                        line.transform.position = new Vector3(line_transform.position.x,
            line_transform.position.y, line_transform.position.z);
                        break;
                    case 2:
                        line.transform.position = new Vector3(line_transform.position.x + 210.0f,
            line_transform.position.y, line_transform.position.z);
                        break;
                }

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
        if (winner == 0)
            line.transform.localScale = new Vector3(0, 0, 0);
        else if (winner == 1)
            xPoints++;
        else
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

        line.transform.position = new Vector3(line_transform.position.x, 
            line_transform.position.y, line_transform.position.z);
        
        var rotationVector = line.transform.rotation.eulerAngles;
        rotationVector.z = 0;
        line.transform.rotation = Quaternion.Euler(rotationVector);

        line.transform.localScale = new Vector3(1, 1, 1);

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

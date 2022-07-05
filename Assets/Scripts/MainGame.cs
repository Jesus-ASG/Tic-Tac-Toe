using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public float delay_time = 0.20f;
    private int player = 1; //0 = empty, 1 = X, 2 = O
    private int[,] board = new int[3, 3];
    private int[] array = new int[9];

    private int winner = 0;
    private int xPoints = 0;
    private int oPoints = 0;
    private int times = 30;

    System.Random random = new System.Random();

    public void Start()
    {
        pnl_winner.SetActive(false);
    }

    public void setImage(int n)
    {
        if (MainMenu.mode == 0) // Vs AI
        {
            if (player == 1 && array[n] == 0)
            {
                StartCoroutine(coroutine(n));
            }
            else if (player == 2)
            {
                // if can win, mark that box
                if (!aiCanWin())
                // else - block player
                    if(!aiCanBlock())
                // none of the above - mark a box randomly
                        aiRandom();
            }
        }
        else                // Vs Player
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
        if (findHorizontal(false) || findVertical(false) || findCrossed(false)) 
        {
            showWinner();
            score.text = "X - " + xPoints + "\nO - " + oPoints;
        }
        else if (fullBoard())
            showWinner();
    }

    private bool findHorizontal(bool tryMode)
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
                if (tryMode)
                    return true;

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

    private bool findVertical(bool tryMode)
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
                if (tryMode)
                    return true;

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

    private bool findCrossed(bool tryMode)
    {
        // back slash form "\"
        if(player == board[0, 0] && player == board[1, 1] && player == board[2, 2])
        {
            if (tryMode)
                return true;

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
            if (tryMode)
                return true;

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

    public void LoadLevel(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }

    private bool aiCanWin()
    {
        for (int i = 0; i < times; i++)
        {
            if (fullBoard())    // check if board is full
                return false;
            int aux = 0;
            while (true)        // get a valid mark
            {
                aux = random.Next(0, array.Length);
                if (array[aux] == 0)
                    break;
            }
            array[aux] = 2;         // mark box in memory as a circle
            updateBoard();          // update board from array
            if (findHorizontal(false) || findVertical(false) || findCrossed(false)) // if found a winner
            {
                img[aux].sprite = imgO; // put right sprite
                showWinner();           // show winner
                score.text = "X - " + xPoints + "\nO - " + oPoints;
                return true;
            }
            else
                array[aux] = 0; // if not, returns to zero that box
        }
        return false;
    }

    private bool aiCanBlock()
    {
        for (int i = 0; i < times; i++)
        {
            if (fullBoard())    // check if board is full
                return false;
            int aux = 0;
            while (true)        // get a valid mark
            {
                aux = random.Next(0, array.Length);
                if (array[aux] == 0)
                    break;
            }
            array[aux] = 1;         // mark box as a cross
            player = 1;             // make turn to player
            updateBoard();          // update board from array
            if (findHorizontal(true) || findVertical(true) || findCrossed(true)) // if found a winner(try mode)
            {
                array[aux] = 2;         // mark that box as a circle
                img[aux].sprite = imgO; // put right sprite
                player = 2;             // leave player as it was before
                return true;
            }
            else
            {
                array[aux] = 0; // if not, returns to zero that box
                player = 2;             // leave player as it was before
            }
        }
        return false;
    }

    private void aiRandom()
    {
        if (fullBoard())
            return;
        int aux = 0;
        while (true)
        {
            aux = random.Next(0, array.Length);
            if (array[aux] == 0)
                break;
        }
        img[aux].sprite = imgO;
        array[aux] = 2;
        updateBoard();
        checkWinner();
        player = 1;
    }

    private IEnumerator coroutine(int n)
    {
        int auxx = xPoints;
        int auxo = oPoints;
        img[n].sprite = imgX;
        array[n] = 1;
        updateBoard();
        checkWinner();
        player = 2;
        yield return new WaitForSeconds(delay_time);
        if ((auxx == xPoints) && auxo == oPoints)
        {
            setImage(player);
            player = 1;
        }
    }

}

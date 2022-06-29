using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class setImage : MonoBehaviour
{
    public Sprite imgO;
    public Sprite imgX;
    public Image img;
    public void setImagem(int n)
    {
        if (MainGame.turn == 0)
        {
            img.sprite = imgX;
            MainGame.turn = 1;
        }
        else
        {
            img.sprite = imgO;
            MainGame.turn = 0;
        }
    }
}

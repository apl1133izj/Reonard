using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonChange : MonoBehaviour
{
    public Image image;
    public Sprite[] buttonChangeImage;

    

    public void ButtonChangeImagePluse()
    {
        image.sprite = buttonChangeImage[0];
    }
    public void ButtonChangeImageminus()
    {
        image.sprite = buttonChangeImage[1];
    }
}

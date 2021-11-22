using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{
    public GameObject pref;
    public Text text_count;
    private int count = 0;
    private Image my_image;
    public Sprite sprite_image;
    private Color color;

    public void DoLEDClick()
    {
        var ele_ob = GameObject.FindWithTag("selected");
        my_image = ele_ob.GetComponent<Image>();
        color = my_image.color;
        color.a = 255;
        my_image.color = color;
        my_image.sprite = sprite_image;

    }
    
}

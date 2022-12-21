using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSetup : MonoBehaviour
{
    public List<Sprite> availableSprites;
    
    //public SpriteRenderer picture;

    public Image image;

    public void SetPicture(int idx)
    {
        image.sprite = availableSprites[idx];
        //image.transform.GetChild(0).GetComponent<Image>().sprite = availableSprites[idx];
        GetComponent<CardManager>().picturename = image.sprite.name;
    }

    private void Awake()
    {        
        //picture = transform.GetChild(0).GetComponent<SpriteRenderer>();
        var loadedSprites = Resources.LoadAll<Sprite>("Animals");
        foreach (var item in loadedSprites)
        {
            availableSprites.Add(item);
        }
    }
}

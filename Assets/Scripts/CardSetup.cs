using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetup : MonoBehaviour
{
    [SerializeField] List<Sprite> availableSprites;

    public SpriteRenderer picture;
    

    public void SetPicture(int idx)
    {
        picture.sprite = availableSprites[idx];
        GetComponent<CardManager>().picturename = picture.sprite.name;
    }


    private void Awake()
    {
        picture = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }



}

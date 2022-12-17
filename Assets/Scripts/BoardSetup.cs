using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardSetup : MonoBehaviour
{
    [Header("Cock")]
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject card;
    [SerializeField] int pairDb;   

    List<int> indexes;
    System.Random rng = new();
    int cardidx = 0;
    float gap = 0.1f;
    float cardsize = 2;
    float x;
    float basex;
    float y;
    float height;
    float width;


    //planed game sizes (heightxwidth) 3x2 4x3 4x4 5x4 6x5 8x5 8x6 9x6
    void Start()
    {
        switch (pairDb*2)
        {
            //3
            case 6:
                height = 3;
                width = 2;
                break;
            //6
            case 12:
                height = 4;
                width = 3;
                break;
            //8
            case 16:
                height = 4;
                width = 4;
                break;
            //10
            case 20:
                height = 5;
                width = 4;
                break;
            //15
            case 30:
                height = 6;
                width = 5;
                break;
            //20
            case 40:
                height = 8;
                width = 5;
                break;
            //24
            case 48:
                height = 8;
                width = 6;
                break;
            //27
            case 54:
                height = 9;
                width = 6;
                break;
        }
        mainCamera.fieldOfView = 60;
        Debug.Log(Screen.width);
        if (height%2==0)
        {
            y = -cardsize / 2 + gap / 2 + (height / 2 * cardsize) + (height / 4 * gap);
        }
        else
        {
            //y = (height / 2 * cardsize) - gap * height;
            y = (height * (cardsize + gap))/2-(cardsize+gap)/2;
        }
        if (width%2==0)
        {

            basex = cardsize/2-gap / 2 - (width / 2*cardsize)-(width/4*gap);
        }
        else
        {
            basex = -(width * (cardsize + gap)) / 2 + (cardsize + gap) / 2;

        }
        
        for (int i = 0; i < height; i++)
        {
            x = basex;
            for (int j = 0; j < width; j++)
            {
                AddCardToBoard();
                x += cardsize+gap;
            }
            y -= cardsize+gap;
        }      
    }

    private void AddCardToBoard()
    {
        
        GameObject newCard = Instantiate(card);
        
        newCard.transform.position = new Vector3(x, y, 0);
        int idx = GetSpriteIdx();
        newCard.GetComponent<CardSetup>().SetPicture(idx);

        newCard.GetComponent<CardManager>().id = cardidx;
        cardidx++;
        
        GetComponent<PairChacker>().cards.Add(newCard);
    }

    private int GetSpriteIdx()
    {
        int rngSelect = rng.Next(0, indexes.Count);
        int idx = indexes[rngSelect];
        indexes.RemoveAt(rngSelect);
        return idx;
       
    }

    private void Awake()
    {
        GameObject tmpcard = Instantiate(card);
        List<Sprite> availableSprites = new List<Sprite>();
        foreach (var item in tmpcard.GetComponent<CardSetup>().availableSprites)
        {
            availableSprites.Add(item);
        }
        tmpcard.SetActive(false);
        Debug.Log(availableSprites.Count);
        
        indexes = new List<int>();
        for (int i = 0; i < pairDb; i++)
        {
            int r;
            do
            {
                r = rng.Next(0, availableSprites.Count);
            } while (indexes.Contains(r));           
            for (int j = 0; j < 2; j++)
            {
                indexes.Add(r);
            }            
        }       
    }
}

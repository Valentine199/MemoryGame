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
    float x = -5f;
    float y = 4.5f;
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
            x = -5;
            for (int j = 0; j < pairDb; j++)
            {
                AddCardToBoard();
                x += 3;
            }
            y -= 3.5f;
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

        indexes = new List<int>();
        for (int i = 0; i < pairDb * 2; i++)
        {
            indexes.Add(i % pairDb);
        }
    }




}

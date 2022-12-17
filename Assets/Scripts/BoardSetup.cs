using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardSetup : MonoBehaviour
{
    [Header("Cock")]
    [SerializeField] GameObject card;
    [SerializeField] int pairDb;

    
    List<int> indexes;
    System.Random rng = new();
    float x = -5f;
    float y = 4.5f;
    int cardidx = 0;


    void Start()
    {
        Debug.Log(Screen.width);

        for (int i = 0; i < pairDb / 2; i++)
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

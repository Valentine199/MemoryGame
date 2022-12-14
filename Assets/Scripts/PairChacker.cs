using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairChacker : MonoBehaviour
{
    public int[] selectedCards;
    public List<GameObject> cards;

    private void Awake()
    {
        selectedCards = new int[] { -1, -1 };
        cards = new List<GameObject>();
        Debug.Log("First");
        Debug.Log(selectedCards.ToString());
    }

    public void CheckPair()
    {
        string firstCard = cards[selectedCards[0]].GetComponent<CardManager>().picturename;
        string SecondCard = cards[selectedCards[1]].GetComponent<CardManager>().picturename;
        bool isPair = firstCard.Equals(SecondCard);

        if (isPair)
        {
            freezeCards();
            //do celebration
            for (int i = 0; i < selectedCards.Length; i++)
            {
                selectedCards[i] = -1;
            }
        }

        
    }

    private void freezeCards()
    {
        for (int i = 0; i < selectedCards.Length; i++)
        {
            cards[selectedCards[i]].GetComponent<CardManager>().isFound = true;
        }
    }

    public void AddSelection(int index)
    {
        if (selectedCards[0] == -1)
        {
            selectedCards[0] = index;
        }
        else if (selectedCards[1] == -1)
        {
            selectedCards[1] = index;
        }
    }

    public void RemoveSelection(int index)
    {
        if (selectedCards[0] == index)
        {
            selectedCards[0] = -1;
        }
        else if (selectedCards[1] == index)
        {
            selectedCards[1] = -1;
        }
    }

    public bool TwoUp()
    {
        return selectedCards[0] != -1 && selectedCards[1] != -1;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    GameObject manager;
    private Animator animator;

    bool isFliped = false; // If flipped = true then you can see the picture NOW

    public bool isFound = false;
    public string picturename;
    public int id;

    private void Start()
    {
        manager = GameObject.Find("GameController");
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (!(manager.GetComponent<PairChacker>().TwoUp()))
        {

            if (!isFliped ) //picture visible
            {
                ShowCard();

                if (manager.GetComponent<PairChacker>().TwoUp())
                {
                    manager.GetComponent<PairChacker>().CheckPair();
                }
            }
            else if (!isFound && isFliped) //picture invisible
            {
                HideCard();
            }
            else
            {
                // Shake Card
            }
        }
        else
        {
            manager.GetComponent<PairChacker>().RemoveAllSelection();
        }
    }

    public void HideCard()
    {
        animator.SetTrigger("TrHide");
        //transform.rotation = Quaternion.identity;

        manager.GetComponent<PairChacker>().RemoveSelection(id);

        isFliped = false;
    }

    public void ShowCard()
    {
        //transform.rotation = Quaternion.Euler(0, 180, 0);
        animator.SetTrigger("TrShow");

        manager.GetComponent<PairChacker>().AddSelection(id);

        isFliped = true;
    }
}

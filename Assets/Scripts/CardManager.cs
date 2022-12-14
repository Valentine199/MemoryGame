using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    GameObject manager;

    bool isFliped = false; // If flipped = true then you can see the picture NOW

    public bool isFound = false;
    public string picturename;
    public int id;

    private void Start()
    {
        manager = GameObject.Find("GameController");
    }

    private void OnMouseDown()
    {
        
        
            if (!isFliped && !(manager.GetComponent<PairChacker>().TwoUp())) //picture visible
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

                manager.GetComponent<PairChacker>().AddSelection(id);

                if (manager.GetComponent<PairChacker>().TwoUp())
                {
                    manager.GetComponent<PairChacker>().CheckPair();
                }
                

            }
            else if(!isFound) //picture invisible
            {
                transform.rotation = Quaternion.identity;
                manager.GetComponent<PairChacker>().RemoveSelection(id);
            }


            isFliped = !isFliped; 
        

    }
}

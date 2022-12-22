using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BoardSetup : MonoBehaviour
{
    [Header("Cock")]
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject card;
    [SerializeField] GameObject board;
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

        // We get the grid component
        var grid = board.GetComponent<GridLayoutGroup>();

        //How big our screen is, minus the padding. We can't draw on the padding so we don't calculate with it
        float screenWidth = Screen.width - (grid.padding.left + grid.padding.right);
        float screenHeight = Screen.height - (grid.padding.top + grid.padding.bottom);

        // For full flexibility we also set the the aspect ration we have to satisfy
        board.GetComponent<AspectRatioFitter>().aspectRatio = screenWidth / screenHeight;

        // We divide the screen by how much element we want there. if we have a 1080 width and want 2 columns then we have a size of 1080/2. (remember that padding is already lovered that number)
        // We also subtract the spacing because we can't draw on it. Remeber that we here calculate the size of one cell so we don't have to multiply it by the element number
        float newWidth = (screenWidth / width) - grid.spacing.x;
        Debug.Log(newWidth);
        float newHeight = (screenHeight / height) - grid.spacing.y;
        Debug.Log(newHeight);

        //TODO 1:1 cards
        // for 1:1 ratio of the cards we do a little trick
        //float GoodRatio = newHeight < newWidth ? newHeight : newWidth;

        //// we also increase the spacing to make our elements "biggger"
        //float increase = Math.Abs(newHeight - newWidth) / 2;
        //grid.spacing = new Vector2(grid.spacing.x, grid.spacing.y + increase);

        grid.cellSize = new Vector2(newWidth, newHeight);


        for (int i = 0; i < pairDb * 2; i++)
        {
            AddCardToBoard();
        }

        /*
         //mainCamera.fieldOfView = 77;
         //mainCamera.aspect = Screen.width / Screen.height;

         float new_screen = (float)Screen.width / (float)Screen.height;
         float size = mainCamera.orthographicSize;
         mainCamera.orthographicSize = size * (new_screen / mainCamera.aspect);

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

         }*/

        /*for (int i = 0; i < height; i++)
        {
            x = basex;
            for (int j = 0; j < width; j++)
            {
                
                AddCardToBoard();
                x += cardsize+gap;
            }
            y -= cardsize+gap;
        }  */
    }

    private void AddCardToBoard()
    {
        
        GameObject newCard = Instantiate(card);
       
        
        //newCard.transform.position = new Vector3(x, y, 0);
        int idx = GetSpriteIdx();
        newCard.GetComponent<CardSetup>().SetPicture(idx);

        newCard.GetComponent<CardManager>().id = cardidx;
        cardidx++;

        newCard.transform.parent = board.transform;
        newCard.transform.localScale = new Vector2(1, 1);
        
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<int> cards;


    // Start is called before the first frame update
    void Awake()
    {
        CreateDeck();
        Shuffle();
    }


    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int r = Random.Range(0, n);
            int temp = cards[r];
            cards[r] = cards[n];
            cards[n] = temp;
        }
    }

    private void CreateDeck()
    {
        if(cards == null)
        {
            cards = new List<int>();
        }
        else
        {
            cards.Clear();
        }

        for (int i = 0; i < 10; i++)
        {
            cards.Add(i);
        }
    }

    public List<int> GetDeck()
    {
        return cards;
    }


}

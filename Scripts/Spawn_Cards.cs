using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Cards : MonoBehaviour
{
    //Create a list or an array to store the cards
    //Right now place holder is numbers(or scriptable objects).
    public Cards[] list_of_cards;


    //Make A function to Spawn Cards
    public void Spawn()
    {
        int randomIndex = 0;
        for (int i = 0; i < list_of_cards.Length; i++)
        {
            Cards temp = list_of_cards[i];
            randomIndex = Random.Range(0, list_of_cards.Length);
            list_of_cards[i] = list_of_cards[randomIndex];
            list_of_cards[randomIndex] = temp;
        }
        Debug.Log(list_of_cards[randomIndex]);

    }
}

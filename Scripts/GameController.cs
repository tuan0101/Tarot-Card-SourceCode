using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject cardPref;
    [SerializeField]
    Transform firstCard = null;

    Deck deck;
    CardModel cardModel;
    GameObject[] cards = null;
    float spaceBetween = 0.5f;

    public void Start()
    {
        StartCoroutine(SpawnCard());
        //cardModel.setPickingPhase(false);
    }

    public IEnumerator SpawnCard()
    {
        cards = new GameObject[3];
        int cardCount = 0;
        print("Card count begin: " + WaitingCards.indexList.Count);
        if (WaitingCards.indexList.Count == 0)
        {
            int i1 = Random.Range(1, 23);
            int i2 = Random.Range(1, 23);
            int i3 = Random.Range(1, 23);
            WaitingCards.indexList.Add(i1);
            WaitingCards.indexList.Add(i2);
            WaitingCards.indexList.Add(i3);
        }
        print("Card count end: " + WaitingCards.indexList.Count);
        foreach (int i in WaitingCards.indexList)
        {
            float x = spaceBetween * cardCount;
            Vector3 tempPos = firstCard.position + new Vector3(x, 0f, 0f);
            Vector3 tempRo = firstCard.eulerAngles;

            cards[cardCount] = Instantiate(cardPref) as GameObject;
            cardModel = cards[cardCount].GetComponent<CardModel>();
            cardModel.setIndex(i);
            cardModel.transform.position = tempPos;
            cardModel.transform.eulerAngles = tempRo;
           //cardModel.showModel(cardModel.transform.position);

           cardModel.transform.parent = transform;
            cardModel.setPickingPhase(false);

            cardCount++;
           
            yield return new WaitForSeconds(.5f);
        }
        
    }

    public void Refresh()
    {
        SceneManager.LoadScene("ShowResult");
    }

    public void LoadMainScreen()
    {
        SceneManager.LoadScene("Main");
    }
}

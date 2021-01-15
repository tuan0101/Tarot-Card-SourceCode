using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Deck))]
public class DeckViews : MonoBehaviour
{
    public AudioSource mainSound;
    public AudioClip[] clipArray;

    public float cardOffset;
    public GameObject cardPref;

    [SerializeField]
    float spaceBetweenCard = 0.1f;

    [SerializeField]
    Transform leftCard = null;
    [SerializeField]
    Transform rightCard = null;
    [SerializeField]
    Transform originalCard = null;

    [SerializeField] float temp;

    [SerializeField] LevelLoader loader;

    // Private attributes
    Vector3 start;
    Mainmenu menu;
    bool isSpread = false;
    Deck deck;
    CardModel cardModel;
    GameObject[] cards;
    WaitingCards wcards;

    void Start()
    {
        deck = gameObject.GetComponent<Deck>();
        wcards = gameObject.GetComponent<WaitingCards>();
        mainSound = gameObject.GetComponent<AudioSource>();

        CreateDeck();
    }

    public void StartShuffling()
    {
        //StartCoroutine(PutIntoTwoDecks());
        StartCoroutine(Shuffle());
    }

    void CreateDeck()
    {
        int num = deck.getCardNum();
        cards = new GameObject[num];
        int cardCount = 0;

        foreach (int i in deck.GetDeck())
        {
            float x = cardOffset * cardCount;

            cards[i] = Instantiate(cardPref) as GameObject;
            start = cards[i].transform.position;
            Vector3 temp = start + new Vector3(-x, 0f, -x);
            cards[i].transform.position = temp;

            cardModel = cards[i].GetComponent<CardModel>();
            cardModel.setIndex(i);

            cardCount++;
        }
    }

    // Extract cards from the deck
    void SpreadCards()
    {
        mainSound.clip = clipArray[0]; //Spread sound
        mainSound.PlayOneShot(mainSound.clip);

        int cardCount = 0;
        foreach (int i in deck.GetDeck())
        {
            float x = spaceBetweenCard * cardCount;
            Vector3 end = start + new Vector3(x, 0f);
            Vector3 startPos = cards[i].transform.position;
            Quaternion rotation = cards[i].transform.rotation;

            cardModel = cards[i].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, end, rotation, rotation, 1f));

            cardCount++;
        }
    }

    // Compress cards into the deck
    void De_Spread()
    {
        mainSound.clip = clipArray[1]; //De SPread sound
        mainSound.PlayOneShot(mainSound.clip);

        cardModel.setEmptyQueue();  // Clear current queue
        float spaceBetweenCard = 0.001f;
        int cardCount = 0;
        foreach (int i in deck.GetDeck())
        {
            float x = spaceBetweenCard * cardCount;
            Vector3 startPos = cards[i].transform.position;
            Vector3 endPos = start + new Vector3(-x, 0f, -x);
            Quaternion rotation = cards[i].transform.rotation;

            cardModel = cards[i].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, endPos, rotation, rotation, 2f));
            cardModel.isPick = false; //reset the picking card condition

            cardCount++;
        }
    }

    //Compress cards into two decks
    public IEnumerator PutIntoTwoDecks()
    {
        //Seperate cards into two piles
        int halfLength = cards.Length / 2;
        int cardCount = 0;
        //Add Left and right card prefab
        //Count the number of cards on the list and divide by two
        //Then use the divided number into two new lists for the cards

        for (int i = 0; i < halfLength; ++i)
        {
            //For LeftCard
            float x = spaceBetweenCard * cardCount;
            Vector3 end = new Vector3(cardCount * 0.05f, 0, x);
            Vector3 startPos = cards[i].transform.position;
            Quaternion rotation = cards[i].transform.rotation;

            //  Vector3 temp = start + new Vector3(-x, 0, -x);
            cardModel = cards[i].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, end, rotation, rotation, 1f));

            cardCount++;

        }
        yield return new WaitForSeconds(.4f);
        //De_Spread();
        for (int i = halfLength; i < cards.Length; ++i)
        {
            //For RightCard
            float x = spaceBetweenCard * cardCount;
            Vector3 end = new Vector3(cardCount * -0.05f, 0, x);
            Vector3 startPos = -cards[i].transform.position;
            Quaternion rotation = cards[i].transform.rotation;

            // Vector3 temp = start + new Vector3(-x, 0, -x);
            cardModel = cards[i].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, end, rotation, rotation, 1f));

            cardCount++;
        }
        yield return new WaitForSeconds(.4f);

        foreach (int i in deck.GetDeck())
        {
            float x = spaceBetweenCard * cardCount;
            Vector3 end = start + new Vector3(.001f, 0, -0.15f);
            Vector3 startPos = cards[i].transform.position;
            Quaternion rotation = cards[i].transform.rotation;

            cardModel = cards[i].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, end, rotation, rotation, 2f));
            cardModel.isPick = false; //reset the picking card condition
            cardCount++;
        }
    }

    public IEnumerator Shuffle()
    {
        Queue<int> leftQueue = new Queue<int>();
        Queue<int> rightQueue = new Queue<int>();

        //Seperate cards into two piles
        int halfLength = cards.Length / 2;
        int cardCount = 0;

        Vector3 endPos;
        Vector3 startPos;
        Quaternion endRotation;

        foreach (int i in deck.GetDeck())
        {
            float x = cardOffset * cardCount;
            if (cardCount < halfLength)
            {
                endPos = leftCard.position + new Vector3(-x, 0, -x);
                endRotation = leftCard.rotation;
                leftQueue.Enqueue(i);
            }
            else
            {
                if (cardCount == halfLength) yield return new WaitForSeconds(.4f);
                //reset offset value for the right deck
                float x2 = cardOffset * (cardCount - halfLength);
                endPos = rightCard.position + new Vector3(x2, 0, x2);
                endRotation = rightCard.rotation;

                rightQueue.Enqueue(i);
            }

            startPos = cards[i].transform.position;
            Quaternion startRotation = cards[i].transform.rotation;

            cardModel = cards[i].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, endPos, startRotation, endRotation, 1f));

            cardCount++;
        }
        yield return new WaitForSeconds(.4f);

        mainSound.clip = clipArray[2]; //shullfe sound
        mainSound.PlayOneShot(mainSound.clip);

        endRotation = originalCard.rotation;
        for (int i = 0; i < cards.Length; ++i)
        {
            int index;
            float x = cardOffset * cardCount;
            if (i % 2 == 0)
            {
                index = leftQueue.Dequeue();
            }
            else
            {
                index = rightQueue.Dequeue();
            }

            endPos = originalCard.position + new Vector3(-x, 0, -x);
            startPos = cards[index].transform.position;
            Quaternion startRotation = cards[index].transform.rotation;

            cardModel = cards[index].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, endPos, startRotation, endRotation, 1.9f));

            cardModel.isPick = false; //reset the picking card condition
            cardCount++;
            yield return new WaitForSeconds(.05f);
        }
    }

    public void CollectAR()
    {
        int cardCount = 0;
        //cardModel.isPick = false;
        if (CardModel.waitingCards.Count == 3)
        {
            foreach (int i in deck.GetDeck())
            {
                cardModel = cards[i].GetComponent<CardModel>();
                if (!cardModel.isPick)
                {
                    float x = cardOffset * cardCount;
                    Vector3 spos = cards[i].transform.position;
                    Vector3 epos = originalCard.position + new Vector3(-x, 0f, -x);

                    Quaternion rotation = cards[i].transform.rotation;
                    StartCoroutine(cardModel.Move(spos, epos, rotation, rotation, 1f));
                    cardCount++;
                }
                else
                {
                    //wcards.setList(i);
                    WaitingCards.indexList.Add(i);
                    cardModel.setPickingPhase(false);
                }
                //else cardModel.inPickingPhase = false;
            }

            StartCoroutine(TransitionToNewScreenAR());
        }
        Debug.Log("print: " + CardModel.waitingCards.Count);

    }

    private void OnMouseUpAsButton()
    {
        if (!isSpread)
        {
            SpreadCards();

            isSpread = true;
        }
        else
        {
            De_Spread();
            isSpread = false;
        }
    }

    public void Collect()
    {
        int cardCount = 0;
        //cardModel.isPick = false;
        if (CardModel.waitingCards.Count == 3)
        {
            foreach (int i in deck.GetDeck())
            {
                cardModel = cards[i].GetComponent<CardModel>();
                if (!cardModel.isPick)
                {
                    float x = cardOffset * cardCount;
                    Vector3 spos = cards[i].transform.position;
                    Vector3 epos = originalCard.position + new Vector3(-x, 0f, -x);

                    Quaternion rotation = cards[i].transform.rotation;
                    StartCoroutine(cardModel.Move(spos, epos, rotation, rotation, 1f));
                    cardCount++;
                }
                else
                {
                    //wcards.setList(i);
                    WaitingCards.indexList.Add(i);
                    cardModel.setPickingPhase(false);
                }
                //else cardModel.inPickingPhase = false;
            }

            StartCoroutine(TransitionToNewScreen());
        }
        Debug.Log("print: " + CardModel.waitingCards.Count);

    }

    public IEnumerator TransitionToNewScreen()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("ShowResult");
    }

    public IEnumerator TransitionToNewScreenAR()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("ShowResult AR");
    }
}


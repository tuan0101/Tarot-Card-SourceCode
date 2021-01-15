using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardModel : MonoBehaviour
{

    static public List<CardModel> waitingCards = null;

    public Material[] materialDirectory;
    public bool isPick = false;
    public bool isDecoration = false;
    public bool isFlipped = false;
    Vector3 modCoord;
    [SerializeField] Text FirstFortune;
    [SerializeField] Text SecondFortune;
    [SerializeField] Text ThirdFortune;
    [SerializeField] int TextSize;
    [SerializeField] Canvas mEanings;

    [SerializeField] Animator aniBox;
    [SerializeField] Animator aniArthur;
    [SerializeField] Animator aniAvelyn;
    GameObject box;

    MeshRenderer myMesh;
    int cardIndex;
    RunText runText;
    public bool inPickingPhase = true;
    public int isReversed = 0; //0 is upright, 1 is reversed
    static int cardOrder = 0; //keeps track of order of cards

    AudioSource mainSound;
    public AudioSource auSrc;
    public AudioClip auClip;

    public Cards[] card_list;

    void Start()
    {
        FirstFortune = FindObjectOfType<Text>();
        myMesh = gameObject.GetComponent<MeshRenderer>();
        mainSound = gameObject.GetComponent<AudioSource>();
        ShowFace();
        waitingCards = new List<CardModel>();
        //dont touch
        auSrc = gameObject.GetComponent<AudioSource>();
        runText = FindObjectOfType<RunText>();
        box = GameObject.Find("DialogueBox");
    }

    void Update()
    {
        if (isDecoration)
        {
            this.transform.Rotate(0, 90 * Time.deltaTime, 0);
        }

        //dont touch
        if (Input.GetKey(KeyCode.Mouse0))
        {
            auSrc.Stop();
        }
    }

    public void ShowFace()
    {
        myMesh.material = materialDirectory[cardIndex];
    }

    public void showModel(Vector3 position)
    {
        modCoord = position;
        modCoord.y = 0.2f;
    }

    public void setIndex(int index)
    {
        cardIndex = index;
    }

    public int getIndex()
    {
        return cardIndex;
    }

    private IEnumerator Rotate(Vector3 start, Vector3 end)
    {
        // Time the rotation takes (0.5s)
        float lerpTime = 0.5f;
        float currentTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentTime += Time.deltaTime;
            t = currentTime / lerpTime;
            this.transform.eulerAngles = Vector3.Lerp(start, end, t);
            yield return null;
        }

    }

    private IEnumerator Rotate1()
    {
        // Time the rotation takes (0.5s)
        float lerpTime = 0.5f;
        float currentTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentTime += Time.deltaTime;
            t = currentTime / lerpTime;
            transform.RotateAround(transform.position, new Vector3(36.22f, 20f, 0), Time.deltaTime * 180f * 2);
            yield return null;
        }

    }


    public void Flip() //flips card and displays associated text
    {
        if (cardOrder == 3) cardOrder = 0; //For testing Refresh Button
        if (cardOrder == 0 && box.activeSelf)
        {
            aniBox = GameObject.Find("DialogueBox").GetComponent<Animator>();
            aniBox.SetTrigger("box");
        }

        //this part randomizes whether each card is in the reversed or not, then rotates it so
        int num = Random.Range(0, 2);
        isReversed = num;
        if (isReversed == 1)
        {
            this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 0, 180);
        }
        Vector3 start = this.transform.eulerAngles;
        //Vector3 end = this.transform.eulerAngles + new Vector3(transform.eulerAngles.x, -180f, 0f);
        Vector3 end = this.transform.eulerAngles + new Vector3(36.22f * 2, -180f, 0f);
        StartCoroutine(Rotate(start, end));

        cardOrder++;
        Display();
    }

    public void RunAnimationText(string text)
    {
        runText.quote.text = "";
        runText.title.text = card_list[cardIndex].cName;

        StartCoroutine(Typing(text));
    }

    IEnumerator Typing(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            runText.quote.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void Display()
    {
        Vector3 zero = new Vector3(0, 0, 0);
        if (cardOrder == 1)
        {
            if (isReversed == 1)
            {
                RunAnimationText(card_list[cardIndex].RePast);
                runText.title.text += " - Your Past";
                auClip = card_list[cardIndex].audioClips[3];
            }
            else
            {
                RunAnimationText(card_list[cardIndex].Past);
                runText.title.text += " - Your Past";
                //dont touch
                auClip = card_list[cardIndex].audioClips[0];
            }
        }
        else if (cardOrder == 2)
        {
            if (isReversed == 1)
            {
                RunAnimationText(card_list[cardIndex].RePresent);
                runText.title.text += " - Your Present";
                auClip = card_list[cardIndex].audioClips[4];
            }
            else
            {
                RunAnimationText(card_list[cardIndex].Present);
                runText.title.text += " - Your Present";
                //dont touch
                auClip = card_list[cardIndex].audioClips[1];
            }
        }
        else if (cardOrder == 3)
        {
            if (isReversed == 1)
            {
                RunAnimationText(card_list[cardIndex].ReFuture);
                runText.title.text += " - Your Future";
                auClip = card_list[cardIndex].audioClips[5];
            }
            else
            {
                RunAnimationText(card_list[cardIndex].Future);
                runText.title.text += " - Your Future";
                //dont touch
                auClip = card_list[cardIndex].audioClips[2];
            }
        }
        auSrc.clip = auClip;
        auSrc.Play();

    }

    public IEnumerator Move(Vector3 startPos, Vector3 endPos,
                    Quaternion startRotation, Quaternion endRotation, float speed)
    {
        float lerpTime = 0.5f;
        lerpTime = lerpTime / speed; //if speed = 1, it takes 0.5s to finsih the animation
        float currentTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentTime += Time.deltaTime;
            t = currentTime / lerpTime;
            this.transform.position = Vector3.Lerp(startPos, endPos, t);
            this.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
    }

    public void Pick()
    {
        mainSound.Play();
        float pickSpace = 0.05f;
        if (!isPick)
        {
            this.transform.position += new Vector3(0, pickSpace);
            isPick = true;

            if (waitingCards.Count < 3)
            {
                waitingCards.Add(this); // add the current card to the waiting List              
            }

            /* Add the card to the waiting List
             * Remove the first card of the List
             * Update the card position */
            else
            {
                waitingCards.Add(this);
                CardModel temp = waitingCards[0];
                waitingCards.RemoveAt(0);

                temp.isPick = false;
                temp.transform.position += new Vector3(0, -pickSpace);
            }
        }
        else // de_pick
        {
            // Remove the card from the waiting List
            foreach (CardModel cModel in waitingCards)
            {
                if (cModel.cardIndex == this.cardIndex)
                {
                    waitingCards.Remove(cModel);
                    break;
                }
            }
            this.transform.position += new Vector3(0, -pickSpace);
            isPick = false;
        }

    }

    public CardModel TopCard()
    {
        if (waitingCards.Count > 0)
        {
            return waitingCards[waitingCards.Count - 1];
        }
        else
        {
            return null;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (inPickingPhase)
        {
            //Click to Pick
            Pick();
        }
        else
        {
            //Click to flip card
            if (!isFlipped)
            {
                Flip();
                isFlipped = true;
                aniArthur = GameObject.Find("Arthur").GetComponent<Animator>();
                aniAvelyn = GameObject.Find("Avelyn").GetComponent<Animator>();
                string name = "attack1";
                int i = Random.Range(1, 4);
                if (i == 1) name = "attack1";
                if (i == 2) name = "attack2";
                if (i == 3) name = "attack3";

                aniArthur.SetTrigger(name);
                aniAvelyn.SetTrigger(name);

            }
        }
    }

    public void setEmptyQueue()
    {
        waitingCards.Clear();
    }

    public void setPickingPhase(bool value)
    {
        inPickingPhase = value;
    }

    public bool getPickingPhase()
    {
        return inPickingPhase;
    }
}

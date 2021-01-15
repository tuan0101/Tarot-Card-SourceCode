using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards")]
public class Cards : ScriptableObject
{
    public int cNumber;
    public string cName;
    public GameObject cModel;
    [TextArea(14, 10)] public string meaning;
    [TextArea(14, 10)] public string upright;
    [TextArea(14, 10)] public string downright;

    [TextArea(14, 10)] public string Past;
    [TextArea(14, 10)] public string Present;
    [TextArea(14, 10)] public string Future;

    [TextArea(14, 10)] public string RePast;
    [TextArea(14, 10)] public string RePresent;
    [TextArea(14, 10)] public string ReFuture;

    public Sprite CardImg;
    public Cards card;

    public List<AudioClip> audioClips = new List<AudioClip>();
}

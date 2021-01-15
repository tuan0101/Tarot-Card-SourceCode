using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settingsmenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider slider;
    public Dropdown resolutiondrop;
    Resolution[] resolutions;

    public void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volume", 0); //sets slider to last saved value

        resolutions = Screen.resolutions;
        resolutiondrop.ClearOptions();   //clear current options in dropdown
        List<string> options = new List<string>(); //creates list of strings

        int CurrResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) //grabs resolution as string
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                CurrResIndex = i;
            }
        }

        resolutiondrop.AddOptions(options);
        resolutiondrop.value = CurrResIndex;
        resolutiondrop.RefreshShownValue();
    }

    public void SetRes(int resindex)
    {
        Resolution resolution = resolutions[resindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setvolume (float volume) //changes volume level and saves set value
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void Setscreen(bool isfull)
    {
        Screen.fullScreen = isfull;
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DnA.managers;


namespace DnA.canvasUI
{
    public class OptionCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private Button sound, music, back;

        [SerializeField] private Sprite soundImage1;
        [SerializeField] private Sprite soundImage2;

        [SerializeField] private Sprite musicImage1;
        [SerializeField] private Sprite musicImage2;

        void Start()
        {
            GameObject.Find("Sound").GetComponentInChildren<Text>().text = "Sound on";
            sound.GetComponent<Image>().sprite = soundImage1;

            GameObject.Find("Music").GetComponentInChildren<Text>().text = "Music on";
            music.GetComponent<Image>().sprite = musicImage1;

            sound.onClick.AddListener(SoundOnClick);
            music.onClick.AddListener(MusicOnClick);
            back.onClick.AddListener(BackOnClick);
        }

        void SoundOnClick()
        {
            if (GameObject.Find("Sound").GetComponentInChildren<Text>().text == "Sound on")
            {
                sound.GetComponent<Image>().sprite = soundImage2;
                GameObject.Find("Sound").GetComponentInChildren<Text>().text = "Sound off";
            }
            else if (GameObject.Find("Sound").GetComponentInChildren<Text>().text == "Sound off")
            {
                sound.GetComponent<Image>().sprite = soundImage1;
                GameObject.Find("Sound").GetComponentInChildren<Text>().text = "Sound on";
            }
        }

        void MusicOnClick()
        {
            if (GameObject.Find("Music").GetComponentInChildren<Text>().text == "Music on")
            { 
                music.GetComponent<Image>().sprite = musicImage2;
                GameObject.Find("Music").GetComponentInChildren<Text>().text = "Music off";
            }
            else if (GameObject.Find("Music").GetComponentInChildren<Text>().text == "Music off")
            { 
                music.GetComponent<Image>().sprite = musicImage1;
                GameObject.Find("Music").GetComponentInChildren<Text>().text = "Music on";
            }
    }

        void BackOnClick()
        {
            canvasManager.SwitchCanvas("back", true,0);
        }
        

    }
}
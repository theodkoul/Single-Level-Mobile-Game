using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DnA.managers;


namespace DnA.canvasUI
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private Button play, options, exit;

        private bool inGame;

        void Start()
        {
            play.onClick.AddListener(PlayOnClick);
            options.onClick.AddListener(OptionsOnClick);
            exit.onClick.AddListener(ExitOnClick);
        }

        void PlayOnClick()
        {
            canvasManager.SwitchCanvas("game",true,0);
        }

        void ExitOnClick()
        {
            Application.Quit();
        }

        void OptionsOnClick()
        {
            canvasManager.SwitchCanvas("options",false,0);
        }

    }
}
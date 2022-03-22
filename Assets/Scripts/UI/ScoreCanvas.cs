using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DnA.managers;
using DnA.HighScoreNs;

namespace DnA.canvasUI
{
    public class ScoreCanvas : MonoBehaviour
    {

        [SerializeField] private Text currentPoints;
        [SerializeField] private InputField scoreNaming;
        [SerializeField] private Button submitScore;
        [SerializeField] private Button exitGame;
        [SerializeField] private Button newGame;

        [SerializeField] private GameObject backgroundYellow;
        
        [SerializeField] private HighScore highScore;

        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private GameCanvas gameCanvas;

        private int previousScore;

        private void Awake()
        {
            currentPoints.gameObject.SetActive(true);
            scoreNaming.gameObject.SetActive(true);
            submitScore.gameObject.SetActive(true);

            submitScore.onClick.AddListener(delegate {
                if (scoreNaming.text.Length > 3)
                {
                    scoreNaming.text = "Name your score with 3 characters only!";
                }
                else
                {
                    SaveScore(scoreNaming.text, previousScore);
                }
            });
            exitGame.onClick.AddListener(ExitOnClick);
            newGame.onClick.AddListener(PlayOnClick);
        }

        private void OnEnable()
        {
            currentPoints.gameObject.SetActive(true);
            scoreNaming.gameObject.SetActive(true);
            submitScore.gameObject.SetActive(true);

            backgroundYellow.gameObject.SetActive(true);

            exitGame.gameObject.SetActive(false);
            newGame.gameObject.SetActive(false);

            highScore.gameObject.SetActive(false);

            previousScore = canvasManager.ScorePoints();
            currentPoints.text = previousScore + " points";
            scoreNaming.text = "..Name your score here..";

            
        }
        

        private void SaveScore(string scorename, int score)
        {
            backgroundYellow.gameObject.SetActive(false);
            currentPoints.gameObject.SetActive(false);
            scoreNaming.gameObject.SetActive(false);
            submitScore.gameObject.SetActive(false);

            highScore.AddHighscoreEntry(score,scorename);
            //highScore.LoadHighScore();

            highScore.gameObject.SetActive(true);

            exitGame.gameObject.SetActive(true);
            newGame.gameObject.SetActive(true);
        }
        

        void PlayOnClick()
        {
            canvasManager.SwitchCanvas("game", true, 0);
            gameCanvas.NewGame();
        }

        void ExitOnClick()
        {
            Application.Quit();
        }

        IEnumerator WaitSeconds()
        {
            yield return new WaitForSeconds(1);
        }

    }
}
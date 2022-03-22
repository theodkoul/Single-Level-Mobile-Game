using UnityEngine;

namespace DnA.managers
{
    public class CanvasManager : MonoBehaviour
    {

        [SerializeField] private GameObject menuCanvas;
        [SerializeField] private GameObject scoreCanvas;
        [SerializeField] private GameObject optionsCanvas;
        [SerializeField] private GameObject gameCanvas;

        private bool inGameManager=false;

        private int score;

        void Awake()
        {
            Time.timeScale = 0;
            
            menuCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            scoreCanvas.SetActive(false);

        }



        public void SwitchCanvas(string canvasss, bool inGame, int points)
        {
            if (canvasss == "score")
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;

                }
                score = points;
                menuCanvas.SetActive(false);
                optionsCanvas.SetActive(false);
                scoreCanvas.SetActive(true);
            }
            else if (canvasss == "options")
            {
                inGameManager = inGame;
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;

                }
                else if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;

                }
                menuCanvas.SetActive(false);
                gameCanvas.SetActive(false);
                optionsCanvas.SetActive(true);
                scoreCanvas.SetActive(false);
            }
            else if (canvasss == "game")
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;

                }
                menuCanvas.SetActive(false);
                gameCanvas.SetActive(true);
                optionsCanvas.SetActive(false);
                scoreCanvas.SetActive(false);
                
            }
            else if (canvasss == "menu")
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;

                }
                else if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;

                }
                menuCanvas.SetActive(true);
                gameCanvas.SetActive(false);
                optionsCanvas.SetActive(false);
                scoreCanvas.SetActive(false);

            }
            else if (canvasss == "back")
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;

                }
                else if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;

                }
                if (!inGameManager)
                {
                    menuCanvas.SetActive(true);
                    gameCanvas.SetActive(false);
                    optionsCanvas.SetActive(false);
                    scoreCanvas.SetActive(false);
                }
                else if (inGameManager)
                {
                    menuCanvas.SetActive(false);
                    gameCanvas.SetActive(true);
                    optionsCanvas.SetActive(false);
                    scoreCanvas.SetActive(false);
                }
                

            }

        }

        public int ScorePoints()
        {
            return score;
        }
    }
}
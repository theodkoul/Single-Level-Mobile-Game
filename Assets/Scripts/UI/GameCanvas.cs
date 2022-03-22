using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DnA.managers;
using DnA.Heart;
using DnA.Monsters;
namespace DnA.canvasUI
{
    public class GameCanvas : MonoBehaviour
    {

        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private GameObject gameCanvas;
        [SerializeField] private Text level;
        private int levelCount;

        [SerializeField] private Text scorePoints;
        private int score;
        private int scoreMultiplier;

        [SerializeField] private GameObject hero;
        private Vector3 heroPos;

        [SerializeField] private HeroLife heart1;
        [SerializeField] private HeroLife heart2;
        [SerializeField] private HeroLife heart3;

        [SerializeField] private GameObject door;
        private bool pass;

        [SerializeField] private Text gameOver;

        [SerializeField] private Monster1Move monster1;

        public List<GameObject> monster1List;
        private int monster1Count;
        public List<GameObject> monster2List;
        private int monster2Count;
        public List<GameObject> monster3List;
        private int monster3Count;

        /* public GameObject monster1;
         [SerializeField] private GameObject monster2;
         [SerializeField] private GameObject monster3;
         [SerializeField] private GameObject slime;
         [SerializeField] private GameObject santelmo;
         [SerializeField] private GameObject cthulhu;
         [SerializeField] private GameObject cyclops;
         [SerializeField] private GameObject devil;*/

        private int xPos;
        private int yPos;

        void Awake()
        {
            monster3Count = 0;
            monster1Count = 1;
            monster2Count = 0;

            gameOver.text = "";
            gameOver.gameObject.transform.position = new Vector3(0, 0, 0);

            heroPos = hero.transform.position;
            hero.gameObject.SetActive(true);

            scoreMultiplier = 0;
            score = 0;
            scorePoints.text = "0pt";

            level.text = "LEVEL 1";
            levelCount = 1;

            pass = true;
            door.SetActive(false);

            StartCoroutine(MonsterDrop());
        }
        private void Start()
        {


        }
        void Update()
        {
            if ((GameObject.FindGameObjectsWithTag("Monster1").Length == 0) && (GameObject.FindGameObjectsWithTag("Monster2").Length == 0) && (GameObject.FindGameObjectsWithTag("Monster3").Length == 0) && (pass))
            {
                door.SetActive(true);
                score += 100 + scoreMultiplier;
                scoreMultiplier += 50;
                scorePoints.text = score + "pt";
                pass = false;
            }

        }

        IEnumerator MonsterDrop()
        {
            if ((levelCount - 1) % 10 == 0)
            {
                monster1.Speed();
                InstantiateMonsterss(-273, 82,1);
               
                if (levelCount != 1)
                {
                    monster1.SpeedUp();
                }
            } else if ((levelCount - 2) % 10 == 0)
            {
                
                InstantiateMonsterss(-273, 82,1);
                InstantiateMonsterss(245, 82,1);
                if (levelCount != 2)
                {
                    //monster2.SpeedUp();
                }
            }
            else if ((levelCount - 3) % 10 == 0)
            {
                
                InstantiateMonsterss(-273, 82,1);
                InstantiateMonsterss(245, 82,1);
                InstantiateMonsterss(-2, -68,1);
                if (levelCount != 3)
                {
                    //monster3.SpeedUp();
                }
            }
            else if ((levelCount - 4) % 10 == 0)
            {
                InstantiateMonsterss(-273, 82,1);
                InstantiateMonsterss(245, 82,1);
                InstantiateMonsterss(-2, -68, 1);
                InstantiateMonsterss(-389, -68, 1);

            }
            else if ((levelCount - 6) % 10 == 0)
            {
                InstantiateMonsterss(-273, 82, 1);
                InstantiateMonsterss(245, 82, 1);
                InstantiateMonsterss(-2, -68, 1);
                InstantiateMonsterss(-389, -68, 1);
                InstantiateMonsterss(379, -68, 1);

            }
            else if ((levelCount - 7) % 10 == 0)
            {
                InstantiateMonsterss(-273, 82, 1);
                InstantiateMonsterss(245, 82, 1);
                InstantiateMonsterss(-2, -68, 1);
                InstantiateMonsterss(-389, -68, 1);
                InstantiateMonsterss(379, -68, 1);
                InstantiateMonsterss(379, 260, 1);
            }
            else if ((levelCount - 8) % 10 == 0)
            {
                InstantiateMonsterss(-273, 82, 1);
                InstantiateMonsterss(245, 82, 1);
                InstantiateMonsterss(-2, -68, 1);
                InstantiateMonsterss(-389, -68, 1);
                InstantiateMonsterss(379, -68, 1);
                InstantiateMonsterss(379, 260, 1);
                InstantiateMonsterss(-2, 260, 1);

            }
            else if ((levelCount - 9) % 10 == 0)
            {
                InstantiateMonsterss(-273, 82, 1);
                InstantiateMonsterss(245, 82, 1);
                InstantiateMonsterss(-2, -68, 1);
                InstantiateMonsterss(-389, -68, 1);
                InstantiateMonsterss(379, -68, 1);
                InstantiateMonsterss(379, 260, 1);
                InstantiateMonsterss(-2, 260, 1);
                InstantiateMonsterss(-389, 260, 1);
            }

            if (levelCount % 5 != 0)
            {
               /* if (levelCount == 1 || levelCount == 2 || levelCount == 3)
                {
                    if (levelCount == 1)
                    {
                        InstantiateMonsterss(-273, 82);
                    }
                    else if (levelCount == 2)
                    {
                        //first monster
                        InstantiateMonsterss(-273, 82);

                        //second monster
                        InstantiateMonsterss(245, 82);

                    }
                    else if (levelCount == 3)
                    {
                        InstantiateMonsterss(-273, 82);
                        InstantiateMonsterss(245, 82);
                        InstantiateMonsterss(-2, -68);
                        monster1count = 3;
                    }
                }
                else
                {
                    if (levelCount % 3 == 0 && levelCount != 3)
                    {
                        monster1Count++;
                    }
                    if (levelCount % 4 == 0)
                    {
                        monster2count++;
                    }
                    if (levelCount % 6 == 0)
                    {
                        monster3count++;
                    }
                    for (int i = 0; i < monster1count; i++)
                    {
                        GameObject prefabToSpawn = monster1List[Random.Range(0, monster1List.Count - 1)];

                        float xPos = Random.Range(-365, 540);
                        float yPos = Random.Range(-277, 268);
                        Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);

                        Vector3 scaleChange = new Vector3(15.588f, 15.588f, 1.3f);

                        GameObject spawnObj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity) as GameObject;
                        spawnObj.transform.parent = gameCanvas.transform;
                        spawnObj.transform.localPosition = spawnPosition;
                        spawnObj.transform.localScale = scaleChange;
                    }
                }*/
            }

            yield return null;
        }

        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(2);
            gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(2);
            canvasManager.SwitchCanvas("score", true, score);
            gameOver.text = "";
        }

        IEnumerator WaitSeconds()
        {
            yield return new WaitForSeconds(1);
        }

        private void InstantiateMonsterss(float xPos, float yPos, int number)
        {
            if (number == 1)
            {
                GameObject prefabToSpawn = monster1List[Random.Range(0, monster1List.Count - 1)];

                Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);

                Vector3 scaleChange = new Vector3(15.588f, 15.588f, 1.3f);

                GameObject spawnObj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity) as GameObject;
                spawnObj.transform.parent = gameCanvas.transform;
                spawnObj.transform.localPosition = spawnPosition;
                spawnObj.transform.localScale = scaleChange;
                monster1Count++;
            }
            else if (number == 2)
            {
                GameObject prefabToSpawn = monster2List[Random.Range(0, monster2List.Count - 1)];

                Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);

                Vector3 scaleChange = new Vector3(15.588f, 15.588f, 1.3f);

                GameObject spawnObj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity) as GameObject;
                spawnObj.transform.parent = gameCanvas.transform;
                spawnObj.transform.localPosition = spawnPosition;
                spawnObj.transform.localScale = scaleChange;
                monster2Count++;
            }
            else if (number == 3)
            {
                GameObject prefabToSpawn = monster3List[Random.Range(0, monster3List.Count - 1)];

                Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);

                Vector3 scaleChange = new Vector3(15.588f, 15.588f, 1.3f);

                GameObject spawnObj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity) as GameObject;
                spawnObj.transform.parent = gameCanvas.transform;
                spawnObj.transform.localPosition = spawnPosition;
                spawnObj.transform.localScale = scaleChange;
                monster3Count++;
            }
        }

        public void LoseLife(int heroLife)
        {
            if (heroLife == 3)
            {
                heart3.LoseLifeAnim();
            }
            else if (heroLife == 2)
            {
                heart2.LoseLifeAnim();
            }
            else if (heroLife == 1)
            {
                heart1.LoseLifeAnim();
                StartCoroutine(EndGame());
            }
            else if (heroLife == 0)
            {
                heart1.LoseLifeAnim();
                heart2.LoseLifeAnim();
                heart3.LoseLifeAnim();
                StartCoroutine(EndGame());
            }
            StartCoroutine(WaitSeconds());

        }


        public void NextLevel()
        {
            hero.gameObject.SetActive(true);

            hero.transform.position = heroPos;
            levelCount++;
            level.text = "LEVEL " + levelCount;
            door.SetActive(false);
            pass = true;
            StartCoroutine(MonsterDrop());
        }

        public void NewGame()
        {
            if (levelCount % 5 != 0)
            {
                if (GameObject.FindGameObjectWithTag("Monster1") != null)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Monster1").Length; i++)
                    {
                        Destroy(GameObject.FindGameObjectsWithTag("Monster1")[i]);
                    }
                }
                if (GameObject.FindGameObjectWithTag("Monster2") != null)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Monster2").Length; i++)
                    {
                        Destroy(GameObject.FindGameObjectsWithTag("Monster2")[i]);
                    }
                }
                if (GameObject.FindGameObjectWithTag("Monster3") != null)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Monster3").Length; i++)
                    {
                        Destroy(GameObject.FindGameObjectsWithTag("Monster3")[i]);
                    }
                }
            }
            else
            {

            }

            monster1Count = 1;
            monster2Count = 0;
            monster3Count = 0;

            hero.gameObject.SetActive(true);
            hero.transform.position = heroPos;

            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);

            levelCount = 1;
            level.text = "LEVEL " + levelCount;
            door.SetActive(false);
            pass = true;
            StartCoroutine(MonsterDrop());
        }
    }
}
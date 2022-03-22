using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnA.managers;
using DnA.canvasUI;
using UnityEngine.UI;

namespace DnA.Monsters
{
    public class Monster1Move : MonoBehaviour
    {

        private Rigidbody2D rb;
        private float delta = 1f;
        public float speed;

        private int hitPoints = 2;

        private Vector3 startPos;

        private void Awake()
        {
           
        }
        private void Start()
        {
            StartCoroutine(WaitSeconds());
        }

        private void FixedUpdate()
        {
           
            StartCoroutine(MonsterMove());
        }

        public void Speed()
        {
            speed = 3.1f;
        }

        public void SpeedUp()
        {
            speed=speed*13/10;
            Debug.Log(speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Arrow"))
            {
                if (hitPoints == 1)
                {
                    Destroy(gameObject, 0.1f);
                }
                else
                {
                    hitPoints = 1;
                }
            }
            if (collision.gameObject.CompareTag("Hole"))
            {
                Destroy(gameObject, 0.1f);
            }
        }

        IEnumerator WaitSeconds()
        {
            yield return new WaitForSeconds(1);
            startPos = transform.position;
        }

        IEnumerator MonsterMove()
        {
            yield return new WaitForSeconds(1);
            Vector3 v = startPos;
            v.x += delta * Mathf.Sin(Time.time * speed);
            transform.position = v;
        }

    }
}
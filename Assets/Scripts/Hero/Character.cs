using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using DnA.managers;
using DnA.canvasUI;

namespace DnA.Hero
{
    public class Character : MonoBehaviour
    {

        private Rigidbody2D rb;
        private Animator anim;
        private float moveSpeed;
        private float dirX;
        private bool facingRight = true;

        private Transform heroTransform;
        private Vector3 localScale;

        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private GameCanvas gameCanvas;

        public GameObject arrowEmmiter;
        public GameObject arrow;
        public float arrowForwardForce;
        
        private int heroLifeTotal;

        private void Start()
        {
            heroLifeTotal = 3;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            localScale = transform.localScale;
            moveSpeed = 6f;

            heroTransform = gameObject.transform;
        }

        private void Update()
        {
            //CrossPlatformInputManager or Input
            dirX = CrossPlatformInputManager.GetAxisRaw("Horizontal") * moveSpeed;

            if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
            {
                rb.AddForce(Vector2.up * 900f);
            }
            if (CrossPlatformInputManager.GetButtonDown("Shoot"))
            {

                anim.SetBool("isShooting", true);
                Invoke("ShootAnimation", 0.59f);
                anim.SetBool("isRunning", false);
                anim.SetBool("isFalling", false);
                anim.SetBool("isJumping", false);

                //The Arrow instantiation happens here.
                GameObject temporary_arrow_handler;
                temporary_arrow_handler = Instantiate(arrow, arrowEmmiter.transform.position, arrowEmmiter.transform.rotation) as GameObject;


                //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                Rigidbody2D Temporary_RigidBody;
                Temporary_RigidBody = temporary_arrow_handler.GetComponent<Rigidbody2D>();

                //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
                if (facingRight)
                {
                    Temporary_RigidBody.AddForce(transform.right * arrowForwardForce);
                }
                else
                {
                    temporary_arrow_handler.transform.eulerAngles = new Vector3(
                        temporary_arrow_handler.transform.eulerAngles.x,
                        temporary_arrow_handler.transform.eulerAngles.y + 180,
                        temporary_arrow_handler.transform.eulerAngles.z
                    );

                    Temporary_RigidBody.AddForce(-transform.right * arrowForwardForce);
                }

                //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
                Destroy(temporary_arrow_handler, 10.0f);

            }

            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                canvasManager.SwitchCanvas("options", true,0);
                anim.SetBool("isShooting", false);

            }

            if ((Mathf.Abs(dirX) > 0 && rb.velocity.y == 0) && !anim.GetBool("isShooting"))
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

            if (rb.velocity.y == 0 && !anim.GetBool("isShooting"))
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", false);
            }

            if (rb.velocity.y > 0 && !anim.GetBool("isShooting"))
            {
                anim.SetBool("isJumping", true);
            }

            if (rb.velocity.y < 0 && !anim.GetBool("isShooting"))
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", true);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Monster1"))
            {
                if (heroLifeTotal == 1)
                {
                    gameCanvas.LoseLife(heroLifeTotal);
                    gameObject.SetActive(false);
                }
                else if (heroLifeTotal == 2)
                {
                    Debug.Log("1 hp left");
                    gameCanvas.LoseLife(heroLifeTotal);
                    heroLifeTotal = 1;
                }
                else if (heroLifeTotal == 3)
                {
                    Debug.Log("2 hp left");
                    gameCanvas.LoseLife(heroLifeTotal);
                    heroLifeTotal = 2;
                }
            }
            if (collision.gameObject.CompareTag("Door"))
            {
                gameCanvas.NextLevel();
            }
            StartCoroutine(WaitSeconds());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Hole"))
            {
                gameCanvas.LoseLife(0);
                gameObject.SetActive(false);
            }
        }

            private void FixedUpdate()
        {
            rb.velocity = new Vector2(dirX, rb.velocity.y);
        }

        private void ShootAnimation()
        {
            anim.SetBool("isShooting", false);
        }

        IEnumerator WaitSeconds()
        {
            yield return new WaitForSeconds(2);
        }

        private void LateUpdate()
        {
            if (dirX > 0)
                facingRight = true;
            else if (dirX < 0)
                facingRight = false;

            if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
                localScale.x *= -1;

            transform.localScale = localScale;
        }
    }
}
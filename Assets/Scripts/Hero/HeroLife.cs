using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DnA.Heart
{
    public class HeroLife : MonoBehaviour
    {
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            anim.SetBool("isDamaged", false);
        }
        

        public void LoseLifeAnim()
        {
            StartCoroutine(AnimationTime());
        }

        IEnumerator AnimationTime()
        {
            anim.SetBool("isDamaged", true);
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace CandyCars
{
    public class Player : GamePiece
    {
        private Rigidbody2D rb;
        public GameObject candyPrefab;
        public float speed = 5f;
        public float waitTime = 0.5f;
        private bool isWait = false;

        private void SetBool() => isWait = false;

        public override void Deactivate()
        {
            base.Deactivate();
            rb.velocity = Vector2.zero;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if(isPlay)
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0f);

                if (!isWait && Input.GetKeyUp(KeyCode.Space))
                {
                    isWait = true;
                    GameObject candy = Instantiate(candyPrefab, transform.position + Vector3.up, Quaternion.identity);
                    c = StartCoroutine(Wait(waitTime, SetBool));
                }
            }
        }

        Coroutine c = null;
        IEnumerator Wait(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }
    }
}


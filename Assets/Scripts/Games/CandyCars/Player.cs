using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace CandyCars
{
    public class Player : MonoBehaviour
    {
        private Vector3 startPosition;
        private Rigidbody2D rb;
        public GameObject candyPrefab;
        public float speed = 5f;
        public float waitTime = 0.5f;
        private bool isWait = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            startPosition = transform.position;
        }
        private void OnEnable()
        {
            transform.position = startPosition;
            rb.velocity = Vector3.zero;
            isWait = false;
        }

        private void OnDisable()
        {
            if (c != null) { StopCoroutine(c); }
        }


        private void Update()
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0f);

            if (!isWait && Input.GetMouseButtonUp(0))
            {
                isWait = true;
                GameObject candy = Instantiate(candyPrefab,transform.position + Vector3.up, Quaternion.identity);
                candy.transform.parent = transform.parent;
                c = StartCoroutine(Wait(waitTime, () => isWait = false));
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


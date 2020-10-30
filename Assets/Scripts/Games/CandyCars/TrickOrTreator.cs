using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//pick a random point between the ports
//move to that point at certain speed
//wait at point for certain time
//if hit by candy increase move speed, decrease wait time
//immediately start moving again

namespace CandyCars
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TrickOrTreator : MonoBehaviour
    {
        private Vector3 startPosition = Vector3.zero;
        public Transform left, right;
        public int level = 1;
        public int speedMax = 10;
        public int waitMax = 4;

        private void Awake()
        {
            startPosition = transform.position;
            Debug.Log("StartPos: " + startPosition);
        }
        private void OnEnable()
        {
            //don't reset levels for now
            //level = 1;
            transform.position = startPosition;
            c = StartCoroutine(Move());
        }
        private void OnDisable()
        {
            StopCoroutine(c);
        }

        public static Action COLLECTEDCANDY;

        private Vector3 GetPoint()
        {
            return Vector3.Lerp(left.position, right.position, UnityEngine.Random.Range(0f, 1f));
        }
        private float GetSpeed()
        {
            return speedMax / level;
        }
        private float GetWait()
        {
            return waitMax / level;
        }

        Coroutine c = null;
        IEnumerator Move()
        {
            Vector3 start = transform.position;
            Vector3 end = GetPoint();
            float speed = GetSpeed();
            float wait = GetWait();

            float count = 0f;
            while (count < speed)
            {
                transform.position = Vector3.Lerp(start, end, count / speed);
                yield return null;
                count += Time.deltaTime;
            }

            yield return new WaitForSeconds(wait);
            c = StartCoroutine(Move());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Candy")
            {
                COLLECTEDCANDY();
                //up level
                level++;
                //setup move again
                StopCoroutine(c);
                c = StartCoroutine(Move());
            }
        }
    }
}


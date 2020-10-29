using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyCars
{
    public class Candy : MonoBehaviour
    {
        Rigidbody2D rb;
        public float speed = 1f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rb.velocity = new Vector2(0f, speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}


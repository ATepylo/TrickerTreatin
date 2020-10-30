using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CandyCars
{
    public class Car : MonoBehaviour
    {
        Rigidbody2D rb;
        public float speed = 1f;
        public int direction = 1;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
       
        private void OnDisable()
        {
            Destroy(gameObject);
        }

        public void SetVelocity(int direction)
        {
            rb.velocity = new Vector2(direction * speed, 0f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"Tag: {collision.transform.tag}");
            if (collision.transform.tag == "Boundary")
            {
                Destroy(gameObject);
            }
        }
    }
}


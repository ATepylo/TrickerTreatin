﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CandyCars
{
    public class Car : GamePiece
    {
        Rigidbody2D rb;
        public float speed = 1f;
        public int direction = 1;

        private void OnEnable()
        {
            Timer.DONE += Remove;
        }
        private void OnDisable()
        {
            Timer.DONE -= Remove;
        }

        private void Remove()
        {
            Destroy(gameObject);
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
                Remove();
            }
        }
    }
}


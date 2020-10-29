using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CandyCars
{
    public class Player : MonoBehaviour
    {
        Rigidbody2D rb;
        public GameObject candyPrefab;
        public float speed = 5f;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0f);

            if(Input.GetKeyUp(KeyCode.Space))
            {
                GameObject candy = Instantiate(candyPrefab, transform.position + Vector3.up, Quaternion.identity);
            }
        }
    }
}


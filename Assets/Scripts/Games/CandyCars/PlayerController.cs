using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CandyCars
{
    public class PlayerController : MonoBehaviour
    {
        private RectTransform rt;
        public RectTransform sideWalk;
        public float speed = 1f;
        public float xMin, xMax;

        public GameObject candyPrefab;
        private Vector2 candySize;
        public Transform container;

        private void Start()
        {
            rt = GetComponent<RectTransform>();

            //set height same as sidwalk, set width as same
            float height = sideWalk.rect.height;
            float width = sideWalk.rect.width;
            Debug.Log($"Size: {width}, {height}");

            rt.sizeDelta = new Vector2(height, height);
            transform.localPosition = Vector3.zero;

            xMin = -width / 2f + height / 2f;
            xMax = width / 2f - height / 2f;
            Debug.Log($"xMin: {xMin}, xMax: {xMax}");

            //candy size
            candySize = new Vector2(height / 4f, height / 4f);
        }

        private void Update()
        {
            if (Input.GetAxis("Horizontal") != 0f) 
            {
                float x = Mathf.Clamp(transform.localPosition.x + Input.GetAxis("Horizontal") * speed, xMin, xMax);
                transform.localPosition = new Vector3(x, 0f, -1f);
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                //fire candy
                GameObject temp = Instantiate(candyPrefab, transform);
                temp.GetComponent<RectTransform>().sizeDelta = candySize;
                temp.GetComponent<RectTransform>().position = transform.position;
                //temp.GetComponent<Candy>().UpdateSpeed(container.GetComponent<RectTransform>().rect.height);
                temp.transform.SetParent(container);
            }
        }
    }
}


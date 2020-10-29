using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CandyCars
{
    public class CarManager : MonoBehaviour
    {
        private RectTransform rt;
        public RectTransform road;
        public float spawnPointLeft;
        public float spawnPointRight;
        public Vector3[,] points;
        public Vector3 topLeft, topRight, bottomLeft, bottomRight;
        public float driveTime = 2f;
        public GameObject prefab;


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //create car
                GameObject temp = Instantiate(prefab, road.transform);
                //set size
                temp.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                //determine path randomly
                int reverse = Random.Range(0, 2);
                int i = Random.Range(0, 2);
                if(reverse == 0)
                {
                    temp.GetComponent<CarOld>().SetDrive(points[i,0], points[i,1], driveTime);
                }
                else
                {
                    temp.GetComponent<CarOld>().SetDrive(points[i, 1], points[i, 0], driveTime);
                }
            }
        }

        public float width, height;
        private void Start()
        {
            rt = GetComponent<RectTransform>();
            width = road.rect.width / 4f;
            height = road.rect.height / 4f;
            
            spawnPointLeft = -road.rect.width / 2f - width;
            spawnPointRight = road.rect.width / 2f + width;
            points = new Vector3[2,2];
            points[0,0] = new Vector3(spawnPointLeft, height, 0f);
            points[0,1] = new Vector3(spawnPointRight, height, 0f);
            points[1,0] = new Vector3(spawnPointLeft, -height, 0f);
            points[1,1] = new Vector3(spawnPointRight, -height, 0f);
        }
    }        
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction { LEFT, RIGHT }
namespace CandyCars
{
    public class CarSpawner : MonoBehaviour
    {
        //spawn cars in the direction specified
        public float spawnTimeMin = 1f;
        public float spawnTimeMax = 1f;
        public Direction direction = Direction.RIGHT;
        public GameObject carPrefab;

        private void Start()
        {
            c = StartCoroutine(Spawn(Random.Range(spawnTimeMin, spawnTimeMax)));
        }

        Coroutine c = null;
        IEnumerator Spawn(float spawnTime)
        {
            float count = 0f;
            while(count < spawnTime)
            {
                count += Time.deltaTime;
                yield return null;
            }

            GameObject car = Instantiate(carPrefab, transform.position, Quaternion.identity);
            //flip car???
            int dir = direction == Direction.RIGHT ? 1 : -1;
            car.GetComponent<Car>().SetVelocity(dir);
            c = StartCoroutine(Spawn(Random.Range(spawnTimeMin, spawnTimeMax)));
        }
    }
}


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

        private void OnEnable()
        {
            c = StartCoroutine(Spawn(Random.Range(spawnTimeMin, spawnTimeMax)));
        }
        private void OnDisable()
        {
            StopCoroutine(c);
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

            GameObject car = Instantiate(carPrefab, transform);
            car.transform.position = transform.position;
            int dir = direction == Direction.RIGHT ? 1 : -1;
            car.GetComponent<Car>().SetVelocity(dir);
            //flip car sprite depending on direction
            car.transform.localScale = new Vector3(1, 1, dir);
            c = StartCoroutine(Spawn(Random.Range(spawnTimeMin, spawnTimeMax)));
        }
    }
}


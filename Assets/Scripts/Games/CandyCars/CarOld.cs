using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyCars
{
    public class CarOld : MonoBehaviour
    {
        private bool isDrive = false;

        public void SetDrive(Vector3 start, Vector3 end, float speed)
        {
            if (!isDrive)
            {
                isDrive = true;
                c = StartCoroutine(Drive(start, end, speed));
            }
        }

        Coroutine c = null;
        IEnumerator Drive(Vector3 start, Vector3 end, float speed)
        {
            Debug.Log("Drive Start");
            float count = 0f;
            while (count < speed)
            {
                transform.localPosition = Vector3.Lerp(start, end, count / speed);
                yield return null;
                count += Time.deltaTime;
            }

            //for now...
            Destroy(gameObject);
        }
    }
}


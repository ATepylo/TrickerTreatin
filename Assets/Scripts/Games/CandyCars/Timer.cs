using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace CandyCars
{
    public class Timer : GamePiece
    {
        public TextMeshProUGUI timerText;
        public float gameTimer = 30f;
        private float currentTimer;

        public static Action DONE;

        public override void Activate()
        {
            base.Activate();
            currentTimer = gameTimer;
            timerText.text = Mathf.RoundToInt(currentTimer).ToString();
        }

        private void Update()
        {
            if (isPlay)
            {
                currentTimer -= Time.deltaTime;
                //update UI
                timerText.text = Mathf.RoundToInt(currentTimer).ToString();

                if (currentTimer <= 0f)
                {
                    Deactivate();
                    //do something...
                    DONE();
                }
            }            
        }
    }
}


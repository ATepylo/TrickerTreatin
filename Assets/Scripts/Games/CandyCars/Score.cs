using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace CandyCars
{
    public class Score : GamePiece
    {
        public TextMeshProUGUI scoreText;
        public int currentScore = 0;

        private void IncrementScore()
        {
            currentScore++;
            scoreText.text = currentScore.ToString();
        }

        public override void Activate()
        {
            base.Activate();
            currentScore = 0;
            scoreText.text = currentScore.ToString();
        }

        private void OnEnable()
        {
            TrickOrTreator.COLLECTEDCANDY += IncrementScore;
        }
        private void OnDisable()
        {
            TrickOrTreator.COLLECTEDCANDY -= IncrementScore;
        }
    }
}


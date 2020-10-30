using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CandyCars
{
    //game manager for Candy Cars
    [RequireComponent(typeof(Timer))]
    [RequireComponent(typeof(Score))]
    public class Game_CandyCars : MonoBehaviour
    {
        public Camera camera;
        public Canvas canvas;
        public GamePiece[] pieces;

        private void Start()
        {
            GetComponent<Timer>().Activate();
            GetComponent<Score>().Activate();

            //for now debug...
            StartGame();
        }

        private void StartGame()
        {
            camera.gameObject.SetActive(true);
            canvas.gameObject.SetActive(true);
            //activate all gamePieces
            for (int i = 0; i < pieces.Length; i++)
            {                
                pieces[i]?.Activate();
            }
        }

        private void EndGame()
        {
            //stop all pieces in game from running
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].Deactivate();
            }

            //do something with score???

            //deactivate camera and canvas
        }

        private void OnEnable()
        {
            Timer.DONE += EndGame;
        }
        private void OnDisable()
        {
            Timer.DONE -= EndGame;
        }
    }
}


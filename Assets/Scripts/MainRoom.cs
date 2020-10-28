using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoom : MonoBehaviour
{
    public enum GameState { start, knock, wait, pause, end }
    public GameState currentState = GameState.start;

    [SerializeField]
    private int eggs;

    public List<GameObject> miniGames;

    //for knock state
    [SerializeField]
    private float knockTimer;
    [SerializeField]
    private float knockRandom;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case GameState.start://used 
                if (Input.GetButtonDown("Jump"))
                {
                    knockRandom = Random.Range(3, 5);
                    currentState = GameState.knock;
                }
                break;
            case GameState.knock:
                knockTimer += Time.deltaTime;
                if(knockTimer >= knockRandom)
                {
                    //play knock sound
                    currentState = GameState.wait;
                }
                break;
            case GameState.wait:

                break;
            case GameState.pause:
                break;
            case GameState.end:
                break;
        }

        if(eggs >= 3)
        {
            currentState = GameState.end;
        }
    }

    public void EggHouse()
    {
        eggs++;
    }
}

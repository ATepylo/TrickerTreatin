using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Games : MonoBehaviour
{
    public MainRoom roomScript;

    public int maxCandies;
    public int candiesSpawned;
    public int maxBags;
    public int bagsHit;
    public int misses;
    public float gameLength;
    public float gameTimer;
    public int hitsNeeded = 1;
    public Text timer;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        gameTimer -= Time.deltaTime;
        timer.text = gameTimer.ToString("F1");
        if(gameTimer <= 0)
        {
            //game over
            roomScript.AddtoKidCound(bagsHit);
            
                roomScript.EggHouse();
            
            roomScript.currentState = MainRoom.GameState.knock;
            roomScript.SetKnockTimer(0);
            roomScript.CloseDoor();
            timer.enabled = false;
            this.gameObject.SetActive(false);
            this.enabled = false;
        }
    }

    public virtual void OnEnable()
    {
        roomScript = FindObjectOfType<MainRoom>();
        misses = 0;
        gameLength = 15/roomScript.gameSpeed; //change this to use gamespeed;
        gameTimer = gameLength;
        timer.enabled = true;
    }

    public virtual void OnDisable()
    {
        StopAllCoroutines();
        timer.enabled = false;
    }
}

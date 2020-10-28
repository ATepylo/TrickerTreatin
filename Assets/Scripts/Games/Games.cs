using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        gameTimer -= Time.deltaTime;
        if(gameTimer <= 0)
        {
            print("???");
            //game over
            roomScript.EggHouse();
            this.gameObject.SetActive(false);
            this.enabled = false;
        }
    }

    public virtual void OnEnable()
    {
        roomScript = FindObjectOfType<MainRoom>();
        misses = 0;
        gameLength = 30; //change this to use gamespeed;
        gameTimer = gameLength;
    }

    public virtual void OnDisable()
    {
        StopAllCoroutines();
    }
}

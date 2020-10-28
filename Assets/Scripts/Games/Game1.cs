using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//game where you drop candy into bags whith automoving hand

public class Game1 : Games
{
    public GameObject hand;
    public enum MoveDir { left, right }
    public MoveDir currentDir = MoveDir.left;

    public GameObject candyPrefab;
    GameObject candy;

    [SerializeField]
    private float handSpeed;

    
    

   

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        switch(currentDir)
        {
            case MoveDir.left:
                if(hand.transform.position.x >= transform.position.x - 5) //replace with actual numbers once sprites come in
                {
                    float x = hand.transform.position.x;
                    x -= handSpeed * Time.deltaTime;
                    hand.transform.position = new Vector2(x, hand.transform.position.y);
                }
                else { currentDir = MoveDir.right; }
                break;
            case MoveDir.right:
                if (hand.transform.position.x <= transform.position.x + 5)
                {
                    float x = hand.transform.position.x;
                    x += handSpeed * Time.deltaTime;
                    hand.transform.position = new Vector2(x, hand.transform.position.y);
                }
                else { currentDir = MoveDir.left; }
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            if (candy)
            {
                Release();
            }
        }
        
    }

    public override void OnEnable()
    {
        base.OnEnable();
        //reset game on enable
        maxBags = Mathf.FloorToInt(roomScript.gameSpeed);
        //instatiate bags here
        maxCandies = maxBags + 1;
        handSpeed = 1 * roomScript.gameSpeed;
        candy = Instantiate(candyPrefab, hand.transform);
        candiesSpawned = 1;
    }

    public void Release()
    {
        candy.transform.parent = null;
        candy.GetComponent<Candy1>().currentState = Candy1.HoldState.released;
        candy = null;
        if(candiesSpawned < maxCandies)
        {
            StartCoroutine(CandyWait());
        }
    }

    IEnumerator CandyWait()
    {
        yield return new WaitForSeconds(1);
        NewCandy();
    }

    public void NewCandy()
    {
        candy = Instantiate(candyPrefab, hand.transform);
        candiesSpawned++;
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MatchGame : Games
{
    private string[] letters = new string[4] { "w", "a", "s", "d"};
    public List<MatchBags> matchBags = new List<MatchBags>();

    //public List<GameObject> bags = new List<GameObject>();

    public GameObject hand;
    public GameObject candyPrefab;
    public GameObject candy;

    public int candiesUsed;

    public bool canDrop;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        maxBags = Mathf.FloorToInt(roomScript.gameSpeed);
        maxCandies = maxBags + 1;
        canDrop = true;
        

        foreach (MatchBags bag in matchBags)
        {
            bag.letter = letters[Random.Range(0, letters.Length - 1)];
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //move
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            if (hit.collider.gameObject.layer == 16 || hit.collider.gameObject.layer == 11)
            {
                hand.transform.position = hit.point;
            }           
        }

        //drop candy
        if (canDrop)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (hit.collider.gameObject.layer == 11 && hit.collider.GetComponent<MatchBags>().letter == "w")
                {
                    bagsHit++;
                    hit.collider.gameObject.SetActive(false);
                }
                else
                {
                    misses++;
                }
                int count = 0;
                foreach (MatchBags obj in matchBags)
                {
                    if (obj.gameObject.activeSelf == false)
                    {
                        count++;
                    }
                }
                if (count == matchBags.Count)
                {
                    StartCoroutine(EndMiniGame());
                }
                canDrop = false;
                candy.SetActive(false);
                StartCoroutine(DropCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (hit.collider.gameObject.layer == 11 && hit.collider.GetComponent<MatchBags>().letter == "a")
                {
                    bagsHit++;
                    hit.collider.gameObject.SetActive(false);
                }
                else
                {
                    misses++;
                }
                int count = 0;
                foreach (MatchBags obj in matchBags)
                {
                    if (obj.gameObject.activeSelf == false)
                    {
                        count++;
                    }
                }
                if (count == matchBags.Count)
                {
                    StartCoroutine(EndMiniGame());
                }
                canDrop = false;
                candy.SetActive(false);
                StartCoroutine(DropCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (hit.collider.gameObject.layer == 11 && hit.collider.GetComponent<MatchBags>().letter == "s")
                {
                    bagsHit++;
                    hit.collider.gameObject.SetActive(false);
                }
                else
                {
                    misses++;
                }
                int count = 0;
                foreach (MatchBags obj in matchBags)
                {
                    if (obj.gameObject.activeSelf == false)
                    {
                        count++;
                    }
                }
                if (count == matchBags.Count)
                {
                    StartCoroutine(EndMiniGame());
                }
                canDrop = false;
                candy.SetActive(false);
                StartCoroutine(DropCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (hit.collider.gameObject.layer == 11 && hit.collider.GetComponent<MatchBags>().letter == "d")
                {
                    bagsHit++;
                    hit.collider.gameObject.SetActive(false);
                }
                else
                {
                    misses++;
                }
                int count = 0;
                foreach (MatchBags obj in matchBags)
                {
                    if (obj.gameObject.activeSelf == false)
                    {
                        count++;
                    }
                }
                if (count == matchBags.Count)
                {
                    StartCoroutine(EndMiniGame());
                }
                canDrop = false;
                candy.SetActive(false);
                StartCoroutine(DropCoolDown());
            }
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    IEnumerator DropCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        canDrop = true;
        candy.SetActive(true);
    }

    IEnumerator EndMiniGame()
    {
        yield return new WaitForSeconds(1);
        if (bagsHit < maxBags)
        {
            roomScript.EggHouse();
        }

        roomScript.AddtoKidCound(bagsHit);
        roomScript.currentState = MainRoom.GameState.knock;
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
}
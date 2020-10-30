using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MatchGame : Games
{
    private string[] letters = new string[4] { "w", "a", "s", "d"};
    public List<MatchBags> matchBags = new List<MatchBags>();

    //public List<GameObject> bags = new List<GameObject>();

    public GameObject hand;
    public Transform startpos;
    public GameObject candyPrefab;
    public GameObject candy;

    public int candiesUsed;

    public bool canDrop;

    private AudioSource src;
    public AudioClip plop;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        maxBags = Mathf.FloorToInt(roomScript.gameSpeed);
        int count = 0;
        foreach (MatchBags bag in matchBags)
        {
            bag.letter = letters[Random.Range(0, letters.Length - 1)];
            //bag.letterText.enabled = false;
            bag.SetLetter();
        }
        for (int i = 0; i < maxBags; i++)
        {
            matchBags[i].gameObject.SetActive(true);
            matchBags[i].letterText.enabled = true;
            count++;
        }
        for (int i = count; i < matchBags.Count; i++)
        {
            matchBags[i].gameObject.SetActive(false);
            matchBags[i].letterText.enabled = false;
        }

        maxCandies = maxBags + 1;
        canDrop = true;
        hand.transform.position = startpos.position;

        

        src = GetComponent<AudioSource>();
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
                RaycastHit2D candyHit = Physics2D.Raycast(candy.transform.position, Vector3.forward);
                if (candyHit.collider.gameObject.layer == 11 && candyHit.collider.GetComponent<MatchBags>().letter == "w")
                {
                    bagsHit++;
                    src.PlayOneShot(plop);
                    candyHit.collider.gameObject.SetActive(false);
                    candyHit.collider.gameObject.GetComponent<MatchBags>().letterText.enabled = false;
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
                RaycastHit2D candyHit = Physics2D.Raycast(candy.transform.position, Vector3.forward);
                if (candyHit.collider.gameObject.layer == 11 && candyHit.collider.GetComponent<MatchBags>().letter == "a")
                {
                    bagsHit++;
                    src.PlayOneShot(plop);
                    candyHit.collider.gameObject.SetActive(false);
                    candyHit.collider.gameObject.GetComponent<MatchBags>().letterText.enabled = false;
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
                RaycastHit2D candyHit = Physics2D.Raycast(candy.transform.position, Vector3.forward);

                if (candyHit.collider.gameObject.layer == 11 && candyHit.collider.GetComponent<MatchBags>().letter == "s")
                {
                    bagsHit++;
                    src.PlayOneShot(plop);
                    candyHit.collider.gameObject.SetActive(false);
                    candyHit.collider.gameObject.GetComponent<MatchBags>().letterText.enabled = false;
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
                RaycastHit2D candyHit = Physics2D.Raycast(candy.transform.position, Vector3.forward);

                if (candyHit.collider.gameObject.layer == 11 && candyHit.collider.GetComponent<MatchBags>().letter == "d")
                {
                    bagsHit++;
                    src.PlayOneShot(plop);
                    candyHit.collider.gameObject.SetActive(false);
                    candyHit.collider.gameObject.GetComponent<MatchBags>().letterText.enabled = false;
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
        roomScript.SetKnockRandom(Random.Range(3, 5));
        roomScript.currentState = MainRoom.GameState.knock;
        roomScript.SetKnockTimer(0);
        roomScript.CloseDoor();
        foreach(MatchBags bag in matchBags)
        {
            bag.letterText.enabled = false;
        }
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
}

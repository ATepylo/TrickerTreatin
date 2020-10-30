using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatGame : Games
{
    public GameObject bagPrefab;
    public List<GameObject> bags = new List<GameObject>();
    public GameObject kidHandPrefab;
    public List<GameObject> kidHands = new List<GameObject>();

    public GameObject leftHand;
    [SerializeField]
    private float leftHandSpeed;
    public GameObject rightHand;
    public GameObject candyBowl;
    public GameObject candy;

    public Transform RHstart;
    public Transform LHstart;

    public bool canDrop;

    private float horizontal;
    private float vertical;

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
        //setup bags
        for (int i = 0; i < maxBags; i++)
        {
            bags[i].SetActive(true);
            kidHands[i].SetActive(true);
            count++;
        }
        for (int i = count; i < bags.Count; i++)
        {
            bags[i].SetActive(false);
            kidHands[i].SetActive(false);
        }
        

        maxCandies = maxBags + 1;
        canDrop = true;
        leftHandSpeed = 5;
        leftHand.transform.position = LHstart.position;
        rightHand.transform.position = RHstart.position;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        //update position of the left hand
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        float x = leftHand.transform.position.x + horizontal * leftHandSpeed * Time.deltaTime;
        x = Mathf.Clamp(x, transform.position.x + 2, transform.position.x + 18); //update these numbers once we have sprites
        float y = leftHand.transform.position.y + vertical * leftHandSpeed * Time.deltaTime;
        y = Mathf.Clamp(y, transform.position.y - 11, transform.position.y + 11);
        leftHand.transform.position = new Vector2(x, y);

        //drop candy
        if (canDrop)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //print("try raycast");
                RaycastHit2D candyHit = Physics2D.Raycast(leftHand.transform.position, Vector3.forward);
                if (candyHit.collider)
                {
                    if (candyHit.collider.gameObject.layer == 11)
                    {
                        print("hit bag");
                        //add score and remove bag
                        bagsHit++;
                        candyHit.collider.gameObject.SetActive(false);
                        int i = 0;
                        int count = 0;
                        for (int j = 0; j < bags.Count; j++)
                        {
                            if (bags[j] == candyHit.collider.gameObject)
                            {
                                i = j;
                                count++;
                            }
                        }
                        kidHands[i].SetActive(false);
                        //check if game has ended
                        //foreach (GameObject obj in bags)
                        //{
                        //    if (obj.activeSelf == false)
                        //    {
                        //        count++;
                        //    }
                        //}
                        if (count == maxBags)
                        {
                            StartCoroutine(EndMiniGame());
                        }
                    }
                }
                canDrop = false;
                candy.SetActive(false);
                StartCoroutine(DropCoolDown());
            }
        }

        //update position of right hand
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            if (hit.collider.gameObject.layer == 16)
            {
                rightHand.transform.position = hit.point;
            }
        } 
        
        //swat hands
        if(Input.GetMouseButtonDown(0))
        {
            if(hit.collider.gameObject.layer == 17)
            {
                hit.collider.GetComponent<SwatGameHands>().SetUpHands();
            }
        }
    }

    public void DisableHands(GameObject hand)
    {
        int i = 0;
        for(int j = 0; j < kidHands.Count; j++)
        {
            if(kidHands[j] == hand)
            {
                i = j;
            }
        }
        hand.SetActive(false);
        bags[i].SetActive(false);
        //check if game has ended
        int count = 0;
        foreach(GameObject obj in kidHands)
        {
            if(obj.activeSelf == false)
            {
                count++;
            }
        }
        if(count == kidHands.Count)
        {
            StartCoroutine(EndMiniGame());
        }
    }

    IEnumerator EndMiniGame()
    {
        yield return new WaitForSeconds(1);
        if(bagsHit < maxBags)
        {
            roomScript.EggHouse();
        }

        roomScript.AddtoKidCound(bagsHit);
        roomScript.currentState = MainRoom.GameState.knock;
        roomScript.SetKnockTimer(0);
        roomScript.CloseDoor();
        this.gameObject.SetActive(false);
        this.enabled = false;
    }

    IEnumerator DropCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        canDrop = true;
        candy.SetActive(true);
    }

}

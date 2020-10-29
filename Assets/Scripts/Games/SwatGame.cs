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
        //instatiate bags here, once we have sprites
        //for(int i = 0; 1 < maxBags; i++)
        //{
        //    Instantiate()
        //}
        maxCandies = maxBags + 1;

        leftHandSpeed = 5;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        //update position of the left hand
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        float x = leftHand.transform.position.x + horizontal * leftHandSpeed * Time.deltaTime;
        x = Mathf.Clamp(x, transform.position.x - 5, transform.position.x + 5); //update these numbers once we have sprites
        float y = leftHand.transform.position.y + vertical * leftHandSpeed * Time.deltaTime;
        y = Mathf.Clamp(y, transform.position.y - 3, transform.position.y + 3);
        leftHand.transform.position = new Vector2(x, y);

        //drop candy
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("try raycast");
            RaycastHit2D candyHit = Physics2D.Raycast(leftHand.transform.position, Vector3.forward);
            if (candyHit.collider)
            {
                if (candyHit.collider.gameObject.layer == 11)
                {
                    print("hit bag");
                    //add score and remove bag
                }
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


    
}

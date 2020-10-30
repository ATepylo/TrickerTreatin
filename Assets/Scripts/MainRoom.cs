using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoom : MonoBehaviour
{
    public enum GameState { start, knock, wait, play, end }
    public GameState currentState = GameState.start;

    public GameObject closedDoor;

    [SerializeField]
    private int eggs;

    public List<GameObject> miniGames;

    [SerializeField]
    private float gameTimer;

    public float gameSpeed;

    [SerializeField]
    private int kidsWCandy;
    public void AddtoKidCound(int i)
    {
        kidsWCandy += i;
    }

    private AudioSource src;
    public AudioClip knock;
    public AudioClip splat;
    public AudioClip creak;
    public AudioClip close;

    //for knock state
    [SerializeField]
    private float knockTimer;
    public void SetKnockTimer(float f)
    {
        knockTimer = f;
    }
    [SerializeField]
    private float knockRandom;
    public void SetKnockRandom(float f)
    {
        knockRandom = Random.Range(2, 5);
    }

    //for wait time
    [SerializeField]
    private float waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameSpeed = 1;
        knockTimer = 0;
        waitTimer = 0;
        kidsWCandy = 0;
        gameTimer = 0;
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case GameState.start://used 
                StartState();
                break;
            case GameState.knock:
                KnockState();
                break;
            case GameState.wait:
                WaitState();
                break;
            case GameState.play:
                break;
            case GameState.end:
                break;
        }

        if(eggs >= 3)
        {
            currentState = GameState.end;
        }

        gameTimer += Time.deltaTime;
        if(gameTimer > 150)
        {
            gameSpeed = 3;
        }
        else if(gameTimer > 75)
        {
            gameSpeed = 2;
        }
        else { gameSpeed = 1; }

    }

    public void EggHouse()
    {
        src.PlayOneShot(splat);
        eggs++;
    }


    private void StartState()
    {
        if (Input.GetButtonDown("Jump"))
        {
            knockRandom = Random.Range(3, 5);
            currentState = GameState.knock;
        }
    }

    private void KnockState()
    {
        knockTimer += Time.deltaTime;


        if (knockTimer >= knockRandom)
        {
            src.PlayOneShot(knock);
            waitTimer = 0;
            currentState = GameState.wait;
        }
    }

    private void WaitState()
    {
        waitTimer += Time.deltaTime;
        if(Input.GetMouseButtonDown(0))
        {
            //opend door animation
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if(hit.collider.gameObject.layer == 9)
            {
                //print("start game");
                //start a random game
                src.PlayOneShot(creak);
                //play door open anim
                StartCoroutine(GameStartDelay());
            }
        }

        if(waitTimer >= (10 / gameSpeed))
        {
            EggHouse();
            knockTimer = 0;
            knockRandom = Random.Range(3, 5);
            currentState = GameState.knock;
        }
    }

    public void CloseDoor()
    {
        //close door animation
        src.PlayOneShot(close);
    }

    IEnumerator GameStartDelay()
    {
        yield return new WaitForSeconds(1);
        GameObject game = miniGames[Random.Range(0, miniGames.Count)];
        game.SetActive(true);
        game.GetComponent<Games>().enabled = true;
        currentState = GameState.play;
    }

    //used to tell kyles characters what face to make
    public void BroadCastMiss()
    {

    }
}

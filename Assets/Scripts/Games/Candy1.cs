using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy1 : MonoBehaviour
{
    public enum HoldState { held, released }
    public HoldState currentState = HoldState.held;

    [SerializeField]
    private float fallSpeed;

    private Game1 game;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        fallSpeed = 10;
        game = GetComponentInParent<Game1>();
    }

    // Update is called once per frame
    public void Update()
    {
        switch(currentState)
        {
            case HoldState.held:
                break;
            case HoldState.released:
                float y = transform.position.y;
                y -= fallSpeed * Time.deltaTime;
                transform.position = new Vector2(transform.position.x, y);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 11 && currentState == HoldState.released)
        {
            game.ScoreCandy();
            //collision.gameObject.GetComponent<Bags>().RemoveBag();
            collision.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.layer == 12 && currentState == HoldState.released)
        {
            if(gameObject.name == "lastCandy")
            {
                game.GameOver();
            }
            Destroy(this.gameObject);
        }
    }
   

}

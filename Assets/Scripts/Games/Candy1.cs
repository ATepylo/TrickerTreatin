using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy1 : MonoBehaviour
{
    public enum HoldState { held, released }
    public HoldState currentState = HoldState.held;

    [SerializeField]
    private float fallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        fallSpeed = 1;
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

            collision.gameObject.GetComponent<Bags>().RemoveBag();
            Destroy(this);
        }
        else if(collision.gameObject.layer == 12 && currentState == HoldState.released)
        {
            Destroy(this);
        }
    }
   

}

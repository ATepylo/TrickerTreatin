using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatGameHands : MonoBehaviour
{
    public enum HandState { move, wait, comeBack}
    public HandState currentState = HandState.wait;

    public GameObject candyBowl;
    public GameObject candy;
    public Vector2 startPos;
    
    public MainRoom room; 
    [SerializeField]
    private float handSpeed;

    private SwatGame game;

    // Start is called before the first frame update
    void OnEnable()
    {        
        startPos = transform.position;
        SetUpHands();
        room = FindObjectOfType<MainRoom>();
        handSpeed = 2 + room.gameSpeed;
        game = GetComponentInParent<SwatGame>();
        
    }

    public void SetUpHands()
    {
        transform.position = startPos;
        currentState = HandState.wait;
        candy.SetActive(false);
        StartCoroutine(WaitTimer());        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case HandState.move:
                transform.position = Vector3.MoveTowards(transform.position, candyBowl.transform.position, handSpeed * Time.deltaTime);
                if(Vector3.Distance(transform.position, candyBowl.transform.position) <= 0.5)
                {
                    candy.SetActive(true);
                    currentState = HandState.comeBack;
                }
                break;
            case HandState.wait:
                break;
            case HandState.comeBack:
                transform.position = Vector3.MoveTowards(transform.position, startPos, handSpeed * Time.deltaTime);
                if(Vector3.Distance(transform.position, startPos) <= 0.2f && candy.activeSelf)
                {
                    game.DisableHands(this.gameObject);
                }
                break;
        }
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(2);
        currentState = HandState.move;
    }

   
}

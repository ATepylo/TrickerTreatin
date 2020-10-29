using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatGameHands : MonoBehaviour
{
    public enum HandState { move, wait}
    public HandState currentState = HandState.wait;

    public GameObject candyBowl;
    public Vector2 startPos;

    public MainRoom room; 
    [SerializeField]
    private float handSpeed;

    // Start is called before the first frame update
    void OnEnable()
    {
        startPos = transform.position;
        SetUpHands();
        room = FindObjectOfType<MainRoom>();
        handSpeed = 1 + room.gameSpeed;
    }

    public void SetUpHands()
    {
        transform.position = startPos;
        currentState = HandState.wait;
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
                    //get rid of hand and bag
                }
                break;
            case HandState.wait:
                break;
        }
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(2);
        currentState = HandState.move;
    }
}

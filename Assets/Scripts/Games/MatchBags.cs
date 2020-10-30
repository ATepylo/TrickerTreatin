using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchBags : MonoBehaviour
{
    public string letter;
    public List<Sprite> leterSprites;
    public Text letterText;

    // Start is called before the first frame update
    void Start()
    {
        //letterText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLetter()
    {
        letterText.enabled = true;
        letterText.text = letter;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCars;

public class CandyCarsGame : Games
{
    public override void OnEnable()
    {
        base.OnEnable();
        TrickOrTreator.COLLECTEDCANDY += Point;
        hitsNeeded = 3;
    }

    public override void OnDisable()
    {
        //base.OnDisable();
        Debug.Log($"<color=red>Final Score: {bagsHit}</color>");
        TrickOrTreator.COLLECTEDCANDY -= Point;
        roomScript.currentState = MainRoom.GameState.knock;
        roomScript.SetKnockTimer(0);
        roomScript.CloseDoor();
    }

    private void Point()
    {
        bagsHit++;
        Debug.Log($"Score: {bagsHit}");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnArround : Master
{
    public void TurnClockWise()
    {
        List<Vector2Int> hexagonStatuses = gameManager.dedector.selectedObject;
        gameManager.ReadyForTurnLeft(gameManager.SortToBigArray(hexagonStatuses));

    }

    
}

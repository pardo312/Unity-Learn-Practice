using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    protected State currentState;
    protected State previousState;

    public void SetState(State state){
        if(currentState!=null)
            previousState = currentState;

        currentState = state;
        StartCoroutine(currentState.Start());
    }
}

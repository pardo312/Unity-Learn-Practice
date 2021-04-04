using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : StateManager
{
    [SerializeField] public UIManagerState UIManager;
    private void Start() {
        SetState(new Begin(this));
    }
    
    public void Attack(){
        StartCoroutine(currentState.Attack());
    }

    public void Heal(){
        StartCoroutine(currentState.Heal());
    }
}

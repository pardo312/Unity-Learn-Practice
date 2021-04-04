using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected BattleSystem battleSystem;

    public State(BattleSystem paramBattleSystem) {
        battleSystem = paramBattleSystem;
    }

    public virtual IEnumerator Start(){
        yield break;
    }

    public virtual IEnumerator Attack(){
        yield break;
    }

    public virtual IEnumerator Heal(){
        yield break;
    }

}

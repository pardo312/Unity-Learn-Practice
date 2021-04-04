using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : State
{
    public End(BattleSystem paramBattleSystem) : base(paramBattleSystem) {
    }

    public override IEnumerator Start() {
        //Logic on begin
        battleSystem.UIManager.changeTextOfUIElement("StateValue", "End");
        yield return showEndText();
        yield break; 
    }
    private IEnumerator showEndText(){
        yield return new WaitForSecondsRealtime(2f);
        battleSystem.SetState(new PlayerTurn(battleSystem));
        battleSystem.UIManager.changeTextOfUIElement("InfoText", "Game Ended");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public Begin(BattleSystem paramBattleSystem) : base(paramBattleSystem) {}

    public override IEnumerator Start() {
        //Logic on begin
        battleSystem.UIManager.changeTextOfUIElement("StateValue", "Begin");
        yield return showInitText();
        yield break; 
    }
    private IEnumerator showInitText(){
        battleSystem.UIManager.changeTextOfUIElement("InfoText", "A new foe has appeared!!");
        yield return new WaitForSeconds(2f);
        battleSystem.SetState(new PlayerTurn(battleSystem));

    }
}

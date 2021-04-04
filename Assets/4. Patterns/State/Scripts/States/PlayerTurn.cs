using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PlayerTurn : State
{
    public PlayerTurn(BattleSystem paramBattleSystem) : base(paramBattleSystem) {}

    public override IEnumerator Start() {
        battleSystem.UIManager.changeTextOfUIElement("StateValue", "PlayerTurn");
        battleSystem.UIManager.changeTextOfUIElement("InfoText", "What are you going to do: ");
        yield break;
    }
    public override IEnumerator Attack(){
        TextMeshProUGUI enemyLife;        
        int enemyLifeInt;
        if(battleSystem.UIManager.GetUITextElement("EnemyLife",out enemyLife))
            if(int.TryParse(enemyLife.text, out enemyLifeInt)){
                if(enemyLifeInt > 0){
                    enemyLifeInt-=Random.Range(5,20);
                    battleSystem.UIManager.changeTextOfUIElement("EnemyLife", "" + enemyLifeInt);
                    if(enemyLifeInt <= 0){
                        battleSystem.UIManager.changeTextOfUIElement("InfoText", "Player Won :D !!!");
                        battleSystem.SetState(new End(battleSystem));
                        yield break;
                    }
                }
                battleSystem.SetState(new EnemyTurn(battleSystem));
            }
        yield break;
    }
}

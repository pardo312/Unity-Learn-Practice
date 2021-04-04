using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyTurn : State
{
    public EnemyTurn(BattleSystem paramBattleSystem) : base(paramBattleSystem) {}

    public override IEnumerator Start() {
        battleSystem.UIManager.changeTextOfUIElement("StateValue", "EnemyTurn");
        battleSystem.UIManager.changeTextOfUIElement("InfoText", "Enemy attacking...");
        yield return new WaitForSecondsRealtime(2f);
        yield return Attack();
        yield break;
    }
    public override IEnumerator Attack(){
        TextMeshProUGUI playerLife;        
        int playerLifeInt;
        if(battleSystem.UIManager.GetUITextElement("PlayerLife",out playerLife))
            if(int.TryParse(playerLife.text, out playerLifeInt)){
                if(playerLifeInt > 0){
                    playerLifeInt-=Random.Range(5,20);
                    battleSystem.UIManager.changeTextOfUIElement("PlayerLife", "" + playerLifeInt);
                    if(playerLifeInt <= 0){
                        battleSystem.UIManager.changeTextOfUIElement("InfoText", "Enemy Won :C !");
                        battleSystem.SetState(new End(battleSystem));
                        yield break;
                    }
                }
                battleSystem.SetState(new PlayerTurn(battleSystem));
            }
        yield break;
    }
}

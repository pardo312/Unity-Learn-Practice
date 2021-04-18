using UnityEngine;
using TMPro;

public class AbilityButton : MonoBehaviour {
    private string ability;

    public void SetAbilityName(string paramAbility){
        ability = paramAbility;
        transform.GetChild(0).GetComponent<TMP_Text>().text = name;
    }
    public void ExecuteAbility(){
        AbilityFactory.instance.GetAbility(ability).Process();
    }
}
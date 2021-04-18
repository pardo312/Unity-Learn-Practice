using UnityEngine;

public class ButtonPanel : MonoBehaviour {
    public AbilityButton buttonPrefab;
    private void OnEnable(){
        foreach (string name in AbilityFactory.instance.GetAbilityNames())
        {
            var button = Instantiate(buttonPrefab,this.transform);
            button.gameObject.name = name + " Button";
            button.SetAbilityName(name);
        }
    }
}
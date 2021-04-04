using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using Unity.Collections;

public class UIManagerState : MonoBehaviour {
    public GameObject[] UIGameObjects;


    public void changeTextOfUIElement(string nameOfUIGameObject, string newText){
        TextMeshProUGUI UIElement;
        if(GetUITextElement(nameOfUIGameObject,out UIElement)){
            UIElement.text = newText;
        }
    }
    public bool GetUITextElement(string name,out TextMeshProUGUI foundTextUIElement ) 
    {
        foundTextUIElement = null;
        foreach (GameObject gameObject in UIGameObjects)
        {
            if(gameObject.name.Equals(name))
                if(gameObject.TryGetComponent<TextMeshProUGUI>(out foundTextUIElement)){
                    return true;

                }
        }
        return false;
    }
    
    private bool GetUIElement(string name,out GameObject foundGameObject ) 
    {
        foundGameObject = null;
        foreach (GameObject gameObject in UIGameObjects)
        {
            if(gameObject.name.Equals(name)){
                foundGameObject = gameObject;
                return true;
            }
        }
        return false;
    }
}
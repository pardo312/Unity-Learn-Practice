using UnityEngine;

public class Cube : MonoBehaviour {
    private void Update() {
        if(this.transform.localScale.x > 10){
            this.gameObject.SetActive(false);
        }
        this.transform.localScale += new Vector3(0.01f,0.01f,0.01f);
    }
}
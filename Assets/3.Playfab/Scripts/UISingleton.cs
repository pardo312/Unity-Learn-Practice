using UnityEngine;

public class UISingleton : MonoBehaviour {
    #region Singleton

    public static UISingleton instance;

    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        IntializeUI();
    }

    #endregion

    [HideInInspector] public GameObject parentCanvas;
    [HideInInspector] public GameObject loginPanel;
    [HideInInspector] public GameObject logoutButton;
    [HideInInspector] public GameObject linkMobileToAccountPanel;
    [HideInInspector] public GameObject linkMobileToAccountButton;
    [HideInInspector] public GameObject addFriendPanel;

    void IntializeUI() {
        parentCanvas = GameObject.Find("Canvas");
        loginPanel = parentCanvas.transform.Find("LoginPanel").gameObject;
        logoutButton = parentCanvas.transform.Find("LogOut").gameObject;
        linkMobileToAccountPanel = parentCanvas.transform.Find("AddAndroidLoginPanel").gameObject;
        linkMobileToAccountButton = parentCanvas.transform.Find("ButtonMobileToAccountButton").gameObject;
        addFriendPanel = parentCanvas.transform.Find("AddFriendPanel").gameObject;
    }
}

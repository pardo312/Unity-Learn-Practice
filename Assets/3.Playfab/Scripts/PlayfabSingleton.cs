using UnityEngine;
using System;
using PlayFab.ClientModels;
using PlayFab;

public class PlayfabSingleton : MonoBehaviour {

    #region Singleton

    public static PlayfabSingleton instance;

    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    #endregion

    [HideInInspector] public string playFabID = "";
    public string displayName = ""; 
    [HideInInspector] public string userName = "";
    [HideInInspector] public string userEmail = ""; 
    [HideInInspector] public string userPassword = ""; 
    public event Action LoginSuccess;

    public void OnLoginSuccess(){
        if(LoginSuccess!=null){

            LoginSuccess.Invoke();
            UpdateDisplayName();
        }
    }
    private void UpdateDisplayName(){
        if(displayName!= ""){
            var requestUpdateDisplayName = new UpdateUserTitleDisplayNameRequest{DisplayName = displayName};
            PlayFabClientAPI.UpdateUserTitleDisplayName(requestUpdateDisplayName, OnDisplayNameSuccess, LogPlayFabError);
        }
        else{
            var requestUpdateDisplayName = new UpdateUserTitleDisplayNameRequest{DisplayName = userName};
            PlayFabClientAPI.UpdateUserTitleDisplayName(requestUpdateDisplayName, OnDisplayNameSuccess, LogPlayFabError);
        }


    }
    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result){
        Debug.Log("Display Name: " + result.DisplayName);
    }

    private void LogPlayFabError(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }

    #region SetUserData

    public void SetUserEmail(string emailIn) {
        userEmail = emailIn;
    }

    public void SetUserPassword(string passwordIn) {
        userPassword = passwordIn;
    }

    public void SetUsername(string usernameIn) {
        userName = usernameIn;
    }
    
    #endregion

}
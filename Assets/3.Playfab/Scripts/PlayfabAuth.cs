using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabAuth : MonoBehaviour {
    private PlayfabMobileAuth playFabMobileAuth;
    private PlayerStats playerStats;

    private void Awake() {
        //Make Sure these are on the playfabGO
        playFabMobileAuth = GetComponent<PlayfabMobileAuth>();
        playerStats = GetComponent<PlayerStats>();
    }

    public void Start() {
        // PlayerPrefs.DeleteAll();
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) {
            PlayFabSettings.TitleId = "FDAC4";
        }

        if (PlayerPrefs.HasKey("EMAIL")) {
            PlayfabSingleton.instance.userEmail = PlayerPrefs.GetString("EMAIL");
            PlayfabSingleton.instance.userPassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        } else {
            //MobileLogin
            playFabMobileAuth.MobileAuthentication();
        }
    }

    #region LoginRegisterCallbacks

    private void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        Debug.Log("Register Success");
        PlayerPrefs.SetString("EMAIL", PlayfabSingleton.instance.userEmail);
        PlayerPrefs.SetString("PASSWORD", PlayfabSingleton.instance.userPassword);

        PlayfabSingleton.instance.playFabID = result.PlayFabId;
        PlayfabSingleton.instance.OnLoginSuccess();

        UISingleton.instance.loginPanel.SetActive(false);
        UISingleton.instance.logoutButton.SetActive(true);
    }

    private void OnLoginSuccess(LoginResult result) {
        Debug.Log("Login Success");
        PlayerPrefs.SetString("EMAIL", PlayfabSingleton.instance.userEmail);
        PlayerPrefs.SetString("PASSWORD", PlayfabSingleton.instance.userPassword);

        PlayfabSingleton.instance.playFabID = result.PlayFabId;
        UISingleton.instance.loginPanel.SetActive(false);
        UISingleton.instance.logoutButton.SetActive(true);
        UISingleton.instance.linkMobileToAccountButton.SetActive(false);
    }

    private void OnLoginFailure(PlayFabError error) {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = PlayfabSingleton.instance.userEmail,
            Password = PlayfabSingleton.instance.userPassword,
            Username = PlayfabSingleton.instance.userName
        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, LogPlayFabError);
    }

    private void LogPlayFabError(PlayFabError error){
        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion

    #region OnClickMethods
    public void OnClickLogin() {
        var request = new LoginWithEmailAddressRequest
        {
            Email = PlayfabSingleton.instance.userEmail,
            Password = PlayfabSingleton.instance.userPassword
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void OnClickLogOut() {
        PlayerPrefs.DeleteAll();
        UISingleton.instance.loginPanel.SetActive(true);
        UISingleton.instance.logoutButton.SetActive(false);
    }
    #endregion
}

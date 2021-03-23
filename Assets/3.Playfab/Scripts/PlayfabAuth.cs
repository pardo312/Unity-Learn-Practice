using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabAuth : MonoBehaviour {
    private string userEmail;
    private string userPassword;
    private string username;
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
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
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
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        UISingleton.instance.loginPanel.SetActive(false);
        UISingleton.instance.logoutButton.SetActive(true);
    }

    private void OnRegisterFailure(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnLoginSuccess(LoginResult result) {
        Debug.Log("Login Success");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);

        UISingleton.instance.loginPanel.SetActive(false);
        UISingleton.instance.logoutButton.SetActive(true);
        UISingleton.instance.linkMobileToAccountButton.SetActive(false);
    }

    private void OnLoginFailure(PlayFabError error) {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = userEmail,
            Password = userPassword,
            Username = username
        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    #endregion

    #region OnClickMethods
    public void OnClickLogin() {
        var request = new LoginWithEmailAddressRequest
        {
            Email = userEmail,
            Password = userPassword
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void OnClickLogOut() {
        PlayerPrefs.DeleteAll();
        UISingleton.instance.loginPanel.SetActive(true);
        UISingleton.instance.logoutButton.SetActive(false);
    }
    #endregion

    #region GetUserData
    public void GetUserEmail(string emailIn) {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn) {
        userPassword = passwordIn;
    }

    public void GetUsername(string usernameIn) {
        username = usernameIn;
    }
    #endregion

}

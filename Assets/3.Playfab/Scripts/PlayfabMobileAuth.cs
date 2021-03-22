using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabMobileAuth : MonoBehaviour {
    private string userEmailMobile;
    private string userPasswordMobile;
    private string usernameMobile;

    public void MobileAuthentication() {

#if UNITY_ANDROID
        var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
        PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginSuccessMobile, OnLoginFailureMobile);
#endif

#if UNITY_IOS
            var requestIOS = new LoginWithAndroidDeviceIDRequest { DeviceId = ReturnMobileID(), CreateAccount = true};  
            PlayFabClientAPI.loginWithIOSDeviceID(requestIOS, OnLoginSuccessMobile, OnLoginFailureMobile)
#endif
    }

    public static string ReturnMobileID() {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }

    #region Authentication

    private void OnLoginSuccessMobile(LoginResult result) {
        Debug.Log("Login Success Mobile");
        UISingleton.instance.loginPanel.SetActive(false);
    }
    
    private void OnLinkAccountSuccess(AddUsernamePasswordResult result) {
        Debug.Log("Account Linked Succesfull!");
        PlayerPrefs.SetString("EMAIL", userEmailMobile);
        PlayerPrefs.SetString("PASSWORD", userPasswordMobile);
        UISingleton.instance.loginPanel.SetActive(false);
        UISingleton.instance.logoutButton.SetActive(true);
    }

    private void OnLoginFailureMobile(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnRegisterFailureMobile(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion

    #region OnClick

    public void OnClickLinkAccountButton() {
        var LinkAccountRequest = new AddUsernamePasswordRequest
        {
            Email = userEmailMobile,
            Password = userPasswordMobile,
            Username = usernameMobile
        };
        PlayFabClientAPI.AddUsernamePassword(LinkAccountRequest, OnLinkAccountSuccess, OnRegisterFailureMobile);
    }

    public void OpenLinkAccountPanel() {
        //Faltaria boton de devolverse en caso de que no quiera linkear la cuenta
        UISingleton.instance.logoutButton.SetActive(false);
        UISingleton.instance.linkMobileToAccountPanel.SetActive(true);
    }
    
    #endregion

    #region SetUserData

    public void SetUserEmailMobile(string emailIn) {
        userEmailMobile = emailIn;
    }

    public void SetUserPasswordMobile(string passwordIn) {
        userPasswordMobile = passwordIn;
    }

    public void SetUsernameMobile(string usernameIn) {
        usernameMobile = usernameIn;
    }
    
    #endregion
}

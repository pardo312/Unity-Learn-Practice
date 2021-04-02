using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabMobileAuth : MonoBehaviour {
    public void MobileAuthentication() {

#if UNITY_ANDROID
        var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
        PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginSuccessMobile, LogPlayFabError);
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
        PlayfabSingleton.instance.playFabID = result.PlayFabId;
        UISingleton.instance.loginPanel.SetActive(false);
    }
    
    private void OnLinkAccountSuccess(AddUsernamePasswordResult result) {
        Debug.Log("Account Linked Succesfull!");
        PlayerPrefs.SetString("EMAIL", PlayfabSingleton.instance.userEmail);
        PlayerPrefs.SetString("PASSWORD", PlayfabSingleton.instance.userPassword);
        
        PlayfabSingleton.instance.OnLoginSuccess();

        UISingleton.instance.loginPanel.SetActive(false);
        UISingleton.instance.logoutButton.SetActive(true);
    }

    private void LogPlayFabError(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion

    #region OnClick

    public void OnClickLinkAccountButton() {
        var LinkAccountRequest = new AddUsernamePasswordRequest
        {
            Email = PlayfabSingleton.instance.userEmail,
            Password = PlayfabSingleton.instance.userPassword,
            Username = PlayfabSingleton.instance.userName
        };
        PlayFabClientAPI.AddUsernamePassword(LinkAccountRequest, OnLinkAccountSuccess, LogPlayFabError);
    }

    public void OpenLinkAccountPanel() {
        //Faltaria boton de devolverse en caso de que no quiera linkear la cuenta
        UISingleton.instance.logoutButton.SetActive(false);
        UISingleton.instance.linkMobileToAccountPanel.SetActive(true);
    }
    
    #endregion
}

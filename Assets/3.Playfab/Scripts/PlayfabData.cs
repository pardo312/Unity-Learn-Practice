using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayfabData : MonoBehaviour {
    private void Start() {
        PlayfabSingleton.instance.LoginSuccess += GetUserData;
    }
    private void OnDisable() {
        PlayfabSingleton.instance.LoginSuccess -= GetUserData;               
    }
    public static void SetUserData() {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
            Data = new Dictionary<string, string>() {
                {"InventorySize", "5"},
                {"Inventory0", "Sword"},
                {"Inventory1", "Shield"},
                {"Inventory2", "Bow"},
                {"Inventory3", "Spell"},
                {"Inventory4", "Potion"},
            }
        },
        result => Debug.Log("Successfully updated user data"),
        error => {
            Debug.Log("Error setting player inventory");
            Debug.Log(error.GenerateErrorReport());
        });
        GetUserData();
    }

    public static void GetUserData() {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
            PlayFabId = PlayfabSingleton.instance.playFabID,
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("InventorySize")) {
                Debug.Log("No Data");
                SetUserData();
            }
            else Debug.Log("Ancestor: "+result.Data["InventorySize"].Value);
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
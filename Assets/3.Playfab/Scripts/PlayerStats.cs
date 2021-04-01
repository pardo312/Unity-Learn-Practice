using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using System.Collections.Generic;
public class PlayerStats : MonoBehaviour {

    [SerializeField] private int playerHealth;
    [SerializeField] private int playerLevel;
    [SerializeField] private int maxScore;

    #region Main methods
    public void UploadStats() {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "PlayerHealth", Value = playerHealth },
                new StatisticUpdate { StatisticName = "PlayerLevel", Value = playerLevel },
                new StatisticUpdate { StatisticName = "MaxScore", Value = maxScore },
            }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    public void GetStats() {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    #endregion

    public void OnGetStatistics(GetPlayerStatisticsResult result) {
        Debug.Log("Received the following Statistics:" + result.Statistics.Count);
        foreach (var eachStat in result.Statistics) {
            switch (eachStat.StatisticName) {
                case "PlayerHealth":
                    playerHealth = eachStat.Value;
                    Debug.Log("PlayerHealth: " + playerHealth);
                    break;
                case "PlayerLevel":
                    playerLevel = eachStat.Value;
                    Debug.Log("PlayerLevel: " + playerLevel);
                    break;
                case "MaxScore":
                    maxScore = eachStat.Value;
                    Debug.Log("MaxScore: " + maxScore);
                    break;
            }
        }
    }

    #region CloudScripts

    // Build the request object and access the API
    public void StartCloudHelloWorld() {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { playerHealth = this.playerHealth, playerLevel = this.playerLevel, maxScore = this.maxScore }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudHelloWorld, OnErrorShared);
    }

    private void OnCloudHelloWorld(ExecuteCloudScriptResult result) {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        Debug.Log(PlayFabSimpleJson.SerializeObject(result.FunctionResult));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error) {
        Debug.Log(error.GenerateErrorReport());
    }

    #endregion
}

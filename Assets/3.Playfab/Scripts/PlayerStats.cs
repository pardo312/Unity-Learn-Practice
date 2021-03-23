using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
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
    foreach (var eachStat in result.Statistics){
        Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
        switch(eachStat.StatisticName){
            case "PlayerHealth":
                playerHealth = eachStat.Value;
                Debug.Log("PlayerHealth: "+playerHealth);
                break;
            case "PlayerLevel":
                playerLevel = eachStat.Value;
                Debug.Log("PlayerLevel: "+playerLevel);
                break;
            case "MaxScore":
                maxScore = eachStat.Value;
                Debug.Log("MaxScore: "+maxScore);
                break;
        }
    }
}


}
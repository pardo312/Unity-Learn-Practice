using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderBoard : MonoBehaviour {
    
    public void GetLeaderBoard(){
        var requestLeaderboard = new GetLeaderboardRequest{StartPosition = 0, StatisticName = "MaxScore", MaxResultsCount = 20};
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard,OnSuccessGetLeaderboard,OnFailureGetLeaderboard);
    }

    void OnSuccessGetLeaderboard(GetLeaderboardResult result){

        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            Debug.Log(player.DisplayName + ": " + player.StatValue);
        }
    }

    void OnFailureGetLeaderboard(PlayFabError error){
        Debug.LogError(error.GenerateErrorReport());
    }
}
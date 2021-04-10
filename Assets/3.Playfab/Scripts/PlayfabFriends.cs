using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabFriends : MonoBehaviour {
    public enum FriendIdType { PlayFabId, Username, Email, DisplayName };
    List<FriendInfo> _friends = null;
    string friendId = null;

    public void GetFriends() {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest
        {
            IncludeSteamFriends = false,
            IncludeFacebookFriends = false,
            XboxToken = null
        }, result => {
            _friends = result.Friends;
            DisplayFriends(); // triggers your UI
        }, LogPlayFabError);
        UISingleton.instance.addFriendPanel.SetActive(true);
    }

    public void InputSetFriend(string paramFriendId) {
        friendId = paramFriendId;
    }
    void DisplayFriends() {
        _friends.ForEach(f => Debug.Log(f.TitleDisplayName));
    }

    public void AddFriend(string friendIdType) {
        if(!Enum.TryParse(friendIdType, out FriendIdType idType))
            return;
        var request = new AddFriendRequest();
        switch (idType) {
            case FriendIdType.PlayFabId:
                request.FriendPlayFabId = friendId;
                break;
            case FriendIdType.Username:
                request.FriendUsername = friendId;
                break;
            case FriendIdType.Email:
                request.FriendEmail = friendId;
                break;
            case FriendIdType.DisplayName:
                request.FriendTitleDisplayName = friendId;
                break;
        }
        // Execute request and update friends when we are done
        PlayFabClientAPI.AddFriend(request, result => {
            Debug.Log("Friend added successfully!");
        }, LogPlayFabError);
        UISingleton.instance.addFriendPanel.SetActive(false);
    }

    // unlike AddFriend, RemoveFriend only takes a PlayFab ID
    // you can get this from the FriendInfo object under FriendPlayFabId
    public void RemoveFriend() {
        FriendInfo friendToRemove = null;
        foreach (FriendInfo friend in _friends)
        {
            if(friend.TitleDisplayName.Equals(friendId))
                friendToRemove = friend;
        }
        if(friendToRemove == null){
            Debug.Log("Friend Not Found");
            return;
        }
        PlayFabClientAPI.RemoveFriend(new RemoveFriendRequest
        {
            FriendPlayFabId = friendToRemove.FriendPlayFabId  
        }, result => {
            Debug.Log("Remove friend successfully");
        }, LogPlayFabError);
    }

    private void LogPlayFabError(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }
}

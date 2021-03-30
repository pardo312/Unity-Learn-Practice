using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PlayerLogic : MonoBehaviour {
    public float playerScore = 0;
    public float playerKillCount = 0;

    private void Update() {
        playerScore += Random.Range(0, 10) * Time.deltaTime;
        playerKillCount += Random.Range(0, 10) * Time.deltaTime;
    }
    #region Delegate
    // Delegate are attributes that consist of a method "template"
    // that we can add methods to it, even from other classes and
    // that when the delegate is executed, all the other methods will too. 

    public delegate void PlayerDeathDelegate();
    public PlayerDeathDelegate playerDeathDelegate;

    public void playerDeathDelegateTest() {
        if (playerDeathDelegate != null)
            playerDeathDelegate();
    }

    #endregion

    #region Event
    // The events are a property assign to a delegate that
    // will restrict the calling class to only add a method callback.
    // It only will allow to add (+=) a new method callback and will not allow to set (=) the delegate
    // It will not allow the calling class to activate the method. I.E: PlayerLogic.playerDeathdelegate();

    public delegate void PlayerDeathEventDelegate();
    public event PlayerDeathDelegate playerDeathEventDelegate;

    public void playerDeathEventDelegateTest() {
        if (playerDeathDelegate != null)
            playerDeathDelegate();
    }

    #endregion

    #region Action 
    //Action is an abreviation of the whole delegate and event attributes
    // Is used when you want a delegate that has void return type
    // If you want the funcion to has parameters you can put them inside the <>
    // I.E: Action<int,string>

    public event Action<int> playerDeathAction;

    public void playerDeathActionTest() {
        if (playerDeathAction != null)
            playerDeathAction(4);
    }

    #endregion

    #region Func
    // Func is an abreviation of the whole delegate and event attributes
    // Is used when you want a delegate that has a custom return type
    // The first paramenter of the Func will be its return tipe I.E: Func<int> returns an int
    // If you want the funcion to has parameters you can put them inside the <> before the return parameter 
    // I.E: Func<int,string> the Func return a string an has an int as parameter

    public event Func<string,int> playerDeathFunc;

    public void playerDeathFuncTest() {
        if (playerDeathFunc != null)
            playerDeathFunc("Hello");
    }

    #endregion
}

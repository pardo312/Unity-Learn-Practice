using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbility : Ability
{
    public override string Name => "Fire";

    public override void Process() {
        Debug.Log("Fire Creation");
    }
}

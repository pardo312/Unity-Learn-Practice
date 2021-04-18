using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    public static AbilityFactory instance;
    private void Awake() {
        if(instance!=null)
            return;

        instance = this; 
        DontDestroyOnLoad(this);
    }
    public Dictionary<string, Type> abilitiesByName;

    public AbilityFactory(){
        var abilityTypes = Assembly.GetAssembly(typeof(Ability)).GetTypes().Where(myType => !myType.IsAbstract && myType.IsSubclassOf(typeof(Ability)));

        abilitiesByName = new Dictionary<string, Type>();

        foreach (var type in abilityTypes)
        {
            var tempEffect = Activator.CreateInstance(type) as Ability;
            abilitiesByName.Add(tempEffect.Name, type);
        }
    }

    public Ability GetAbility(string abilityType){
        if(abilitiesByName.ContainsKey(abilityType)){
            Type type = abilitiesByName[abilityType];
            var ability = Activator.CreateInstance(type) as Ability;
            return ability;
        }
        return null;
    }

    internal IEnumerable<string> GetAbilityNames(){
        return abilitiesByName.Keys;
    }
}

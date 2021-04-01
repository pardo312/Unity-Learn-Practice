using System;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public static event Action<PointOfInterest> unlockAchivement;
    [SerializeField] private string poiName;
    public void AchivementUnlocked()
    {
        if(unlockAchivement!=null) 
            unlockAchivement.Invoke(this);
    }
    
}

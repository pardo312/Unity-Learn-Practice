using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementSystem : MonoBehaviour
{
    private void Start() {
        PointOfInterest.unlockAchivement += unlockAchivementWithName; 
    }

    private void unlockAchivementWithName(PointOfInterest poi)
    {
        // Do the achivement unlock || add it to playerPrefs
    }
}

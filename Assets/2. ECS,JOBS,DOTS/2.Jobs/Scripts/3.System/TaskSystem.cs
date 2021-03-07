using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
public class TaskSystem : SystemBase
{
    // protected override void OnUpdate()
    // {
    //     float answer = 0;
    //     Entities.ForEach((ref TaskComponent taskComponent) =>
    //     {
    //         for (int i = 0; i < taskComponent.numberOfTimesCalculation; i++)
    //         {
    //             answer = Mathf.Sqrt(i * (i % 20));
    //         }
    //     });
    // }

    protected override void OnUpdate()
    {
        Entities.ForEach((in TaskComponent taskComponent) =>
        {
            float answer = 0;
            for (int i = 0; i < taskComponent.numberOfTimesCalculation; i++)
            {
                answer = Mathf.Sqrt(i * (i % 20));
            }
        }).Schedule();
    }
}
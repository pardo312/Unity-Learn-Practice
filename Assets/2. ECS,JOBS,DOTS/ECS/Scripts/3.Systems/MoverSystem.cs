using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class MoverSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeedComponent) =>
        {
            if(translation.Value.y > 5) moveSpeedComponent.speed = -Mathf.Abs(moveSpeedComponent.speed);
            if(translation.Value.y < -5) moveSpeedComponent.speed = Mathf.Abs(moveSpeedComponent.speed);
            translation.Value.y += 1f * Time.DeltaTime * moveSpeedComponent.speed;
        });
    }
}

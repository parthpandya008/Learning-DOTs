using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class PlayerMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.
            WithAll<PlayerTag>().
            ForEach((ref Translation pos, in MoveData moveData) =>
        {
            float3 normalizeDir = math.normalizesafe(moveData.direction);
            pos.Value += normalizeDir * moveData.speed * deltaTime; 

        }).Run() ;
    }
}

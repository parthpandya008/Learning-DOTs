using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class MoveForwardSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.
            WithAny<AstroidTag, ChaserTag>().
            WithNone<PlayerTag>().
            ForEach((ref Translation translation, in MoveData moveData, in Rotation rotation) => 
        {
            float3 forwardDirection = math.forward(rotation.Value);
            translation.Value += forwardDirection * moveData.speed * deltaTime;
        }).ScheduleParallel();
    }
}

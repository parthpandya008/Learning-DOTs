using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class TargetToDirectionSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.
            WithNone<PlayerTag>().
            WithAll<ChaserTag>().
            ForEach((ref MoveData moveData, ref Rotation rotation, in Translation translation, in TargetData targetData) =>
            {
                ComponentDataFromEntity<Translation> allTranslation = GetComponentDataFromEntity<Translation>(true);
                
                Translation targetTranslation = allTranslation[targetData.targetEntity];
                float3 dir = targetTranslation.Value - translation.Value;
                moveData.direction = dir;

               // FaceDirection(ref rotation, moveData);

            }).Run();
    }
}

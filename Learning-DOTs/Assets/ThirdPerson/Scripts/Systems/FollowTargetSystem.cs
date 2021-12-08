using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using ThirdPerson;

namespace ThirdPerson
{
    public class FollowTargetSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref MoveData moveData, in TargetData targetData, in Translation translation) => {

                ComponentDataFromEntity<Translation> allTranslations = GetComponentDataFromEntity<Translation>(true);
                if (allTranslations.HasComponent(targetData.targetEntity))
                {
                    Translation targetPos = allTranslations[targetData.targetEntity];
                    float3 dir = targetPos.Value - translation.Value;
                    moveData.targetDirection = new float3(dir.x, 0, dir.z);
                }

            }).Schedule();
        }
    }
}

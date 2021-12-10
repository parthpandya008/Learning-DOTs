using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace ThirdPerson
{
    public class LookAtTargetSystem : SystemBase
    {
        protected override void OnUpdate()
        {

            Entities.ForEach(( ref RotateData rotateData, in TargetData targetData, in Translation translation) =>
            {
                ComponentDataFromEntity<Translation> allTranslations = GetComponentDataFromEntity<Translation>(true);
                if (allTranslations.HasComponent(targetData.lookAtEntity))
                {
                    Translation targetPos = allTranslations[targetData.lookAtEntity];
                    float3 dir = targetPos.Value - translation.Value;
                    rotateData.rotateTargetPosition = new float3(dir.x, 0, dir.z);
                }
            }).Schedule();
        }
    }
}

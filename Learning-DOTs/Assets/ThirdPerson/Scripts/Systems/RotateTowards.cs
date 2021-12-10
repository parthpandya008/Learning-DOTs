using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using ThirdPerson;

namespace ThirdPerson
{

    public class RotateTowards : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Rotation rotation, in RotateData rotateData) => {
                    if (!rotateData.rotateTargetPosition.Equals(float3.zero))
                    {
                        quaternion newRotation = quaternion.LookRotationSafe(rotateData.rotateTargetPosition, math.up());
                        rotation.Value = math.slerp(rotation.Value, newRotation, rotateData.rotateSpeed);
                    }                    
                }).Schedule();
        }
    }
}

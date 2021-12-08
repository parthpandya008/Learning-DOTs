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
            Entities.ForEach((ref Rotation rotation,
                in MoveData moveData, in RotateData rotateData) => {
                    if (!moveData.targetDirection.Equals(float3.zero))
                    {
                        quaternion newRotation = quaternion.LookRotationSafe(moveData.targetDirection, math.up());
                        rotation.Value = math.slerp(rotation.Value, newRotation, rotateData.rotateSpeed);
                    }                    
                }).Schedule();
        }
    }
}

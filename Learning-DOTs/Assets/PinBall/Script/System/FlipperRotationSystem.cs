using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace PinBall
{
    public class FlipperRotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Rotation rotation, ref FlipperRotationData flipperRotationData) => {
                if(flipperRotationData.CurrentRotationDirection == 0)
                {
                    return;
                }
                float targetRotation = flipperRotationData.CurrentRotationDirection == flipperRotationData.RotationDirectionForActivation ?
                                        flipperRotationData.ActivatedRotation : flipperRotationData.IntialRotation;
               
                quaternion newRotation = quaternion.AxisAngle(new float3(0,0,1), math.radians(targetRotation));
                rotation.Value = newRotation;

                flipperRotationData.CurrentRotationDirection = 0;
            }).Run();
        }
    }
}

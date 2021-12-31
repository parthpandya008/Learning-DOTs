using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


namespace PinBall
{
    public class FlipperInputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref FlipperRotationData flipperRotationData, in FlipperInput flipperInput) =>
            {
                if (Input.GetKeyDown(flipperInput.keyCode))
                {
                    flipperRotationData.CurrentRotationDirection = flipperRotationData.RotationDirectionForActivation;
                }
                else if (Input.GetKeyUp(flipperInput.keyCode))
                {
                    flipperRotationData.CurrentRotationDirection = -flipperRotationData.RotationDirectionForActivation;
                }

            }).Run ();
        }
    }
}

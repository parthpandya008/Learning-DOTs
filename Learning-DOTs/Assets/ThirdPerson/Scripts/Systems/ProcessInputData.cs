using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ThirdPerson
{
    public class ProcessInputData : SystemBase
    {
        protected override void OnUpdate()
        {
            //Temp vars coz Horizontal & Vertical are not blittable types
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            Entities.ForEach((ref RawInputData inputData, ref MoveData moveData, ref RotateData rotateData) =>
            {
                inputData.inputH = inputH;
                inputData.inputV = inputV;

                //Set Direction Data
                moveData.targetDirection = new float3(inputData.inputH,0 ,inputData.inputV);
                rotateData.rotateTargetPosition = moveData.targetDirection;
            }).Schedule();
        }
    }
}

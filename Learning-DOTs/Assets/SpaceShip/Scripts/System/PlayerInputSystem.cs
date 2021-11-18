using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Entities;

public class PlayerInputSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref MoveData moveData, in InputData inputData) =>
        {
            bool isRight = Input.GetKey(inputData.rightKey);
            bool isLeft = Input.GetKey(inputData.leftKey);
            bool isUp = Input.GetKey(inputData.upKey);
            bool isDown = Input.GetKey(inputData.downKey);

            moveData.direction.x = Convert.ToInt32(isRight);
            moveData.direction.x -= Convert.ToInt32(isLeft);
            moveData.direction.z = Convert.ToInt32(isUp);
            moveData.direction.z -= Convert.ToInt32(isDown);


        }).Run();
    }
}

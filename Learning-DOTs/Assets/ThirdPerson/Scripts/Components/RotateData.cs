using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;


namespace ThirdPerson
{
    [GenerateAuthoringComponent]
    public struct RotateData : IComponentData
    {
        public float rotateSpeed;
    }
}
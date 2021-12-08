using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ThirdPerson
{
    [GenerateAuthoringComponent]
    public struct TargetData : IComponentData
    {
        public Entity targetEntity;


    }
}

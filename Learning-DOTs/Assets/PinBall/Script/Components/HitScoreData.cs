using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace PinBall
{
    [GenerateAuthoringComponent]
    public struct HitScoreData : IComponentData
    {
        public int HitScore;
    }
}

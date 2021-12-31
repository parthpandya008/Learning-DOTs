using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace PinBall
{
    [GenerateAuthoringComponent]
    public struct ScoreData : IComponentData
    {
        public int totalScore;
    }
}

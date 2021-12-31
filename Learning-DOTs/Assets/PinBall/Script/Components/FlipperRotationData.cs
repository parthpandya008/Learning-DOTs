using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace PinBall
{
    [GenerateAuthoringComponent]
    public struct FlipperRotationData : IComponentData
    {
        public int RotationDirectionForActivation;
        public int CurrentRotationDirection;
        public float RotationSpeed;
        public float IntialRotation;
        public float ActivatedRotation;
    }
}
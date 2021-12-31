using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PinBall
{
    [GenerateAuthoringComponent]
    public struct FlipperInput : IComponentData
    {
        public KeyCode keyCode;

    }
}

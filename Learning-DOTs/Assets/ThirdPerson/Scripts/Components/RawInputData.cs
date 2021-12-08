using Unity.Entities;
using UnityEngine;

namespace ThirdPerson
{
    [GenerateAuthoringComponent]
    public struct RawInputData : IComponentData
    {
        [HideInInspector]
        public float inputH; //Horizontal Input
        [HideInInspector]
        public float inputV; //Vertical Input
    }
}

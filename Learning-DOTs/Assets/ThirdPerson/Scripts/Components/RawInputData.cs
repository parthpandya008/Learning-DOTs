using Unity.Entities;

[GenerateAuthoringComponent]
public struct RawInputData : IComponentData
{
    public float inputH; //Horizontal Input
    public float inputV; //Vertical Input
}

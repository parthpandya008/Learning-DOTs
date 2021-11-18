using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpinnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.
            WithAny<SpinnerTag>().
            WithNone<PlayerTag>().
            ForEach((ref Rotation rot, in MoveData moveData) =>
        {
            quaternion quaternionNormal = math.normalize(rot.Value);
            quaternion angleToRotate = quaternion.AxisAngle(math.up(), moveData.turnSpeed * deltaTime);

            rot.Value = math.mul(quaternionNormal , angleToRotate);

        }).ScheduleParallel();
    }

    
}

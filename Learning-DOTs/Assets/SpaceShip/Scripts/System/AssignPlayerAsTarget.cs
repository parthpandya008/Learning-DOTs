using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup (typeof(SimulationSystemGroup))]
[UpdateBefore (typeof (TargetToDirectionSystem))]
public class AssignPlayerAsTarget : SystemBase
{
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        AssignPlayer();
    }

    protected override void OnUpdate()
    {
        //throw new System.NotImplementedException();
    }

    private void AssignPlayer()
    {
        //EntityQuery entityQuery = GetEntityQuery(typeof(PlayerTag));
        EntityQuery entityQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerTag>());
        Entity playerEntity = entityQuery.GetSingletonEntity();

        Entities.
            WithAll<ChaserTag>().
            ForEach((ref TargetData targetData) =>
            {
                targetData.targetEntity = playerEntity;
            }).Schedule();
    }
}

using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public class RemoveOnDeathSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem ecbSystem;

    protected override void OnCreate()
    {
        base.OnCreate();

        ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();

        Entities.
            WithAny<PlayerTag, ChaserTag>(). 
            ForEach((Entity entity, in HealthData healthData) =>
            {
                if(healthData.isDead)
                {
                    ecb.DestroyEntity(entity);
                }

            }).Schedule();


        ecbSystem.AddJobHandleForProducer(this.Dependency);
    }
}

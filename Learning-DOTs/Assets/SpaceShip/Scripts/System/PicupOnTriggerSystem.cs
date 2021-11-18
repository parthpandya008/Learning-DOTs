using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateAfter (typeof(EndFramePhysicsSystem))]
public class PicupOnTriggerSystem : JobComponentSystem
{
    private BuildPhysicsWorld buildPhysicsWorld;
    private StepPhysicsWorld stepPhysicsWorld;
    private EndSimulationEntityCommandBufferSystem ecbSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        
        ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //Intilize all the fields of struct in the OnUpdate
        PickupOnTriggerSystemJob job = new PickupOnTriggerSystemJob();
        job.allPlayers = GetComponentDataFromEntity<PlayerTag>(true);
        job.allPickups = GetComponentDataFromEntity<PickupTag>(true);
        job.ecb = ecbSystem.CreateCommandBuffer();

        JobHandle jobHandle = job.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}

[BurstCompile]
public struct PickupOnTriggerSystemJob : ITriggerEventsJob
{
    [ReadOnly] public ComponentDataFromEntity<PickupTag> allPickups;
    [ReadOnly] public ComponentDataFromEntity<PlayerTag> allPlayers;
    public EntityCommandBuffer ecb;    

    public void Execute(TriggerEvent triggerEvent)
    {
        Entity entityA = triggerEvent.EntityA;
        Entity entityB = triggerEvent.EntityB;

        Debug.Log("TriggerEvent");
        ecb.DestroyEntity(entityA);
        //allPickups.Exist(entityA);
        if (allPickups.HasComponent(entityA) && allPlayers.HasComponent(entityB))
        {
            
            Debug.Log("Entity A PickUp");
            ecb.DestroyEntity(entityA);


        }
        else if (allPlayers.HasComponent(entityA) && allPickups.HasComponent(entityB))
        {
            ecb.DestroyEntity(entityB);
            Debug.Log("Entity B PickUp");
        }
        /*
        if (allPickups.Exists(entityA) && allPickups.Exists(entityB))
        {
            
            return;
        }
        
      else if (allPickups.HasComponent(entityA) && allPlayers.HasComponent(entityB))
        {
            UnityEngine.Debug.Log("AllPick up Enityt A" + entityA + " AllPickUp Entity B " + entityB);
            
        }
        else if (allPlayers.HasComponent(entityA) && allPickups.HasComponent(entityB))
        {
            UnityEngine.Debug.Log("allPlayers up Enityt A" + entityA + " allpickups Entity B " + entityB);
            
        }
        */
    }
}

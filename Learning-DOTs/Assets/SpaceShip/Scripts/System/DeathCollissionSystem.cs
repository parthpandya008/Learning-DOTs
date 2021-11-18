using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;

[UpdateAfter (typeof(EndFramePhysicsSystem))]
public class DeathCollissionSystem : JobComponentSystem
{
    private BuildPhysicsWorld buildPhysics;
    private StepPhysicsWorld stepPhysics;

    protected override void OnCreate()
    {
        base.OnCreate();

        buildPhysics = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysics = World.GetOrCreateSystem<StepPhysicsWorld>();
    }
    private struct DeathCollissionSystemJob : ICollisionEventsJob
    {

        [ReadOnly]
        public ComponentDataFromEntity<DeathColliderTag> deathColliderGroup;
        [ReadOnly]
        public ComponentDataFromEntity<ChaserTag> cheaserGroup;
        public ComponentDataFromEntity<HealthData> healthGroup;
        public void Execute(CollisionEvent collisionEvent)
        {
            Entity entityA = collisionEvent.EntityA;
            Entity entityB = collisionEvent.EntityB;

           
            bool enitytACheaser = cheaserGroup.HasComponent(entityA);
            bool enitytBCheaser = cheaserGroup.HasComponent(entityB);
            //bool enitytBIsDeath = deathColliderGroup.HasComponent(entityB);
            //bool enitytBCheaser = cheaserGroup.HasComponent(entityB);
            

            if(enitytACheaser)
            {
                HealthData newData = healthGroup[entityA];
                newData.isDead = true;
                healthGroup[entityA] = newData;

                UnityEngine.Debug.Log(" entityA " + entityA + " entityB " + entityB);
                UnityEngine.Debug.Log(" enitytBCheaser " + enitytBCheaser + " enitytACheaser " + enitytACheaser);
            }
            else if(enitytBCheaser)
            {
                HealthData newData = healthGroup[entityA];
                newData.isDead = true;
                healthGroup[entityA] = newData;

                UnityEngine.Debug.Log(" entityA " + entityA + " entityB " + entityB);
                UnityEngine.Debug.Log(" enitytBCheaser " + enitytBCheaser + " enitytACheaser " + enitytACheaser);
            }
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        DeathCollissionSystemJob job = new DeathCollissionSystemJob();
        job.deathColliderGroup = GetComponentDataFromEntity<DeathColliderTag>(true);
        job.cheaserGroup = GetComponentDataFromEntity<ChaserTag>(true);
        job.healthGroup = GetComponentDataFromEntity<HealthData>(false);

        JobHandle jobHandle = job.Schedule(stepPhysics.Simulation, ref buildPhysics.PhysicsWorld, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}

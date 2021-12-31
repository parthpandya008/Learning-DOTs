using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;


namespace PinBall
{
    public class ScoreSystem : SystemBase
    {
       private BuildPhysicsWorld buildPhysicsWorld;
       private StepPhysicsWorld stepPhysicsWorld;
       private EntityQuery entityQueryGroup;

        protected override void OnCreate()
        {
            buildPhysicsWorld = World.GetExistingSystem<BuildPhysicsWorld>();
            stepPhysicsWorld = World.GetExistingSystem<StepPhysicsWorld>();
            entityQueryGroup = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[] 
                { 
                    typeof(ScoreData) 
                }
            });
        }

        protected override void OnUpdate()
        {
            if(entityQueryGroup.CalculateEntityCount() == 0)
            {
                return;
            }

            ScoreJob scoreJob = new ScoreJob();
            scoreJob.HitScoreDataGroup = GetComponentDataFromEntity<HitScoreData>(true);
            scoreJob.ScoreDataGroup = GetComponentDataFromEntity<ScoreData>();
            scoreJob.ParentGroup = GetComponentDataFromEntity<Parent>(true);
            JobHandle jobHandle = scoreJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, Dependency);
            jobHandle.Complete();
        }
    }

    public struct ScoreJob : ICollisionEventsJob
    {         
        public ComponentDataFromEntity<ScoreData> ScoreDataGroup;
        [ReadOnly]
        public ComponentDataFromEntity<HitScoreData> HitScoreDataGroup;
        [ReadOnly]
        public ComponentDataFromEntity<Parent> ParentGroup;

        
        public void Execute(CollisionEvent collisionEvent)
        {
            Entity entityA = collisionEvent.EntityA;
            Entity entityB = collisionEvent.EntityB;

            bool isAScoreCollider = HitScoreDataGroup.HasComponent(entityA);
            bool isBScoreCollider = HitScoreDataGroup.HasComponent(entityB);

            if((isAScoreCollider && isBScoreCollider) || (!isAScoreCollider && !isBScoreCollider))
            {
                return;
            }

            Entity collisionEntity = isAScoreCollider ? entityA : entityB;
            HitScoreData hitScoreData = HitScoreDataGroup[collisionEntity];

            Parent parent = ParentGroup[collisionEntity];
            ScoreData scoreData = ScoreDataGroup[parent.Value];

            scoreData.totalScore += hitScoreData.HitScore;
            //Can't modify directly so set it back
            ScoreDataGroup[parent.Value] = scoreData;
        }
    }
}

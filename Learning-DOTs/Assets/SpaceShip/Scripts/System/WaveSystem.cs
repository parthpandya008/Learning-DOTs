using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;


public class WaveSystem_ComponentSystem : ComponentSystem //ComponentSystem is a base for System
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation trans, ref MoveSpeedData speedData, ref WaveData wave) =>
        {
            float zPos = wave.amplitude * math.sin((float)Time.ElapsedTime * speedData.Value) 
            + (trans.Value.x * wave.xOffset) + (trans.Value.y * wave.yOffset);
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPos);
        });
    }
}



public class WaveSystem_JobComponentSystem : JobComponentSystem
{
 //OnUpdate Runs on Main Thread
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float elapsedTime = (float)Time.ElapsedTime;

        JobHandle jobHandle;
        #region Jobified Logic (worker Thread)
        //Used in coz, those are read only properties, in gives little bit performace boost
        jobHandle = Entities.ForEach((ref Translation trans, in MoveSpeedData speedData, in  WaveData wave) =>
        {
            
            float zPos = wave.amplitude * math.sin(elapsedTime * speedData.Value)
            + (trans.Value.x * wave.xOffset) + (trans.Value.y * wave.yOffset);
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPos);
        }).Schedule(inputDeps); //Insted of Schedule if you use Run, Job runs on Main thread
        #endregion
        return jobHandle;
    }
}



public class WaveSystem_SystemBase : SystemBase
{
    //OnUpdate Runs on Main Thread
    protected override void OnUpdate()
    {
        float elapsedTime = (float)Time.ElapsedTime;

        JobHandle jobHandle;
        #region Jobified Logic (worker Thread)
        //Used in coz, those are read only properties, in gives little bit performace boost
        Entities.ForEach((ref Translation trans, in MoveSpeedData speedData, in WaveData wave) =>
        {
            float zPos = wave.amplitude * math.sin(elapsedTime * speedData.Value)
            + (trans.Value.x * wave.xOffset) + (trans.Value.y * wave.yOffset);
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPos);
        }).ScheduleParallel();
        ; //Insted of Schedule if you use Run, Job runs on Main thread
        #endregion
       
    }
}

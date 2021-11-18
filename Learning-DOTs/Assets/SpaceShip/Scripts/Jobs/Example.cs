using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace Jobs
{
    public class Example : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DoExample();
        }

       private void DoExample()
        {
            NativeArray<float> resultArry = new NativeArray<float>(1, Allocator.TempJob); //Tempjob would keep this till job is executing or 4 frams
            //Instante and intilize Job
            SimpleJob simpleJob = new SimpleJob
            {
                a = 5,
                result = resultArry
            };
            SecondJob secondJob = new SecondJob
            {
                result = resultArry
            };

            //Schedule
            JobHandle jobHandle = simpleJob.Schedule();
            JobHandle secondJobHandle = secondJob.Schedule(jobHandle);

            //other task to run in main thread

            //jobHandle.Complete(); // No need, because dependancies implies with the second job handle 
            secondJobHandle.Complete();

            float resultingValue = resultArry[0];
            Debug.Log(" resultArry " + resultingValue);

            resultArry.Dispose();
            
        }
    }

    public struct SimpleJob : IJob
    {
        public float a;
        public NativeArray<float> result;
        public void Execute()
        {
            result[0] = a;
        }
    }

    public struct SecondJob : IJob
    {        
        public NativeArray<float> result;
        public void Execute()
        {
            result[0] +=1;
        }
    }
}

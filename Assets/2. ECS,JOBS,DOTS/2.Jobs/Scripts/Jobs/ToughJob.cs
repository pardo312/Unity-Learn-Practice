using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
[BurstCompile]
public struct ToughJob : IJob
{
    public int numberToSquareRoot;
    public void Execute()
    {
        float answer = 0;
            answer = Mathf.Sqrt( numberToSquareRoot* (numberToSquareRoot%20));
            Debug.Log(answer);        
    }
}

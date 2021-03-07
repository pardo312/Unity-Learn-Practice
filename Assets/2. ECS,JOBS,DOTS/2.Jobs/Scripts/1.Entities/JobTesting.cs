using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Rendering;
using Unity.Transforms;

public class JobTesting : MonoBehaviour
{
    public void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(TaskComponent),
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(RenderBounds)
        );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(1, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);

        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new TaskComponent
            {
                numberOfTimesCalculation = 660000
            });
        }
        entityArray.Dispose();
    }
 
}

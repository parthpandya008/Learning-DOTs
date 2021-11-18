using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Mesh unityMesh;
    [SerializeField]
    private Material unitMaterial;

    [SerializeField]
    private GameObject prefab;

    private Entity entityPrefab;
    private World defaultWorld;
    private EntityManager entityManager;
    [SerializeField]
    private int row, colom, width;
    [SerializeField]
    private float space;
    // Start is called before the first frame update
    void Start()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);

        //InstantiateEntity(new float3(1, 1, 1));
        InstantiateEntityGrid(row, colom,width, space);
    }

    private void InstantiateEntity(float3 position)
    {
        Entity newInstantiateEnity = entityManager.Instantiate(entityPrefab);
        entityManager.SetComponentData(newInstantiateEnity, new Translation
        {
            Value = position
        }); ;
    }

    private void InstantiateEntityGrid(int row, int col, int width, float space)
    {
        for(int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {
                for(int k = 0; k < width; k++)
                {
                    InstantiateEntity(new float3(i * space, j * space, k*space));
                }                
            }
        }
    }
    void MakeEntity()   
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager; //World.DefaultGameObjectInjectionWorld is default world  
        EntityArchetype archetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
            );
        Entity myEntity = entityManager.CreateEntity(archetype);

        entityManager.AddComponentData(myEntity, new Translation
        { 
            Value = new float3(2,0,4) 
        });

        entityManager.AddSharedComponentData(myEntity, new RenderMesh
        {
            mesh = unityMesh,
            material = unitMaterial
        }) ;

    }
}

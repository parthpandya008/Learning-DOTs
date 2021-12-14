using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ThirdPerson;
using Unity.Mathematics;
using Unity.Transforms;

namespace ThirdPerson
{
    public class ConvertPlayerCam : MonoBehaviour, IConvertGameObjectToEntity
    {
        public EntityManager entityManager;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float rotateSpeed;
        [SerializeField]
        private float3 offset;

        public Entity followEntity, lookAtEntity;
        

        private void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
         
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<CopyTransformToGameObject>(entity);

            dstManager.AddComponentData(entity, new MoveData { moveSpeed = moveSpeed});
            dstManager.AddComponentData(entity, new RotateData { rotateSpeed = rotateSpeed });
            dstManager.AddComponentData(entity, new TargetData { followEntity = followEntity, lookAtEntity = lookAtEntity, offset = offset });
        }
    }
}

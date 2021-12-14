using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ThirdPerson;
using Unity.Transforms;

namespace ThirdPerson
{
    public class ConvertPlayer : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeReference]
        private GameObject camera;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<CopyTransformToGameObject>(entity);
            ConvertPlayerCam convertCamera = camera.GetComponent<ConvertPlayerCam>();
            if (convertCamera != null)
            {
                convertCamera.lookAtEntity = entity;
                convertCamera.followEntity = entity;
            }
        }
        
    }
}
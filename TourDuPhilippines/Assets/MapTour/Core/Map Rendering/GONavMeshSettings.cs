using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigiShared;
using UnityEngine.AI;

namespace DigiMap {

    [System.Serializable]
    public class GONavMeshSettings
    {

        public bool buildNavmeshSurfaces = false;

        [Header("NavMesh Surface Settings")]
        public int AgentTypeIndex = 0;
        public CollectObjects CollectObjects = CollectObjects.All;
        public LayerMask IncludeLayers = ~0;
        public NavMeshCollectGeometry m_UseGeometry = NavMeshCollectGeometry.RenderMeshes;

        [Header("NavMesh Link")]
        public bool useNavmeshLinks;
    }
}

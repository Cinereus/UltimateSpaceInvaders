using System;
using System.Collections.Generic;
using PlayScene.Common.ObjectPool;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace PlayScene.Swarms
{
   public class SwarmGenerator : MonoBehaviour
   {
      public UnityAction<SwarmController> OnSwarmGenerated = arg0 =>{};

      private GameObject[] _swarmControllers;
      private List<Pool> _swarmPools = new List<Pool>();

      private const int SWARM_POOL_SIZE = 3;

      private void Awake()
      {
         Cursor.visible = false;
         _swarmControllers = Resources.LoadAll<GameObject>("Prefabs\\Swarms");
         
         foreach (var swarmController in _swarmControllers)
         {
            _swarmPools.Add(Pool.CreatePool(swarmController, SWARM_POOL_SIZE));
         }
      }
      
      private void Start()
      {
         GenerateSwarm();
      }

      private void GenerateSwarm()
      {
         var currentSwarm = GetRandomSwarmPool().GetObject().GetComponent<SwarmController>();;
         currentSwarm.InitializeSwarm();
         OnSwarmGenerated(currentSwarm);
      }

      private Pool GetRandomSwarmPool()
      {
         var randomIndex = Random.Range(0, _swarmPools.Count);
         return _swarmPools[randomIndex];
      }
   }
}

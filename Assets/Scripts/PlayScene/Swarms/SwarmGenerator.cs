using System;
using System.Collections.Generic;
using PlayScene.Common.ObjectPool;
using PlayScene.UI;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace PlayScene.Swarms
{
   public class SwarmGenerator : MonoBehaviour
   {
      [SerializeField] private List<Path> _paths; 
      [SerializeField] private GameObject[] _invaderPrefabs;
      [SerializeField] private GameObject[] _swarmControllers;
      [SerializeField] private List<Material> _invaderMaterials;
      [SerializeField] private VictoryPanel _panel;
      
      private List<Pool> _swarmPools = new List<Pool>();
      private const int SWARM_POOL_SIZE = 3;

      private void Awake()
      {
         Cursor.visible = false;

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
         currentSwarm.InitializeSwarm(_paths, _invaderPrefabs, _invaderMaterials);
         currentSwarm.OnDie += _panel.ShowVictoryPanel;
      }

      private Pool GetRandomSwarmPool()
      {
         var randomIndex = Random.Range(0, _swarmPools.Count);
         return _swarmPools[randomIndex];
      }
   }
}

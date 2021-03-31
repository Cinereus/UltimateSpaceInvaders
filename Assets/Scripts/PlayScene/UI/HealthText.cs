using System;
using PlayScene.Player;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace PlayScene.UI
{
    [RequireComponent(typeof(Text))]
    public class HealthText : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        
        private void Awake()
        {
            var health = GetComponent<Text>();
            
            _player.OnHealthChanged += healthPoints => { health.text =  "HEALTH: "+healthPoints; };
        }
    }
}

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
        private void Awake()
        {
            var health = GetComponent<Text>();
            var player = FindObjectOfType<PlayerController>();

            player.OnHealthChanged += healthPoints => { health.text =  "HEALTH: "+healthPoints; };
        }
    }
}

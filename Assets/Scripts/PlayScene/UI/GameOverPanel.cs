using System.Collections;
using System.Collections.Generic;
using PlayScene.Player;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    void Awake()
    {
        var player = FindObjectOfType<PlayerController>();

        player.OnDie += () => _panel.SetActive(true);
    }
}

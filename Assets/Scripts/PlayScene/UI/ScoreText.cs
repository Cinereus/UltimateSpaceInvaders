using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayScene.UI
{
    [RequireComponent(typeof(Text))]
    public class ScoreText : MonoBehaviour
    {
        private Text _text;
        private static int _scores;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void GainScore(int scores)
        {
            _scores += scores;
        }
    
        private void Update()
        {
            _text.text = "SCORES: " + _scores;
        }

        private void OnDestroy()
        {
            _scores = 0;
        }
    }
}

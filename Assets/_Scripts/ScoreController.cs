using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private int _score;

        private Tween _counterTween;
        
        public void AddScore(int score)
        {
            var startScore = _score;
            _score += Math.Max(0, score);
            
            _counterTween?.Complete();
            _counterTween = DOVirtual.Float(startScore, _score, .32f, value =>
            {
                _scoreText.text = $"{value:N0}";
            });
        }
        
        private void Awake()
        {
            _scoreText.text = "0";
        }
    }
}
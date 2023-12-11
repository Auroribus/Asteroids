using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Components.Asteroids.Configs
{
    [CreateAssetMenu(fileName = nameof(AsteroidsConfig), menuName = "Configs/" + nameof(AsteroidsConfig))]
    public class AsteroidsConfig : ScriptableObject
    {
        [Serializable]
        private class Setting
        {
            public AsteroidSize Size;
            public float SizeFactor;
            public Color Color;
            public float VelocityFactor;
        }

        [SerializeField] private Setting[] _settings;

        private Dictionary<AsteroidSize, Setting> _cache;
        private Dictionary<AsteroidSize, Setting> Cache => _cache ??= _settings.ToDictionary(e => e.Size);

        public float GetSizeFactor(AsteroidSize size)
        {
            return Cache[size].SizeFactor;
        }
        
        public float GetVelocityFactor(AsteroidSize size)
        {
            return Cache[size].VelocityFactor;
        }
        
        public Color GetColor(AsteroidSize size)
        {
            return Cache[size].Color;
        }
    }
}
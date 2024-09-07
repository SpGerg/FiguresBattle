using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models.Abilities.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New ability data", menuName = "Ability data", order = 51)]
    public class AbilityData : ScriptableObject
    {
        [Serializable]
        public class AbilityKeyAndValuePair
        {
            [SerializeField]
            private string _key;

            [SerializeField]
            private float _value;

            public string Key { get => _key; }

            public float Value { get => _value; }
        }

        public IReadOnlyDictionary<string, float> Values { get; private set; }

        [SerializeField]
        private AbilityKeyAndValuePair[] keyAndValuePairs;

        public void Awake()
        {
            var dictionary = new Dictionary<string, float>();

            foreach (var keyValuePair in keyAndValuePairs)
            {
                dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }

            Values = dictionary;
        }
    }
}

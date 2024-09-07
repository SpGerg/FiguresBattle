using System;
using UnityEngine;

namespace Presenters.Figures.Datas
{
    using Enums;

    [Serializable]
    public class DirectionAndValue
    {
        [SerializeField]
        private DirectionType _direction;

        [SerializeField]
        private float _value;

        [SerializeField]
        private bool _isInfinity;

        [SerializeField]
        private bool _isEnemyRequired;

        public DirectionType Direction { get => _direction; }

        public float Value { get => _value; }

        public bool IsInfinity { get => _isInfinity; }

        public bool IsEnemyRequired { get => _isEnemyRequired; }
    }
}

namespace Datas
{
    using System;
    using UnityEngine;

    [Serializable]
    public class KeyAndValue<T, K>
    {
        [SerializeField]
        private T _key;

        [SerializeField]
        private K _value;

        public T Key { get => _key; }

        public K Value { get => _value; }
    }
}

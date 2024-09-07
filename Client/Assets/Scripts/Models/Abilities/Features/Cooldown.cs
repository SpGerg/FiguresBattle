using UnityEngine;

namespace Models.Abilities.Features
{
    public class Cooldown
    {
        public Cooldown(float cooldownInSeconds)
        {
            CooldownInSeconds = cooldownInSeconds;
        }

        private float _time;

        private bool _isUsed;

        public float CooldownInSeconds { get; set; }

        public float RemainingTime => Time.time - _time;

        public bool IsReady => RemainingTime >= CooldownInSeconds && _isUsed;

        public void Use()
        {
            _time = Time.time;
            _isUsed = true;
        }
    }
}

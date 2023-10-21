using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        public IHealthObserver HealthObserver;
        public SpriteRenderer Sprite;
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;

        public float InvincibilityTime = 1;
        private float _timeSpentInvincible = 0;
        private bool _isInvincible;
        private int _nrOfFlickers = 20;
        private float _timeSinceLastFlicker = 0;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            HealthObserver.OnHealthUpdated(currentHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(bool bypassInvincibilityFrames = false)
        {
            if (_isInvincible && !bypassInvincibilityFrames) return;
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
            else
            {
                _isInvincible = true;
            }
            HealthObserver.OnHealthUpdated(currentHP);
        }

        public void Revive()
        {
            while (currentHP < maxHP) Increment();
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement(true);
        }

        void Start()
        {
            currentHP = maxHP;
            HealthObserver.OnMaxHealthUpdated(maxHP, currentHP);
        }

        private void Update()
        {
            if(_isInvincible)
            {
                _timeSinceLastFlicker += Time.deltaTime;
                if (_timeSinceLastFlicker > InvincibilityTime / _nrOfFlickers)
                {
                    _timeSinceLastFlicker -= InvincibilityTime / _nrOfFlickers;
                    Sprite.enabled = !Sprite.enabled;
                }


                _timeSpentInvincible += Time.deltaTime;
                if(_timeSpentInvincible > InvincibilityTime)
                {
                    _timeSpentInvincible = 0;
                    _isInvincible = false;
                    Sprite.enabled = true;
                }

            }
        }
    }
}

using System;
using UnityEngine;

namespace MobaLesson
{
    public class CharacterView : MonoBehaviour
    {
        private readonly int isRunningKey = Animator.StringToHash("IsRunning");

        [SerializeField] Animator _animator;

        [SerializeField] Character _character;

        private void Update()
        {
            if (_character.CurrentVelocity.magnitude > 0.1)
                StartRunning();
            else
                StopRunning();
        }

        private void StartRunning() => _animator.SetBool(isRunningKey, true);

        private void StopRunning() => _animator.SetBool(isRunningKey, false);

    }
}
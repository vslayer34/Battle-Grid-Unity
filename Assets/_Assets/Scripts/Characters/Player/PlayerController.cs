using System;
using BattleGridUnity.Scripts.Input;
using UnityEngine;

namespace BattleGridUnity.Scripts.Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Header("Requried")]
        private InputListener _inputListener;

        [SerializeField]
        private CharacterController _characterController;
        private Vector2 _inputVector;


        [SerializeField]
        private float _characterSpeed = 5.0f;

        [SerializeField]
        private float _jumpHeight = 1.5f;

        [SerializeField]
        private float _gravity = -9.81f;

        private Vector3 _moveDirection;
        private Vector3 _verticalVelocity;




        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            _inputListener.OnMoveInputsPressed += UpdateInputVector;
        }

        private void Update()
        {
            if (_characterController.isGrounded)
            {
                // Apply downward force for stability
                if (_verticalVelocity.y < -2.0f)
                {
                    _verticalVelocity.y = -2.0f;
                }
            }

            _moveDirection.x = _inputVector.x;
            _moveDirection.y = 0.0f;
            _moveDirection.z = _inputVector.y;
            _moveDirection = _moveDirection.normalized;

            // if (_moveDirection != Vector3.zero)
            // {
            //     transform.forward = _moveDirection;
            // }

            // Apply Gravity
            _verticalVelocity.y += _gravity * Time.deltaTime;

            var _finalMoveDirection = _moveDirection * _characterSpeed + Vector3.up * _verticalVelocity.y;
            _characterController.Move(_finalMoveDirection * Time.deltaTime);
        }

        private void OnDestroy()
        {
            _inputListener.OnMoveInputsPressed -= UpdateInputVector;
        }

        // Member Methods--------------------------------------------------------------------------

        // Signal Methods--------------------------------------------------------------------------

        private void UpdateInputVector(Vector2 vector) => _inputVector = vector;

    }
}
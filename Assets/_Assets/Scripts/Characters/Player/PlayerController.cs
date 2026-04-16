using System;
using BattleGridUnity.Scripts.Input;
using UnityEngine;

namespace BattleGridUnity.Scripts.Characters.Player
{
    internal enum CharacterStance
    {
        Standing,
        Crouched,
        Prone
    }
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Header("Requried")]
        private InputListener _inputListener;

        [SerializeField]
        private CharacterController _characterController;
        private Vector2 _inputVector;


        
        [SerializeField]
        private float _characterStandingSpeed = 5.0f;
        private float _characterSpeed;

        [SerializeField]
        private float _jumpHeight = 1.5f;

        [SerializeField]
        private float _gravity = -9.81f;

        private Vector3 _moveDirection;
        private Vector3 _verticalVelocity;

        // Character Controller configuerations
        private float _controllerHeight;
        private Vector3 _controllerCenter;
        private float _controllerRadius;

        const float CROUCH_REDUCTION_RATIO = 0.5f;
        const float PRONE_REDUCTION_RATIO = 0.25f;
        private CharacterStance _stance = CharacterStance.Standing;




        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            _characterSpeed = _characterStandingSpeed;

            _controllerHeight = _characterController.height;
            _controllerCenter = _characterController.center;
            _controllerRadius = _characterController.radius;

            _inputListener.OnMoveInputsPressed += UpdateInputVector;
            _inputListener.OnJumpInputPressed += Jump;
            _inputListener.OnCrouchToggled += CheckForCrouch;
            _inputListener.OnProneToggled += CheckForProne;
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
            _inputListener.OnJumpInputPressed -= Jump;
            _inputListener.OnCrouchToggled -= CheckForCrouch;
            _inputListener.OnProneToggled -= CheckForProne;
        }

        // Member Methods--------------------------------------------------------------------------

        private void UpdateCharacterControllerConfigs(CharacterStance stance)
        {
            float modifier = 0.0f;

            switch (stance)
            {
                case CharacterStance.Standing:
                    modifier = 1.0f;
                    _characterController.radius = _controllerRadius;
                    break;

                case CharacterStance.Crouched:
                    modifier = CROUCH_REDUCTION_RATIO;
                    _characterController.radius = _controllerRadius;
                    break;

                case CharacterStance.Prone:
                    modifier = PRONE_REDUCTION_RATIO;
                    _characterController.radius = _controllerRadius / 2.0f;
                    break;
            }
            _characterController.height = _controllerHeight * modifier;
            _characterController.center = _controllerCenter * modifier;
            _characterSpeed = _characterStandingSpeed * modifier;
        }

        // Signal Methods--------------------------------------------------------------------------

        private void UpdateInputVector(Vector2 vector) => _inputVector = vector;
        private void Jump()
        {
            if (_characterController.isGrounded)
            {
                _verticalVelocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
            }
        }
        private void CheckForCrouch(bool toggledOn)
        {
            switch (toggledOn)
            {
                case true:
                    UpdateCharacterControllerConfigs(CharacterStance.Crouched);
                    break;
                
                case false:
                    UpdateCharacterControllerConfigs(CharacterStance.Standing);
                    break;
            }
        }

        private void CheckForProne(bool toggledOn)
        {
            switch (toggledOn)
            {
                case true:
                    UpdateCharacterControllerConfigs(CharacterStance.Prone);
                    break;
                
                case false:
                    UpdateCharacterControllerConfigs(CharacterStance.Standing);
                    break;
            }
        }
    }
}
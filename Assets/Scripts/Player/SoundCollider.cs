
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundCollider : MonoBehaviour {
     
    //Initialization of renderer with movement controller
    [Header("Components")]
    [SerializeField] private new GameObject renderer;
    [SerializeField] private MovementV2 movementV2;

    //Initialization of 3 different radius of sound
    [Header("Variables")]
    [SerializeField] private float baseMaxNoise;
    [SerializeField] private float sneakingNoiseMultiplier;
    [SerializeField] private float runningNoiseMultiplier;
    
    //Initialization of states with speed and direction
    private MovementV2.PlayerMovementState _currentPlayerMovementState;
    private Vector2 _velocityVector;
    private Vector2 _currentScale;
    private float _currentMaxNoise;
    private float _horizontalDirection;
    private float _verticalDirection;
    
    private void Update() {
        //Gets the direction of the movement
        _horizontalDirection = movementV2.GetHorizontalDirection();
        _verticalDirection = movementV2.GetVerticalDirection();
    }

    private void FixedUpdate() {
        UpdatePlayerNoise();
    }

    private void UpdatePlayerNoise() {
        //Gets movement state with velocity with renderer change scale
        _currentPlayerMovementState = movementV2.GetPlayerMovementState();
        _velocityVector = new Vector2(_horizontalDirection, _verticalDirection).normalized * 100f;
        renderer.transform.localScale = _velocityVector;

        //Switches the movement state
        _currentMaxNoise = _currentPlayerMovementState switch {
            MovementV2.PlayerMovementState.Sneaking => baseMaxNoise * sneakingNoiseMultiplier,
            MovementV2.PlayerMovementState.Walking => baseMaxNoise,
            MovementV2.PlayerMovementState.Running => baseMaxNoise * runningNoiseMultiplier,
            _ => throw new ArgumentOutOfRangeException()
        };

        //Renderer change scale based in velocity (x,y)
        if (Mathf.Abs(_velocityVector.x) > _currentMaxNoise || Mathf.Abs(_velocityVector.y) > _currentMaxNoise) {
            _currentScale = new Vector2(  Mathf.Sign( _velocityVector.x) * _currentMaxNoise, Mathf.Sign(_velocityVector.y) * _currentMaxNoise);
            renderer.transform.localScale = _currentScale;
        }
    }
}

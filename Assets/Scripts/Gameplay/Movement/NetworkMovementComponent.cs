using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace NOBRAIN.KAPUTT.Gameplay.Movement {
    public class NetworkMovementComponent : NetworkBehaviour{
        [SerializeField] private CharacterController _cc;
        [SerializeField] private float _speed;
        [SerializeField] private float _turnSpeed;

        [SerializeField] private Transform _camSocket;
        [SerializeField] private GameObject _cam;
        [SerializeField] private Animator animator;

        private Transform _camTransform;

        private int _tick = 0;
        private float _tickRate = 1f/60f;
        private float _tickDeltaTime = 0f;

        private const int BUFFER_SIZE = 1024;
        private MovementInputState[] _inputStates = new MovementInputState[BUFFER_SIZE];
        private TransformState[] _transformStates = new TransformState[BUFFER_SIZE];

        public NetworkVariable<TransformState> ServerTransformState = new NetworkVariable<TransformState>();
        public TransformState _PreviousTransformState;

        void Start(){
            _cc = GetComponent<CharacterController>();
            _camTransform = _cam.transform;
            animator = GetComponent<Animator>();
        }

        private void OnEnable(){
            ServerTransformState.OnValueChanged += OnServerTransformStateChange;
        }

        private void OnServerTransformStateChange(TransformState previousValue, TransformState serverState){
            if(!IsLocalPlayer) return;
            if(_PreviousTransformState == null) _PreviousTransformState = serverState;

            TransformState calculatedState = _transformStates[serverState.Tick % BUFFER_SIZE];
            if(calculatedState.Position != serverState.Position){
                TeleportPlayer(serverState);

                IEnumerable<MovementInputState> inputs = _inputStates.Where(input => input.Tick > serverState.Tick);
                inputs = from input in inputs orderby input.Tick select input;
                foreach(MovementInputState input in inputs){
                    MovePlayer(input.movementInput);
                    RotatePlayer(input.lookInput);

                    TransformState newState = new TransformState(){
                        Tick = input.Tick,
                        Position = transform.position,
                        Rotation = transform.rotation,
                        HasStartedMoving = true
                    };


                    for(int i = 0; i < _transformStates.Length; i++){
                        if(_transformStates[i].Tick == input.Tick){
                            _transformStates[i] = newState;
                            break;
                        }
                    }
                }
            }
        }


        private void TeleportPlayer(TransformState state){
            _cc.enabled = false;
            transform.position = state.Position;
            transform.rotation = state.Rotation;
            _cc.enabled = true;

            for(int i = 0; i < _transformStates.Length;i++){
                if(_transformStates[i].Tick == state.Tick){
                    _transformStates[i] = state;
                    break;
                }
            }
        }

        public void ProcessLocalPlayerMovement(Vector2 movementInput, Vector2 lookInput){
            int bufferedIndex = _tick % BUFFER_SIZE;
            if(!IsServer){
                MovePlayerServerRpc(_tick, movementInput, lookInput);
                MovePlayer(movementInput);
                RotatePlayer(lookInput);
                SaveState(movementInput, lookInput, bufferedIndex);
            } else {
                MovePlayer(movementInput);
                RotatePlayer(lookInput);

                TransformState state = new TransformState(){
                    Tick = _tick,
                    Position = transform.position,
                    Rotation = transform.rotation,
                    HasStartedMoving = true
                };

                SaveState(movementInput, lookInput, bufferedIndex);

                _PreviousTransformState = ServerTransformState.Value;
                ServerTransformState.Value = state;
            }

            _tick++;
        }

        void SaveState(Vector2 movement, Vector2 look, int bufferIndex){
            MovementInputState inputState = new MovementInputState(){
                Tick = _tick,
                movementInput = movement,
                lookInput = look
            };

            TransformState transformState = new TransformState{
                Tick = _tick,
                Position = transform.position,
                Rotation = transform.rotation,
                HasStartedMoving = true
            };

            _inputStates[bufferIndex] = inputState;
            _transformStates[bufferIndex] = transformState;
        }
        public void ProcessSimulatedPlayerMovement(){
            _tickDeltaTime += Time.deltaTime;
            if (_tickDeltaTime > _tickRate){
                if(ServerTransformState.Value.HasStartedMoving){
                    transform.position = ServerTransformState.Value.Position;
                    transform.rotation = ServerTransformState.Value.Rotation;
                }

                _tickDeltaTime -= _tickRate;
                _tick++;
            }
        }

        private void MovePlayer(Vector2 movementInput){
            Vector3 movement = movementInput.x * _camTransform.right + movementInput.y * _camTransform.forward;

            movement.y = 0;

            if(!_cc.isGrounded){
                movement.y -= 9.81f;
            }

            _cc.Move(movement * _speed * _tickRate);
        }

        private void RotatePlayer(Vector2 lookInput){
            _camTransform.RotateAround(_camTransform.position, _camTransform.right, -lookInput.y * _turnSpeed * _tickRate);
            transform.RotateAround(transform.position, transform.up, lookInput.x * _turnSpeed * _tickRate);
        }

        Vector2 smoothBlended;
        Vector2 animationVelocity;
        [SerializeField] private float animationSmoothTime = 0.1f;

        [ServerRpc]
        private void MovePlayerServerRpc(int tick, Vector2  movementInput, Vector2 lookInput){
            MovePlayer(movementInput);
            RotatePlayer(lookInput);

            TransformState state = new TransformState(){
                Tick = tick,
                Position = transform.position,
                Rotation = transform.rotation,
                HasStartedMoving = true
            };

            _PreviousTransformState = ServerTransformState.Value;
            ServerTransformState.Value = state;

            smoothBlended = Vector2.SmoothDamp(smoothBlended, movementInput.normalized, ref animationVelocity, animationSmoothTime);
            animator.SetFloat("speed_x", smoothBlended.x);
            animator.SetFloat("speed_y", smoothBlended.y);
        }

    }

    
}
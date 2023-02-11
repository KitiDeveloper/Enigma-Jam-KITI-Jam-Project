using UnityEngine;
using UnityEngine.Assertions;

namespace Units.RayCaster {

    public abstract class BasePlayerRayCaster : MonoBehaviour {

        protected Camera _playerCamera;

        protected float _currentCastTime;

        [SerializeField]
        protected float _startCastTime = 0.1f;

        [SerializeField]
        protected LayerMask _interactableLayerMask;

        [SerializeField]
        protected Vector3 _castOffset = Vector3.zero;

        [SerializeField]
        protected float _castDistance = 3f;

        protected virtual void Start() {
            _playerCamera = GetComponentInChildren<Camera>();
            _currentCastTime = _startCastTime;
            Assert.IsNotNull(_playerCamera);
        }


        protected abstract void HandleInteractionCheck();

        protected virtual RaycastHit CastForObject() {
            if (Physics.Raycast(_playerCamera.transform.position + _castOffset, _playerCamera.transform.forward, out var hit,
                    _castDistance, _interactableLayerMask)) {
            }
            return hit;
        }
    }

}
using UnityEngine;
using UnityEngine.Assertions;

public class ObjectInteracter : MonoBehaviour {


    private Interactable _currentInteractable;

    //todo: why do we need this?
    private bool _canInteract = true;

    private Camera _playerCamera;

    private float _currentCastTime;

    [SerializeField]
    private float _startCastTime = 0.1f;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private Vector3 _interactionRayPoint = Vector3.zero;

    [SerializeField]
    private float _interactionDistance = 3f;

    [SerializeField]
    private KeyCode _interactKey = KeyCode.Mouse0;

    private void Awake() {
        _playerCamera = GetComponentInChildren<Camera>();
        _currentCastTime = _startCastTime;
        Assert.IsNotNull(_playerCamera);
    }

    private void Update() {
        if (!_canInteract) return;

        HandleInteractionInput();

        if (_currentCastTime <= 0) {
            _currentCastTime = _startCastTime;
            HandleInteractionCheck();
        } else {
            _currentCastTime -= Time.deltaTime;
        }
    }


    private void HandleInteractionInput() {
        if (Input.GetKeyDown(_interactKey) && _currentInteractable != null) {
            _currentInteractable.OnInteract();
        }
    }

    private void HandleInteractionCheck() {
        var hitObject = CastForObject();
        if (hitObject.collider != null) {
            TryFocusOnObject(hitObject);
        } else if (_currentInteractable != null) {
            LoseFocusOfCurrentObject();
        }
    }

    private void TryFocusOnObject(RaycastHit hitObject) {
        if (HitNewObject(hitObject)) {
            hitObject.collider.TryGetComponent(out _currentInteractable);

            if (_currentInteractable) {
                _currentInteractable.OnFocus();
            }
        }
    }

    private bool HitNewObject(RaycastHit hitObject) {
        return _currentInteractable == null ||
               hitObject.collider.gameObject != _currentInteractable.gameObject;
    }

    private void LoseFocusOfCurrentObject() {
        _currentInteractable.OnLoseFocus();
        _currentInteractable = null;
    }

    private RaycastHit CastForObject() {
        if (Physics.Raycast(transform.position + _interactionRayPoint, transform.forward, out var hit,
                _interactionDistance, _layerMask)) {
        }
        return hit;
    }
}
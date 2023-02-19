using UnityEngine;

namespace Units.RayCaster {

    public class ObjectInteracter : BasePlayerRayCaster {


        private Interactable _currentInteractable;

        public bool CanInteract { get; set; } = true;

        [SerializeField]
        private KeyCode _interactKey = KeyCode.Mouse0;

        private void Update() {
            if (!CanInteract) return;

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
                // Debug.Log($"interracting with {_currentInteractable.transform.name}");
                _currentInteractable.OnInteract();
            }
        }

        protected override void HandleInteractionCheck() {
            var hitObject = CastForObject();
            if (hitObject.collider != null) {
                // Debug.Log(hitObject.collider);
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
    }

}
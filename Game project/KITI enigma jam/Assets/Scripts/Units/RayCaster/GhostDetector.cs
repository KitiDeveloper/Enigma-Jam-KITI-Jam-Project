using UnityEngine;

namespace Units.RayCaster {

    public class GhostDetector : BasePlayerRayCaster {

        private void Update() {
            if (_currentCastTime <= 0) {
                _currentCastTime = _startCastTime;
                HandleInteractionCheck();
            } else {
                _currentCastTime -= Time.deltaTime;
            }
        }

        protected override void HandleInteractionCheck() {
            var hitObject = CastForObject();
            if (hitObject.collider != null) {
                InterractWithGhost(hitObject);
            }
        }

        private void InterractWithGhost(RaycastHit raycastHit) {
            raycastHit.transform.TryGetComponent(out Ghost ghost);
            if (ghost != null) {
                ghost.CatchHandle();
            }
        }
    }

}
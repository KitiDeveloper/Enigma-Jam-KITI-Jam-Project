using UnityEngine;

namespace GameObjects.Ghosts {

    public class GhostSpawner : Interactable {

        private bool wasInteracted = false;
        [SerializeField] GhostSpawnPoint ghostSpawnPos;
        
        public override void OnInteract() {
            if(wasInteracted) return;
            Debug.Log("trying to spawn a ghost");
            SpawnGhost();
        }

        private void SpawnGhost() {
            GhostSpawnController.Instance.SpawnGhostAtFixedPos(ghostSpawnPos);
            //todo: remove comments after tests
            // wasInteracted = true;
        }

        public override void OnFocus() {
            Debug.Log("Looking at ghost spawner");
        }

        public override void OnLoseFocus() {
            Debug.Log("Stop Looking at ghost spawner");
        }
    }

}
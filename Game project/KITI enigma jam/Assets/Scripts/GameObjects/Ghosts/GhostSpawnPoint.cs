using System;
using UnityEngine;

namespace GameObjects.Ghosts {

    public class GhostSpawnPoint : MonoBehaviour {

        [SerializeField]
        private SpawnLocationType _locationType;

        public Vector3 GetPointPosition() {
            return transform.position;
        }

        private void Start() {
            GhostSpawnController.Instance.RegisterSpawnPoint(this);
        }
    }

}
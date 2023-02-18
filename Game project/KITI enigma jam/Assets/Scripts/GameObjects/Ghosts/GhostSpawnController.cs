using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameObjects.Ghosts {

    public class GhostSpawnController : MonoBehaviour {

        private List<GhostSpawnPoint> _spawnPoints = new();

        [SerializeField]
        private GameObject _ghostPrefab;

        public static GhostSpawnController Instance { get; set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }

        public void RegisterSpawnPoint(GhostSpawnPoint point) {
            _spawnPoints.Add(point);
        }

        private GhostSpawnPoint GetRandomSpawnPoint() {
            if (_spawnPoints.Count <= 0) {
                Debug.LogException(new Exception("There is no SpawnPoint for Ghosts!!!"));
            }
            return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        }

        public void SpawnGhostInRandomPoint() {
            var ghost = Instantiate(_ghostPrefab, GetRandomSpawnPoint().transform.position, Quaternion.identity);
            //todo: you can handle ghost settings here if needed
        }

        public void SpawnGhostAtFixedPos(GhostSpawnPoint point)
        {
            var ghost = Instantiate(_ghostPrefab, point.transform.position, Quaternion.identity);
            //todo: you can handle ghost settings here if needed
        }
    }

}
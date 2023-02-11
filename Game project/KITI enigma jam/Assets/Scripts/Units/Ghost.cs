using System;
using System.Collections;
using UnityEngine;

namespace Units {

    [RequireComponent(typeof(MeshRenderer))]
    public class Ghost : MonoBehaviour {

        private bool _wasCaught;

        private Color _colorCache;

        private MeshRenderer _meshRenderer;

        private GhostFlashUI _ghostFlashUI;

        [SerializeField]
        private float _fadeTime = 5f;

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start() {
            _ghostFlashUI = GhostFlashUI.Instance;
        }

        public void CatchHandle() {
            if (_wasCaught) return;

            Debug.Log("Player have catch the ghost!");
            _wasCaught = true;

            _meshRenderer.enabled = true;
            _colorCache = _meshRenderer.material.color;
            _ghostFlashUI.Flash();
            StartCoroutine(FadeAway(_ghostFlashUI.flashTime));
        }

        private IEnumerator FadeAway(float flashTime) {
            Debug.Log("entering cor");
            yield return new WaitForSeconds(flashTime/2);

            Color colorTmp = _meshRenderer.material.color;
            float startAlpha = colorTmp.a;


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / _fadeTime) {
                colorTmp.a = Mathf.Lerp(startAlpha, 0f, t);
                _meshRenderer.material.color = colorTmp;
                yield return null;
            }

            Die();
        }

        private void Die() {
            Destroy(gameObject, 1f);
        }
    }

}
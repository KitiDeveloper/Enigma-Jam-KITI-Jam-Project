using System;
using UnityEngine;

namespace GameObjects.Puzzle
{
    public class TimeSkipEffect : MonoBehaviour
    {
        [SerializeField] private MeshRenderer distortionRenderer;
        private static readonly int Distortion = Shader.PropertyToID("_Distortion");
        private float distortionProgress;
        private readonly float distortionEffectTime = 0.5f;

        public void StartEffect()
        {
            distortionProgress = distortionEffectTime;
        }

        private void Update()
        {
            distortionProgress = Math.Max(0, distortionProgress - Time.deltaTime);
            var d = Mathf.Lerp(0.0f, 0.5f, distortionProgress / distortionEffectTime);
            distortionRenderer.materials[0].SetFloat(Distortion, d);
        }
    }
}
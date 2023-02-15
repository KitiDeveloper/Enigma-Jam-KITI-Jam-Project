using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameObjects.Puzzle
{
    [Serializable]
    public class HideByClock
    {
        [SerializeField] internal int[] statesToShowIn;
        [SerializeField] internal GameObject gameObject;
    }

    [Serializable]
    public class ClockState
    {
        [SerializeField] internal GameObject clockObject;
        [SerializeField] internal Material skybox;

        internal void SetActive(bool active)
        {
            clockObject.SetActive(active);
            if (active)
            {
                RenderSettings.skybox = skybox;
                DynamicGI.UpdateEnvironment();
            }
        }
    }
    
    public class ClockInteractable : Interactable
    {
        [SerializeField] private float cooldownBetweenSkips = 1.0f;
        [SerializeField] private TimeSkipEffect timeSkipEffect;
        [SerializeField] private ClockState[] clockStates;
        [SerializeField] private int state;
        [SerializeField] private HideByClock[] hideByClockObjects;
        private float cooldown;

        private void Update()
        {
            cooldown = Math.Max(0, cooldown - Time.deltaTime);
        }

        private void Start()
        {
            clockStates[state].SetActive(true);
            for (int i = 0; i < clockStates.Length; i++)
            {
                if (i != state)
                {
                    clockStates[i].SetActive(false);
                }
            }
            UpdateHiddenObjects();
        }

        public override void OnInteract()
        {
            if (cooldown > 0) return;
            cooldown = cooldownBetweenSkips;
            clockStates[state].SetActive(false);
            state = (state + 1) % clockStates.Length;
            clockStates[state].SetActive(true);
            UpdateHiddenObjects();
            timeSkipEffect.StartEffect();
        }

        private void UpdateHiddenObjects()
        {
            foreach (var hideByClockObject in hideByClockObjects)
            {
                var shouldShow = hideByClockObject.statesToShowIn.Contains(state);
                hideByClockObject.gameObject.SetActive(shouldShow);
            }
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameObjects.Puzzle
{
    [Serializable]
    public class PuzzleState
    {
        [SerializeField] internal int timeState;
        [SerializeField] internal int progressState;
        [SerializeField] internal GameObject[] gameObjectsToShow;
        [SerializeField] internal GameObject[] gameObjectToHide;
    }

    [Serializable]
    public class ClockTimeState
    {
        [SerializeField] internal Material skybox;

        internal void SetActive(bool active)
        {
            if (active)
            {
                RenderSettings.skybox = skybox;
                DynamicGI.UpdateEnvironment();
            }
        }
    }
    
    public class ClockPuzzle : Interactable
    {
        [SerializeField] private float cooldownBetweenSkips = 1.0f;
        [SerializeField] private TimeSkipEffect timeSkipEffect;
        [SerializeField] private ClockTimeState[] clockStates;
        [SerializeField] private ClockInteractableState[] clockInteractables;
        [FormerlySerializedAs("state")] [SerializeField] private int timeState;
        [SerializeField] private int puzzleProgressState;
        [SerializeField] private PuzzleState[] puzzleStatesDefinition;
        private Dictionary<Tuple<int, int>, PuzzleState> puzzleStates;
        private float cooldown;

        private void Update()
        {
            cooldown = Math.Max(0, cooldown - Time.deltaTime);
        }

        private void Start()
        {
            puzzleStates = puzzleStatesDefinition.ToDictionary((state) => new Tuple<int, int>(state.progressState, state.timeState));
            clockStates[timeState].SetActive(true);
            for (int i = 0; i < clockStates.Length; i++)
            {
                if (i != timeState)
                {
                    clockStates[i].SetActive(false);
                }
            }
            
            foreach (var clockInteractableState in clockInteractables)
            {
                clockInteractableState.SetParent(this);
            }
            
            UpdateHiddenObjects();
        }

        public void IncreasePuzzleProgress()
        {
            puzzleProgressState++;
        }

        public override void OnInteract()
        {
            if (cooldown > 0) return;
            cooldown = cooldownBetweenSkips;
            clockStates[timeState].SetActive(false);
            timeState = (timeState + 1) % clockStates.Length;
            clockStates[timeState].SetActive(true);
            UpdateHiddenObjects();
            timeSkipEffect.StartEffect();
        }

        private void UpdateHiddenObjects()
        {
            PuzzleState puzzleState = puzzleStates[new Tuple<int, int>(puzzleProgressState, timeState)];
            foreach (var o in puzzleState.gameObjectsToShow)
            {
                o.SetActive(true);
            }
            foreach (var o in puzzleState.gameObjectToHide)
            {
                o.SetActive(false);
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
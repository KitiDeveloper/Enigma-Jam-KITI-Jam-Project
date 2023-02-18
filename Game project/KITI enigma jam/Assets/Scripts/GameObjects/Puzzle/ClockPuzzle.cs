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
        [SerializeField] internal Color fogColor;

        internal void SetActive(bool active)
        {
            if (active)
            {
                RenderSettings.skybox = skybox;
                RenderSettings.fogColor = fogColor;
                DynamicGI.UpdateEnvironment();
            }
        }
    }
    
    public class ClockPuzzle : MonoBehaviour
    {
        private float cooldownBetweenSkips = 0.5f;
        [SerializeField] private TimeSkipEffect timeSkipEffect;
        [SerializeField] private ClockTimeState[] clockStates;
        [FormerlySerializedAs("state")] [SerializeField] private int timeState;
        [SerializeField] private int puzzleProgressState;
        [SerializeField] private PuzzleState[] puzzleStatesDefinition;
        private Dictionary<Tuple<int, int>, PuzzleState> puzzleStates;
        private float cooldown;
        private int previousTimeState;

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
            
            UpdateHiddenObjects();
        }

        public void SetPuzzleProgress(int progress)
        {
            puzzleProgressState = progress;
        }

        public bool SwitchTime()
        {
            return SetTime((timeState + 1) % clockStates.Length);
        }

        public bool SetTime(int time)
        {
            if (cooldown > 0) return false;
            if (time > clockStates.Length || time < 0) throw new Exception("Invalid time state");
            cooldown = cooldownBetweenSkips;
            previousTimeState = timeState;
            timeState = time;
            return true;
        }

        public void UpdateObjects()
        {
            clockStates[previousTimeState].SetActive(false);
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
    }
}
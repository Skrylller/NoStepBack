using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatSceneController : MonoBehaviour
{
    [System.Serializable]
    public class AnimationInCatScene
    {
        public Animator animator;
        public int state;
    }

    [System.Serializable]
    public class Stage
    {
        public List<UnityEvent> _events = new List<UnityEvent>();
        public List<AnimationInCatScene> animations = new List<AnimationInCatScene>();
    }

    [SerializeField] private List<Stage> _stages = new List<Stage>();

    public List<UnityEvent> _eventsEnd = new List<UnityEvent>();

    private int _numStage;

    public void StartCatScene()
    {
        StartStage(0);
    }

    public void StartStage(int num)
    {
        _numStage = num;
        foreach(UnityEvent ev in _stages[_numStage]._events)
        {
            ev?.Invoke();
        }

        foreach(AnimationInCatScene an in _stages[_numStage].animations)
        {
            an.animator.SetInteger("state", an.state);
        }
    }

    public void NextStage()
    {
        _numStage++;
        if (_numStage >= _stages.Count)
        {
            EndCatScene();
        }
        else
        {
            StartStage(_numStage);
        }
    }

    public void EndCatScene()
    {
        foreach (UnityEvent ev in _eventsEnd)
        {
            ev?.Invoke();
        }
    }
}

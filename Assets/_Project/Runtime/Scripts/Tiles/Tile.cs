using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;


public class Tile : MonoBehaviour
{
    [SerializeField] private TILE_TYPE _TILE_TYPE;
    
    //Action
    //public event UnityAction OnTriggerEntered;
    public event UnityAction OnTileEffectDone;

    //UnityEvent
    //[SerializeField] protected UnityEvent OnTriggerEnteredEvent;
    [SerializeField] protected UnityEvent OnTileEffectDoneEvent;

    //Properties
    public TILE_TYPE TILE_TYPE => _TILE_TYPE;

    private void Awake()
    {
        //OnTriggerEnteredEvent.AddListener(()=>OnTriggerEntered?.Invoke());
        OnTileEffectDoneEvent.AddListener(()=>OnTileEffectDone?.Invoke());
    }

    [Button]
    public virtual void DoTileEffect()
    {
        OnTileEffectDoneEvent?.Invoke();
    }
}

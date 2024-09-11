using System;
using UnityEngine;
using UnityEngine.Events;


public class Tile : MonoBehaviour
{
    [SerializeField] private TILE_TYPE _TILE_TYPE;
    
    //Action
    public event UnityAction OnTriggerEntered;

    //UnityEvent
    [SerializeField] protected UnityEvent OnTriggerEnteredEvent;

    private void Awake()
    {
        OnTriggerEnteredEvent.AddListener(()=>OnTriggerEntered?.Invoke());
    }
}

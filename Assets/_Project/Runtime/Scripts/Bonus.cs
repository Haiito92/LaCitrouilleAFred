using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bonus : MonoBehaviour
{
    [SerializeField] private int _bonusValue;
    [SerializeField] private UnityEvent _onBonusPickUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterBeheviour>())
        {
            GameManager.Instance.TimerGame -= _bonusValue;
            _onBonusPickUp.Invoke();
        }
    }
}

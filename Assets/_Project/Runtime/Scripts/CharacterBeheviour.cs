using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBeheviour : MonoBehaviour
{
    [SerializeField] GameObject _gridPrefab;
    [SerializeField] PlayerInput _playerController;
    [SerializeField] InputActionReference _move;

    private int _layers;
    private Vector2 _Direction;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        // _playerController = new PlayerInput();
        _layers = LayerMask.NameToLayer("wall");

    }

    private void Moving(InputAction.CallbackContext ctx)
    {
        var dir = ctx.ReadValue<Vector2>();
        RaycastHit2D raycasthit = Physics2D.Raycast(transform.position, dir, 1f, LayerMask.GetMask(new string[] { "wall" }));
        Debug.DrawRay(transform.position, dir, Color.red, 1);
        if (raycasthit.collider != null)
        {
            Debug.Log("wall");
        }
        else
        {
            this.transform.Translate(dir);

            Collider2D tileCollision = Physics2D.OverlapCircle(transform.position, .1f);
            if (tileCollision != null && tileCollision.TryGetComponent(out Tile tile))
            {
                switch(tile.TILE_TYPE)
                {
                    case TILE_TYPE.ICE :
                        //slide untile tile is not iced
                        break;
                    case TILE_TYPE.DOUBLE_INPUT :
                        //1 input = 2 so move 2 case away
                        break;
                    case TILE_TYPE.INVERSION :
                        //forward = backward, left = right
                        break;
                }
            }
        }
    }

    private void OnEnable()
    {
        _move.action.started += Moving;
    }
    private void OnDisable()
    {
        _move.action.canceled -= Moving;
    }
}

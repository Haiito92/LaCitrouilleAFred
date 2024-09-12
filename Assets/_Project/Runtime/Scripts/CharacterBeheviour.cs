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
    [SerializeField] InputActionReference _inversion;

    private int _movingDistance = 1;
    private int _layers;
    private bool _isSubToMoving = true;
    private Vector2 _oldDirection;
    private Coroutine _doMovmentCoroutine;
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
        RaycastHit2D raycasthit = Physics2D.Raycast(transform.position, dir, _movingDistance, LayerMask.GetMask(new string[] { "wall" }));
        Debug.DrawRay(transform.position, dir, Color.red, _movingDistance);
        if (raycasthit.collider != null)
        {
            Debug.Log("wall");
        }
        else
        {

            if (_doMovmentCoroutine == null)
            {
                //Debug.Log("StartMovement");
                _doMovmentCoroutine = StartCoroutine(DoMovement(dir));
            }

            /*this.transform.Translate(dir);
            _oldDirection = dir;

            Collider2D tileCollision = Physics2D.OverlapCircle(transform.position, .1f);
            if (tileCollision != null && tileCollision.TryGetComponent(out Tile tile))
            {
                switch(tile.TILE_TYPE)
                {
                    case TILE_TYPE.ICE :
                        //slide untile tile is not iced
                        Slide();
                        break;
                    case TILE_TYPE.DOUBLE_INPUT :
                        //1 input = 2 so move 2 case away
                        _movingDistance = 2;
                        break;
                    case TILE_TYPE.INVERSION :
                        //forward = backward, left = right
                        Inversion();
                        break;
                }
            }*/
        }
    }

    private void Slide()
    {
        //Debug.Log("slide");
        var dir = _oldDirection;
        RaycastHit2D raycasthit = Physics2D.Raycast(transform.position, dir, _movingDistance, LayerMask.GetMask(new string[] { "wall" }));
        Debug.DrawRay(transform.position, dir, Color.red, _movingDistance);
        if (raycasthit.collider != null)
        {
            Debug.Log("wall");
        }
        else
        {
            if (_doMovmentCoroutine == null)
            {
                //Debug.Log("DoSlide");
                _doMovmentCoroutine = StartCoroutine(DoMovement(dir));
            }
        }
    }


    IEnumerator DoMovement(Vector2 direction)
    {
        Vector2 _Destination = (Vector2)transform.position + direction * _movingDistance;
        while (Vector2.Distance((Vector2)transform.position, _Destination) > 0.03f)
        {
            transform.position = (Vector2)transform.position + direction * _movingDistance * Time.deltaTime;
            //Debug.Log("Moving");
            yield return null;
        }
        transform.position = _Destination;
        _oldDirection = direction;
        //Debug.Log("endMovement");
        EndDoMovement();
    }

    private void EndDoMovement()
    {
        StopCoroutine(_doMovmentCoroutine);
        _doMovmentCoroutine = null;

        Collider2D tileCollision = Physics2D.OverlapCircle(transform.position, .1f);
        if (tileCollision != null && tileCollision.TryGetComponent(out Tile tile))
        {
            Debug.Log("FoundTile");
            switch (tile.TILE_TYPE)
            {
                case TILE_TYPE.ICE:
                    //slide untile tile is not iced
                    Slide();
                    break;
                case TILE_TYPE.DOUBLE_INPUT:
                    //1 input = 2 so move 2 case away
                    _movingDistance = 2;
                    break;
                case TILE_TYPE.INVERSION:
                    //forward = backward, left = right
                    Inversion();
                    break;
                case TILE_TYPE.DEATH:
                    tile.DoTileEffect();
                    break;
                case TILE_TYPE.HOLE:
                    tile.DoTileEffect();
                    break;
                case TILE_TYPE.STAIR:
                    tile.DoTileEffect();
                    break;
                case TILE_TYPE.NOT_A_TILE:
                    //error
                    Debug.LogWarning("This is not a tile");
                    break;

            }
        }
    }

    private void Inversion()
    {
        Debug.Log("Doinversion");
        if(_isSubToMoving)
        {
            _move.action.started -= Moving;
            _inversion.action.started += Moving;
        }
        else
        {
            _inversion.action.started -= Moving;
            _move.action.started += Moving;
        }
        _isSubToMoving = !_isSubToMoving;
    }

    private void OnEnable()
    {
        _move.action.started += Moving;
    }
    private void OnDisable()
    {
        _move.action.started -= Moving;
        _inversion.action.started -= Moving;
    }
}

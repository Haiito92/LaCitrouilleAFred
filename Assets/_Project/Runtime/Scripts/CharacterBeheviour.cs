using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CharacterBeheviour : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;

    [SerializeField] GameObject _gridPrefab;
    [SerializeField] PlayerInput _playerController;
    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _inversion;
    [SerializeField] StatusReport statusReport;

    //UnityActions
    public event UnityAction<int> OnDoubleInputChargesChanged;

    //Unity Events
    [SerializeField] private UnityEvent _onGoingUp;
    [SerializeField] private UnityEvent _onGoingDown;
    [SerializeField] private UnityEvent _onGoingLeft;
    [SerializeField] private UnityEvent _onGoingRight;
    [SerializeField] private UnityEvent _onHittingWall;
    [SerializeField] private UnityEvent _onMisteryObject;
    [SerializeField] private UnityEvent _onFalling;

    //Movement
    [SerializeField] private LayerMask _whatIsTile;
    private int _movingDistance = 1;
    [SerializeField] private float _movingSpeed = 1.0f;
    private bool _isSubToMoving = true;
    private Vector2 _oldDirection;
    private Coroutine _doMovmentCoroutine;

    //Double Input
    private int _doubleInputCharges = 0;

    public int DoubleInputCharges
    {
        get => _doubleInputCharges;
        set
        {
            _doubleInputCharges = value;
            OnDoubleInputChargesChanged?.Invoke(_doubleInputCharges);
        }
    }
    [SerializeField] private int _doubleInputChargesGivenByDoubleInputTile = 4;

    private Rigidbody2D _rb;

    //Animator
    [Header("Animator")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _idleStateName;
    [SerializeField] private string _movingStateName;
    [SerializeField] private string _directionXParameterName;
    [SerializeField] private string _directionYParameterName;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        // _playerController = new PlayerInput();
    }

    private void Moving(InputAction.CallbackContext ctx)
    {
        var dir = ctx.ReadValue<Vector2>();

        RaycastHit2D raycasthit;
        if (DoubleInputCharges > 0)
        {
            raycasthit = Physics2D.Raycast(transform.position, dir, _movingDistance * 2, LayerMask.GetMask(new string[] { "wall" }));
        }
        else
        {
            raycasthit = Physics2D.Raycast(transform.position, dir, _movingDistance, LayerMask.GetMask(new string[] { "wall" }));
        }
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
                if (DoubleInputCharges > 0)
                {
                    DoubleInputCharges -= 1;
                    _doMovmentCoroutine = StartCoroutine(DoMovement(dir, _movingDistance * 2));
                }
                else
                {
                    _doMovmentCoroutine = StartCoroutine(DoMovement(dir, _movingDistance));
                }
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
            _onHittingWall.Invoke();
            FindObjectOfType<AudioManager>().Play("Wall_hit");
            Debug.Log("wall");
        }
        else
        {
            if (_doMovmentCoroutine == null)
            {
                //Debug.Log("DoSlide");
                _doMovmentCoroutine = StartCoroutine(DoMovement(dir, _movingDistance));
            }
        }
    }


    IEnumerator DoMovement(Vector2 direction, int distance)
    {
        switch (direction.x, direction.y)
        {
            case (0f, 1f):
                FindObjectOfType<AudioManager>().Play("Up");
                _onGoingUp.Invoke();
                break;
            case (0f, -1f):
                FindObjectOfType<AudioManager>().Play("Down");
                _onGoingDown.Invoke();
                break;
            case (1f, 0f):
                FindObjectOfType<AudioManager>().Play("Left");
                _onGoingLeft.Invoke();
                break;
            case (-1f, 0f):
                FindObjectOfType<AudioManager>().Play("Right");
                _onGoingRight.Invoke();
                break;
        }
        _animator.SetFloat(_directionXParameterName, direction.x);
        _animator.SetFloat(_directionYParameterName, direction.y);
        _animator.Play(_movingStateName);

        //FindObjectOfType<AudioManager>().Play("MouvementSound");

        Vector2 _Destination = (Vector2)transform.position + direction * distance;
        while (Vector2.Distance((Vector2)transform.position, _Destination) > 0.03f)
        {
            transform.position = (Vector2)transform.position + direction * _movingSpeed * Time.deltaTime;
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

        _animator.Play(_idleStateName);

        Collider2D tileCollision = Physics2D.OverlapCircle(transform.position, .1f, _whatIsTile);
        //Debug.Log(tileCollision);
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
                    _onMisteryObject.Invoke();
                    //1 input = 2 so move 2 case away
                    _doubleInputCharges = _doubleInputChargesGivenByDoubleInputTile;
                    statusReport.InverseReveal();
                    break;
                case TILE_TYPE.INVERSION:
                    //forward = backward, left = right
                    _onMisteryObject.Invoke();
                    statusReport.DoubleReveal();
                    Inversion();
                    break;
                case TILE_TYPE.DEATH:
                    tile.DoTileEffect();
                    break;
                case TILE_TYPE.HOLE:
                    _onFalling.Invoke();
                    FindObjectOfType<AudioManager>().Play("Fall");
                    tile.DoTileEffect();
                    break;
                case TILE_TYPE.STAIR:
                    Debug.Log("Stair");
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
        if (_isSubToMoving)
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

    private void SubscribeToMove()
    {
        if (_isSubToMoving)
        {
            _inversion.action.started -= Moving;
            _move.action.started += Moving;
        }
        else
        {
            _move.action.started -= Moving;
            _inversion.action.started += Moving;
        }
    }
    private void UnsubscribeToMove()
    {
        if (_isSubToMoving)
        {
            _move.action.started -= Moving;
        }
        else
        {
            _inversion.action.started -= Moving;
        }
    }
    private void OnEnable()
    {
        //_move.action.started += Moving;

        _levelManager.OnLevelResumed += SubscribeToMove;
        _levelManager.OnLevelPaused += UnsubscribeToMove;
    }
    private void OnDisable()
    {
        _move.action.started -= Moving;
        _inversion.action.started -= Moving;

        _levelManager.OnLevelResumed -= SubscribeToMove;
        _levelManager.OnLevelPaused -= UnsubscribeToMove;
    }
}

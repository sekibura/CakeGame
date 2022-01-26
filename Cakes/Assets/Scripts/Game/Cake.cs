using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Cake : MonoBehaviour
{
    public int ID { get; set; }
    public GameManagerScript GameManager { get; set; }

    [SerializeField]
    private Material _defaultMaterial;
    [SerializeField]
    private Material _finalMaterial;
    private List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();
    [SerializeField]
    private Sprite _cakeIcon;

    private Follower _follower;

    //места точек куда перемещать торты
    public Vector3 LeftPosition;
    public Vector3 RightPosition;
    private Vector3 _currentTargetToMove;
    private bool _isMoving = false;

    private string _lastTag = null;

    private float _speedMovingToBox = 5f;
    private Color _color;

    [SerializeField]
    private AudioSource _chooseSound;

    #region Shop Values
    [SerializeField]
    private int _price;
    [SerializeField]
    private string _shopName;
    [SerializeField]
    private string _description;
    [SerializeField]
    private int _profit;
    #endregion

   

    private void Start()
    {
        InitMeshRenderers();
        SetMaterial(_defaultMaterial);
        _color = Color.red;
        
    }
    private void OnEnable()
    {
        if(_follower==null)
            _follower = gameObject.GetComponent<Follower>();
        _follower.enabled = true;
        _lastTag = null;
        _isMoving = false;
        SetMaterial(_defaultMaterial);
    }

    private void Update()
    {
        if (_isMoving)
        {
            float step = _speedMovingToBox * Time.deltaTime;
            _color = Color.yellow;
            transform.position = Vector3.MoveTowards(transform.position, _currentTargetToMove, step);
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (_lastTag == null)
        {
            _lastTag = other.gameObject.tag;
            switch (_lastTag)
            {
                case "OvenZone":
                    ChangeTexture();
                    break;
                case "DestroyZone":
                    DestroyCake();
                    break;
                case "BoxZone":
                    DisableCake();
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ChooseZone"))
        {
            
            ChoozeZone();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _lastTag = null;
        _color = Color.red;
    }

    private void DestroyCake()
    {
      

        //TODO
        GameManager.GameOver();

        gameObject.SetActive(false);
    }

    private void DisableCake()
    {
        gameObject.SetActive(false);
    }

    private void ChangeTexture()
    {
        SetMaterial(_finalMaterial);
    }

    private void ChoozeZone()
    {
        if (_isMoving)
            return;

        _color = Color.green;
        Sides side = Sides.NoSide;
        Sides sideFromOrfer = Sides.NoSide;
#if UNITY_EDITOR
        if(GameManager._autoPlay)
            sideFromOrfer = GameManager.GetOrder(ID);
#endif
        if (SwipeInput.swipedLeft || sideFromOrfer == Sides.Left)
        {
            Debug.Log("SwipeLeft");
            side = Sides.Left;
            _currentTargetToMove = LeftPosition;
            _chooseSound.Play();
        }
        else if(SwipeInput.swipedRight || sideFromOrfer == Sides.Right)
        {
            Debug.Log("SwipeRight");
            side = Sides.Right;
            _currentTargetToMove = RightPosition;
            _chooseSound.Play();
        }
        

        if (side != Sides.NoSide)
        {
            _follower.enabled = false;
            _isMoving = true;
            GameManager.AddToBox(side, ID, GetProfit());
        }

    }

    private void SetMaterial(Material material)
    { 
        foreach (var item in _meshRenderers)
        {
            item.material = material;
        }
    }
    
    private void InitMeshRenderers()
    {
        if (_meshRenderers.Count == 0)
        {
            foreach (Transform child in transform)
            {
                var mesh = child.GetComponent<MeshRenderer>();
                if(mesh!=null)
                    _meshRenderers.Add(mesh);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, 0.3f);

        Vector3 a = new Vector3(transform.position.x, transform.position.y+2, transform.position.z);
        GUIStyle style = new GUIStyle();
        style.fontSize = 100;
        
        Handles.Label(a, ID.ToString(), style);
    }
#endif
    public Sprite GetSprite()
    {
        return _cakeIcon;
    }

    public int GetPrice()
    {
        return _price;
    }
    public int GetProfit()
    {
        return _profit;
    }

    public string GetShopName()
    {
        return _shopName;
    }

    public string GetDescription()
    {
        return _description;
    }

}


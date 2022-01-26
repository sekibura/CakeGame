using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;
using System;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
//    [SerializeField]
    private  List<GameObject> _cakes = new List<GameObject>();
    //private List<ObjectPoolNonSingleton> _pools = new List<ObjectPoolNonSingleton>();
    private readonly int _amoutToPool = 5;

    //лево
    private Queue<List<int>> _leftOrders = new Queue<List<int>>();
    //право
    private Queue<List<int>> _rightOrders = new Queue<List<int>>();
    //список id тортов для спавна
    public List<int> _pipeQueue = new List<int>();
    [SerializeField]
    private PathCreator _mainPath;
  

    [SerializeField]
    private Transform _leftBoxPosition;
    [SerializeField]
    private Transform _rightBoxPosition;

    
    public bool _autoPlay = false;

    private float _deltaS = 5f;
    #region timeDelay
    private float _lastTimeSpawn = 0f;
    private float _timeBetweenSpawn;
    #endregion

    #region speed
    private float _startSpeedMoving = 3f;
    private float _currentSpeedMoving;

    private float _maxSpeedMoving = 10f;
    private float _stepSpeedIncrease = 0.01f;
    #endregion
    private int _stage = 1;

    private bool _isReadyForUpdate = false;

    [SerializeField]
    private ScreenScript _screenLeft;
    [SerializeField]
    private ScreenScript _screenRight;

    private List<int> spawned = new List<int>();
    private List<Follower> spawnedFollower = new List<Follower>();

    #region score and money
    //очки за каждый правильный заказ
    private int _currentScore = 0;
    private int _leftMoney = 0;
    private int _rightMoney = 0;
    [SerializeField]
    private GamePlayUI _gamePlayUI;
    #endregion



    private void Start()
    {
        ScoreSystem.CurrentScore = 0;
       _currentSpeedMoving = _startSpeedMoving;
        _timeBetweenSpawn = _deltaS / _currentSpeedMoving;

        _gamePlayUI.UpdateScoreAndMoney();
        InitCakesResources();
        InitPools();
        InitCakes();
        InitPipeQueue();
        _isReadyForUpdate = true;
    }
    private void InitCakesResources()
    {
        #region load json
        string jsonStr = SaveManager.Instance.Load<string>("ShopItems");
        ShopItemsProxy shopItemsProxy = new ShopItemsProxy();
        Debug.LogWarning("Game manager json = " + jsonStr);
        if (!string.IsNullOrEmpty(jsonStr))
            shopItemsProxy = JsonUtility.FromJson<ShopItemsProxy>(jsonStr);
        else
            Debug.LogWarning("Json empty!");
        #endregion


        #region load gameobjects from resources
        List<GameObject> cakesResources = new List<GameObject>();

        try
        {
            cakesResources = Resources.LoadAll("Cakes", typeof(GameObject)).Cast<GameObject>().ToList();
            if (cakesResources.Count == 0)
                Debug.LogWarning("No cakes resources");

            foreach (var go in cakesResources)
            {
                Debug.Log(go.name);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Proper Method failed with the following exception: ");
            Debug.Log(e);
        }
        #endregion
        

        foreach (var cake in cakesResources)
        {
            var result = shopItemsProxy.ShopItems.Find(x => x.Name == cake.name);
            if (result != null)
            {
                if(result.AlreadyPurchased)
                    _cakes.Add(cake);
                else if (cake.GetComponent<Cake>().GetPrice() == 0)
                {
                    _cakes.Add(cake);
                }
            }
            else
            {
                if (cake.GetComponent<Cake>().GetPrice() == 0)
                {
                    _cakes.Add(cake);
                }
            }
            
        }
        Debug.Log("Cakes count = " + _cakes.Count);
    }
    private void InitCakes()
    {
        var pools = ObjectPool.SharedInstance.GetPools();

        for (int i = 0; i < _cakes.Count; i++)
        {
            foreach (var item in pools[i])
            {
                Cake cake = item.GetComponent<Cake>();
                cake.ID = i;
                cake.GameManager = gameObject.GetComponent<GameManagerScript>();
                cake.LeftPosition = _leftBoxPosition.position;
                cake.RightPosition = _rightBoxPosition.position;

                item.GetComponent<Follower>().SetValues(_mainPath, _currentSpeedMoving);
            }
        }
    }
    private void InitPools()
    {
        ObjectPool.SharedInstance.InitPools(_cakes.ToArray(), _amoutToPool);
    }
    private void InitPipeQueue()
    {
        NewOrder(Sides.Left);
        NewOrder(Sides.Right);
    }
    private void Update()
    {
        if(_isReadyForUpdate)
            InstantinateCakes();
    }
    private List<int> CreateOrder(int maxLengthOrder)
    {
        List<int> order = new List<int>();
        RandomWithoutDublicate(order, (0, _cakes.Count), maxLengthOrder, false);
        return order;
    }
    private void InstantinateCakes()
    {
        if (Time.time > (_lastTimeSpawn + _timeBetweenSpawn))
        {
            if (_pipeQueue.Count == 0)
            {
                //new order
                //NewNewOrder();
                return;
            }


            //choose index
            var summ = _leftOrders.Peek().Concat(_rightOrders.Peek()).Except(spawned).ToList();
            if (summ.Count == 0)
                return;
            int index = 0;
         
           
            int a = UnityEngine.Random.Range(0, summ.Count);
            Debug.Log("length = " + summ.Count + " || " + a);
            int value = summ[a];
            int indexinList = _pipeQueue.IndexOf(value);
            index = value;
            _pipeQueue.RemoveAt(indexinList);


            GameObject cake =  ObjectPool.SharedInstance.GetPooledObject(index);
            if (cake != null)
            {
                spawned.Add(index);
                var follower = cake.GetComponent<Follower>();
                follower.SetSpeed(_currentSpeedMoving);
                spawnedFollower.Add(follower);
                cake.transform.position = new Vector3(100f, 100f, 100f);
                cake.SetActive(true);
                _lastTimeSpawn = Time.time;

            }
            else
            {
                Debug.LogError("no pooled obj");
            }
        }
    }
    public void NewOrder(Sides side)
    {
        //update values!!!!!


        //UpdateSpeedAndTime();
        //_speedMoving++;

        Debug.Log(_pipeQueue.Count + " before: " + PrintList(_pipeQueue));
        if (side == Sides.Left)
        {
            var left = CreateOrder(3);
            _leftOrders.Enqueue(left);
            _pipeQueue.AddRange(left);
            //UpdateScreen();
            
        }
        else
        {
            var right = CreateOrder(3);
            _rightOrders.Enqueue(right);
            _pipeQueue.AddRange(right);
            //UpdateScreen();
        }
        _pipeQueue.Shuffle();
        UpdateScreen();
        Debug.Log(_pipeQueue.Count + " after: " + PrintList(_pipeQueue));
    }
    private void NewNewOrder()
    {
        //update values!!!!!

        //_stage++;
        //_speedMoving++;

        var left = CreateOrder(3);
        var right = CreateOrder(3);

        _leftOrders.Enqueue(left);
        _rightOrders.Enqueue(right);

        var summaryOrder = new List<int>();
        summaryOrder.AddRange(left);
        summaryOrder.AddRange(right);
        summaryOrder.Shuffle();

        _pipeQueue.AddRange(summaryOrder);
        Debug.Log("New order = "+PrintList(summaryOrder));
        UpdateScreen();
    }
    private void UpdateSpeedAndTime()
    {
        _stage++;
        if((_startSpeedMoving + _stage * _stepSpeedIncrease) <_maxSpeedMoving)
        {
            _currentSpeedMoving = _startSpeedMoving + _stage * _stepSpeedIncrease;
            SetSpeedCakes(_currentSpeedMoving);

            _timeBetweenSpawn = _deltaS / _currentSpeedMoving;
        }
        else
        {
            Debug.LogWarning("Max speed ! Time = " + Time.realtimeSinceStartup);
        }

     

        Debug.Log("current speed = "+_currentSpeedMoving+ "\ntime = "+_timeBetweenSpawn);
    }
    public void GameOver()
    {
        Debug.LogWarning("GameOver!");
        _gamePlayUI.GameOver();
        SetPause(true);
        
#if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
    public void AddToBox(Sides side, int ID, int price)
    {
        UpdateSpeedAndTime();
        spawned.Remove(ID);
        Debug.Log("Add to box: " + side + " - " + ID);
        if (side == Sides.Left)
        {
            if (!_leftOrders.Peek().Contains(ID))
                GameOver();

            _leftOrders.Peek().Remove(ID);
            _leftMoney += price;
            if (_leftOrders.Peek().Count == 0)
            {
                Debug.Log("order finished! ");
                AddFinishedOrder(_leftMoney);
                _leftMoney = 0;
                _leftOrders.Dequeue();
                NewOrder(Sides.Left);
            }
        }
        else if (side == Sides.Right)
        {
            if (!_rightOrders.Peek().Contains(ID))
                GameOver();

            _rightOrders.Peek().Remove(ID);
            _rightMoney += price;
            if (_rightOrders.Peek().Count == 0)
            {
                Debug.Log("order finished! ");
                AddFinishedOrder(_rightMoney);
                _rightMoney = 0;
                _rightOrders.Dequeue();
                NewOrder(Sides.Right);
            }
        }
        UpdateScreen();
    }
    //очки за законченный заказ
    private void AddFinishedOrder(int money)
    {
        //_currentScore++;
        ScoreSystem.CurrentScore++;
        ScoreSystem.Instance.AddMoney(money);
        _gamePlayUI.UpdateScoreAndMoney();
        if (ScoreSystem.Instance.SetMaxScore(ScoreSystem.CurrentScore))
        {
            //play anim new max score

            //update screen score
        }
        else
        {
            //update screen score
        }
    }
    private void SetSpeedCakes(float value)
    {
        foreach (var item in spawnedFollower)
        {
            item.SetSpeed(value);
        }
    }
    private void RandomWithoutDublicate(List<int> order, (int,int) range, int count, bool withoutDublicate)
    {
        
        List<int> possible = Enumerable.Range(range.Item1, range.Item2).ToList();

        //if (_leftOrders.Count > 0)
        //{
        //    foreach (var item in _leftOrders.Peek())
        //    {
        //        possible.Remove(item);
        //    }
        //}

        //if (_rightOrders.Count > 0)
        //{
        //    foreach (var item in _rightOrders.Peek())
        //    {
        //        possible.Remove(item);
        //    }
        //}
        
        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, possible.Count);
            order.Add(possible[index]);
            if(withoutDublicate)
                possible.RemoveAt(index);
        }
    }
    public Sides GetOrder(int ID)
    {
        if (_leftOrders.Peek().Contains(ID))
            return Sides.Left;
        else if (_rightOrders.Peek().Contains(ID))
            return Sides.Right;
        else return Sides.NoSide;
    }
    private string PrintList(List<int> l)
    {
        string s = "";
        foreach (var item in l)
        {
            s += item + ", ";
        }
        return s;
    }
    private void UpdateScreen()
    {
        List<Sprite> sprites = new List<Sprite>();
        if (_leftOrders.Count > 0)
        {
            _screenLeft.Set(_leftOrders.Peek());
            InitImagesList(sprites, _leftOrders.Peek());
            _screenLeft.Set(sprites);
        }
        if (_rightOrders.Count > 0)
        {
            _screenRight.Set(_rightOrders.Peek());
            InitImagesList(sprites, _rightOrders.Peek());
            _screenRight.Set(sprites);
        }
            
    }
    private void InitImagesList(List<Sprite> sprites, List<int> ordersID)
    {
        sprites.Clear();
        foreach (var item in ordersID)
        {
            sprites.Add(_cakes[item].GetComponent<Cake>().GetSprite());
        }
    }
    public void SetPause(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            //SetSpeedCakes(0);
            //_isReadyForUpdate = false;
        }
        else
        {
            Time.timeScale = 1;
            //SetSpeedCakes(_currentSpeedMoving);
            //_isReadyForUpdate = true;
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        Debug.Log("restart");
        _gamePlayUI.ToDark();
        StartCoroutine(ReloadScene(1,"GamePlay"));
    }
    public void GoHome()
    {
        Time.timeScale = 1f;
        Debug.Log("home");
        _gamePlayUI.ToDark();
        StartCoroutine(ReloadScene(1,"Main"));
    }
    IEnumerator ReloadScene( float time, string scene)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(scene);
    }
}

public static class ListExtension
{
    public static void Shuffle<T>(this IList<T> list)
    {
        //Debug.Log(list.Count);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        //Debug.Log(list.Count);
    }
}

public enum Sides
{
    Left,
    Right,
    NoSide
}

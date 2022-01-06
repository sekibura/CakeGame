using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private  GameObject[] _cakes;
    //private List<ObjectPoolNonSingleton> _pools = new List<ObjectPoolNonSingleton>();
    private readonly int _amoutToPool = 10;

    //����
    private List<int> _order1 = new List<int>();
    //�����
    private List<int> _order2 = new List<int>();
    //������ id ������ ��� ������
    private List<int> _pipeQueue = new List<int>();
    [SerializeField]
    private PathCreator _mainPath;
    [SerializeField]
    private PathCreator _leftPath;
    [SerializeField]
    private PathCreator _rightPath;

    private float _timeBetweenSpawn = 0f;
    private float _lastTimeSpawn = 0f;
    private float _speedMoving = 0.5f;
    private int _stage = 1;
  

    private void Start()
    {
        InitCakes();
        InitPools();
        InitPipeQueue();
        
    }

    private void InitCakes()
    {
        for (int i = 0; i < _cakes.Length; i++)
        {
            _cakes[i].GetComponent<Cake>().ID = i;
            _cakes[i].GetComponent<Follower>().SetValues(_mainPath, _leftPath, _rightPath, _speedMoving);
        }
    }

    private void InitPools()
    {
        ObjectPool.SharedInstance.InitPools(_cakes, _amoutToPool);
    }
    private void InitPipeQueue()
    {
        CreateOrder(_order1, 3);
        CreateOrder(_order2, 3);
        _pipeQueue.AddRange(_order1);
        _pipeQueue.AddRange(_order2);
        _pipeQueue.Shuffle();

    }

    private void Update()
    {
        InstantinateCakes();
    }

    private void CreateOrder(List<int> order, int maxLengthOrder)
    {
        order.Clear();
        RandomWithoutDublicate(order, (0, _cakes.Length), maxLengthOrder);
    }

    private void InstantinateCakes()
    {
        if ((Time.time > _lastTimeSpawn + _timeBetweenSpawn) && (_pipeQueue.Count > 0))
        {
            int index = _pipeQueue[0];
            _pipeQueue.RemoveAt(0);

            GameObject cake =  ObjectPool.SharedInstance.GetPooledObject(index);
            cake.SetActive(true);
            _lastTimeSpawn = Time.time;
        }
    }

    public void NewOrder(Sides side)
    {
        //update values!!!!!

        _stage++;
        //_speedMoving++;


        if (side == Sides.Left)
        {
            CreateOrder(_order1, 3);
            _pipeQueue.AddRange(_order1);
        }
        else
        {
            CreateOrder(_order2, 3);
            _pipeQueue.AddRange(_order2);
        }
        _pipeQueue.Shuffle();

    }


    private void RandomWithoutDublicate(List<int> order, (int,int) range, int count)
    {
        
        List<int> possible = Enumerable.Range(range.Item1, range.Item2).ToList();
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, possible.Count);
            order.Add(possible[index]);
            possible.RemoveAt(index);
        }
    }

    private void PrintList(List<int> l)
    {
        string s = "";
        foreach (var item in l)
        {
            s += item + ", ";
        }
        Debug.Log(s);
    }
}

public static class ListExtension
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public enum Sides
{
    Left,
    Right
}
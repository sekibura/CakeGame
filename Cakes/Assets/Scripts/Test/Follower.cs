using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private PathCreator _currentPath;
    [SerializeField]
    private PathCreator _mainPath;
    [SerializeField]
    private float _speed = 5;
    private float _defaultSpeed;
    private float distanceTravelled;

    private void Start()
    {
        _defaultSpeed = _speed;
        _currentPath = _mainPath;
    }

    public void SetValues(PathCreator mainPath,  float speed)
    {
        _mainPath = mainPath;
        _speed = speed;
        _currentPath = mainPath;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void ResetSpeed()
    {
        _speed = _defaultSpeed;
        Debug.Log("_speed = " + _speed);
    }

    private void OnEnable()
    {
        distanceTravelled = 0;
    }

    private void Update()
    {
        distanceTravelled += _speed * Time.deltaTime;
        transform.position = _currentPath.path.GetPointAtDistance(distanceTravelled);
    }

    public void SetDistance(float distanceTravelled)
    {
        this.distanceTravelled = distanceTravelled;
    }
}

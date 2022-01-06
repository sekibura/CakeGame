using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    private PathCreator _currentPath;
    private PathCreator _mainPath;
    private PathCreator _leftPath;
    private PathCreator _rightPath;
    [SerializeField]
    private float _speed = 5;
    private float distanceTravelled;

    public void SetValues(PathCreator mainPath, PathCreator leftPath, PathCreator rightPath, float speed)
    {
        _mainPath = mainPath;
        _leftPath = leftPath;
        _rightPath = rightPath;
        _speed = speed;
        _currentPath = _mainPath;
    }

    private void Update()
    {
        distanceTravelled += _speed * Time.deltaTime;
        transform.position = _currentPath.path.GetPointAtDistance(distanceTravelled);
    }
}

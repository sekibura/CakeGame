using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCake", menuName = "ScriptableObjects/Cakes", order = 1)]
public class Cake : ScriptableObject
{
    [Tooltip("�������� �����")]
    [SerializeField] private GameObject _mesh;


    [Tooltip("�������� �����")]
    [SerializeField] private Texture _defaultTexture;

    [Tooltip("�������� �����")]
    [SerializeField] private Texture[] _textures;
}

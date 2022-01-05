using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCake", menuName = "ScriptableObjects/Cakes", order = 1)]
public class Cake : ScriptableObject
{
    [Tooltip("моделька торта")]
    [SerializeField] private GameObject _mesh;


    [Tooltip("моделька торта")]
    [SerializeField] private Texture _defaultTexture;

    [Tooltip("моделька торта")]
    [SerializeField] private Texture[] _textures;
}

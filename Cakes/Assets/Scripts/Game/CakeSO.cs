using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCake", menuName = "ScriptableObjects/Cakes", order = 1)]
public class CakeSO : ScriptableObject
{
    [Header("�������� �����")]
    [SerializeField] private GameObject _mesh;
    public GameObject Mesh
    {
        get { return _mesh; }
        private set { }
    }


    [Header("�������� ����� �� �������������")]
    [SerializeField] private Texture _defaultTexture;
    public Texture DefaultTexture
    {
        get { return _defaultTexture; }
        private set { }
    }

    [Tooltip("�������� � ������ �������� �����")]
    [SerializeField] private TextureIcon[] _texturesIcons;
    public TextureIcon[] Textures
    {
        get { return _texturesIcons; }
        private set { }
    }
}

[System.Serializable] 
public struct TextureIcon
{
    [Header("�������� �������� �����")]
    [SerializeField] private Texture _texture;
    [Header("������ ��� ����������� �� �������")]
    [SerializeField] private Sprite _sprite;
}

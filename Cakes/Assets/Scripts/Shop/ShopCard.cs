using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    private string _prefabName;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private TMP_Text _description;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TMP_Text _price;
    [SerializeField]
    private TMP_Text _profit;
    [SerializeField]
    private bool _alreadyPurchased;
    [SerializeField]
    private GameObject _button;
    private ShopSystem _shopSystem;
    private ShopView _shopView;

    private void Start()
    {
        _shopSystem = FindObjectOfType<ShopSystem>();
        _shopView = FindObjectOfType<ShopView>();
    }

    public void SetValues(string name, string prefabName, string description, Sprite image, int price, int profit, bool alreadyPurchased)
    {
        _name.text = name;
        _description.text = description;
        _image.sprite = image;
        _price.text = price.ToString();
        _profit.text = profit.ToString();
        _alreadyPurchased = alreadyPurchased;
        _prefabName = prefabName;

        _button.SetActive(!_alreadyPurchased);

        if (ScoreSystem.Instance.GetMoney() < price)
            _button.GetComponent<Button>().interactable = false;

    }
    public void SetValues(Cake cake, string prefabName, bool alreadyPurchased)
    {
        SetValues(cake.GetShopName(), prefabName, cake.GetDescription(), cake.GetSprite(), cake.GetPrice(), cake.GetProfit(), alreadyPurchased);
    }

    public void BuyButton()
    {
        
        if(_shopSystem.BuyItem(_name.text, _prefabName))
            _button.SetActive(false);
        _shopView.Init();


    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopCard;

    [SerializeField]
    private GameObject _pages;
    [SerializeField]
    private PageSwiper _pageSwiper;
    [SerializeField]
    private TMP_Text _moneyField;
    [SerializeField]
    private TMP_Text _moneyFieldMenu;
    [SerializeField]
    private TMP_Text _maxScore;

    private void Start()
    {
        _moneyField.text = ScoreSystem.Instance.GetMoney().ToString();
    }
    public void AddCard(Cake cake, bool alreadyPurchased)
    {
        GameObject shopCard = Instantiate(_shopCard);
        shopCard.transform.SetParent(_pages.transform);
        RectTransform rectTransform = shopCard.GetComponent<RectTransform>();
        rectTransform.SetLeft(0);
        rectTransform.SetRight(0);
        rectTransform.SetTop(0);
        rectTransform.SetBottom(0);
        rectTransform.localScale = new Vector3(1, 1, 1);
        shopCard.GetComponent<ShopCard>().SetValues(cake, cake.gameObject.name, alreadyPurchased);


        _pageSwiper.Init();
    }
    public void Init()
    {
        _pageSwiper.Init();
        _moneyField.text = ScoreSystem.Instance.GetMoney().ToString();
        _moneyFieldMenu.text =_moneyField.text;
        _maxScore.text = ScoreSystem.Instance.GetMaxScore().ToString();
    }
}


public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}

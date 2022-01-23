using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ShopSystem : MonoBehaviour
{
    //private List<ShopItem> _shopItems = new List<ShopItem>();
    private ShopItemsProxy shopItemsProxy = new ShopItemsProxy();
    private List<GameObject> _cakes = new List<GameObject>();

    [SerializeField]
    private ShopView _shopView;

    private void Start()
    {
#if DEBUG
        //SaveManager.Instance.Save<string>("ShopItems", "");
#endif
        LoadItems();
    }
    private void OnEnable()
    {
        //LoadItems();
    }

    private void LoadItems()
    {
        Debug.Log("LoadItems");

        #region load json
        string jsonStr = SaveManager.Instance.Load<string>("ShopItems");
        Debug.LogWarning(jsonStr);
        if (!string.IsNullOrEmpty(jsonStr))
            shopItemsProxy = JsonUtility.FromJson<ShopItemsProxy>(jsonStr);
        else
            Debug.LogWarning("Json empty!");

        #endregion


        # region load gameobjects from resources
        try
        {
            _cakes = Resources.LoadAll("Cakes", typeof(GameObject)).Cast<GameObject>().ToList();
            if (_cakes.Count == 0)
                Debug.LogWarning("No cakes resources");

            foreach (var go in _cakes)
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

        Debug.Log(_cakes.Count);
        foreach (var cake in _cakes)
        {
            var result = shopItemsProxy.ShopItems.Find(x => x.Name == cake.name);
            if (result == null)
            {
                Debug.LogWarning("Json не содержит cake - " + cake.name);

                shopItemsProxy.ShopItems.Add(new ShopItem(cake.name, false));

                AddToShop(cake, false);
            }
            else
            {
                AddToShop(cake, result.AlreadyPurchased);
            }
        }

        //SaveJson();
    }

    private void AddToShop(GameObject cakeObj, bool alreadyPurchased)
    {
        var cake = cakeObj.GetComponent<Cake>();
        bool aviableCake = cake.GetPrice() == 0 ? true : alreadyPurchased;
        //_shopItems.Add(new ShopItem(cakeObj.name, aviableCake));
        SpawnCard(cake, aviableCake);
    }

    public bool BuyItem(string ShopName, string prefabName)
    {
        Debug.Log("Buy item - " + ShopName);
        try
        {
            var money = SaveManager.Instance.Load<int>("Money");
            var price = _cakes.Find(x => x.gameObject.name == prefabName).GetComponent<Cake>().GetPrice();

            SaveManager.Instance.Save<int>("Money", money - price);

            shopItemsProxy.ShopItems.Find(x => x.Name == prefabName).AlreadyPurchased = true;

            SaveJson();

            return true;
        }
        catch
        {
            return false;
        }
    }

    private void SpawnCard(Cake cake, bool alreadyPurchased)
    {
        _shopView.AddCard(cake, alreadyPurchased);
    }

    private void SaveJson()
    {
        //////TODO
        ///update shopitems
        //string jsonStr = JsonUtility.ToJson(_shopItems);
        string jsonStr = JsonUtility.ToJson(shopItemsProxy);
        Debug.Log(jsonStr);
        SaveManager.Instance.Save<string>("ShopItems", jsonStr);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KozUtils;

public class Shopkeeper : StaticInstance<Shopkeeper>
{
    public int quantityInStock;
    List<Equipable> _stock;
    Equipable[] _templates;

    HintAnimator animator;
    bool playerOnRange = false;

    public event System.Action<List<Equipable>> OnStockChanged;
    // public event System.Action<int, 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            // Show button hint display
            animator.gameObject.SetActive(true);
            playerOnRange = true;
        }
    }

    // private void OnTriggerStay2D(Collider2D other) 
    // {
    //     Debug.Log("An object is within the shopkeeper's interaction range");
    //     if (other.CompareTag("Player") && Input.GetButtonDown("Interact"))
    //     {
    //         Debug.Log("Interacted with shopkeeper.");
    //     }
    // }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            // Hide button hint display
            animator.gameObject.SetActive(false);
            playerOnRange = false;
        }
    }

    private void Start() {
        _stock = new();
        _templates = Resources.LoadAll<Equipable>("Equipables");
        animator = GetComponentInChildren<HintAnimator>();
        animator.gameObject.SetActive(false);
    }

    private void Update() 
    {
        if (playerOnRange && Input.GetButtonDown("Interact"))
        {
            ShopGUI.Instance.OpenInterface();
            GenerateNewCollection();
        }
    }

    public void GenerateNewCollection()
    {
        _stock.Clear();
        Debug.Log("Generating new collection...");

        Equipable item; 
        for (int i=0; i < quantityInStock; i++)
        {
            item = Instantiate(_templates[Random.Range(0,_templates.Length)]);
            item.SetRandomColor();
            _stock.Add(item);
        }

        OnStockChanged?.Invoke(_stock);
    }

    public int BuyItem(Equipable item)
    {
        _stock.Add(item);
        OnStockChanged?.Invoke(_stock);

        return item.price;
    }

    public void SellItemByIndex(int index)
    {
        Equipable item = _stock[index];

        if (PlayerInventory.Instance.TryBuyItem(item))
        {
            _stock.Remove(item);
            OnStockChanged?.Invoke(_stock);
        }
    }
}

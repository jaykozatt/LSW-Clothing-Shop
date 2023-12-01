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
    bool wasInteracted = false;

    public event System.Action<List<Equipable>> OnNewStock;
    // public event System.Action<int, 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            // Show button hint display
            animator.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Interact"))
        {
            wasInteracted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            // Hide button hint display
            animator.gameObject.SetActive(false);
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
        if (wasInteracted)
        {
            ShopGUI.Instance.OpenInterface();
            GenerateNewCollection();
        }
        wasInteracted = false;
    }

    public void GenerateNewCollection()
    {
        _stock.Clear();

        Equipable item; 
        for (int i=0; i < quantityInStock; i++)
        {
            item = Instantiate(_templates[Random.Range(0,_templates.Length)]);
            item.SetRandomColor();
            _stock.Add(item);
        }

        OnNewStock?.Invoke(_stock);
    }

    public void BuyItemByIndex(int index)
    {

    }
}

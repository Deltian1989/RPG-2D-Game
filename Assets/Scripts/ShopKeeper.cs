using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpen;

    public string[] itemsForSale= new string[40];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.T) &&
            PlayerController.instance.canMove &&
            !Shop.instance.shopMenu.activeInHierarchy)
        {
            Shop.instance.itemsForSale = itemsForSale;

            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
}

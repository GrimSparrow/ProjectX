using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SImpleInventoryUI : BaseWindow
{
    [SerializeField] private GameObject inventoryItemContainer;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private InventoryController inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<InventoryController>();
    }

    public void Show()
    {
        Clean();
        DrawItems();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Clean()
    {
        DrawDetails(null);
        foreach (Transform child in inventoryItemContainer.transform) 
        {
            Destroy(child.gameObject);
        }
    }

    private void DrawItems()
    {
        var items = inventory.Cells;

        foreach (var item in items)
        {
            if (item.container != null)
            {
                DrawItemCell(item);
            }
        }
    }

    private void DrawItemCell(ItemCell item)
    {
        GameObject itemBtn = Instantiate(itemPrefab, inventoryItemContainer.transform);
        var image = itemBtn.GetComponent<Image>();
        image.sprite = item.Pic;
        var button = itemBtn.GetComponent<Button>();
        var activityIndicator = button.transform.Find("Selected").gameObject;
        button.onClick.AddListener(() =>
        {
            DrawDetails(item);
            activityIndicator.gameObject.SetActive(item.isActive);
        });
    }

    private void DrawDetails(ItemCell item)
    {
        if (item == null)
        {
            itemName.SetText(string.Empty);
            itemDescription.SetText(string.Empty);
            itemImage.enabled = false;
            return;
        }    
        itemName.text = item.Name;
        itemDescription.text = item.Description;
        itemImage.sprite = item.Pic;
        itemImage.enabled = true;
    }
}

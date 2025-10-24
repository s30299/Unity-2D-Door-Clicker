using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class StoreUpgrade : MonoBehaviour
{
    [Header("Components")]
    public TMP_Text priceText;
    public TMP_Text incomeInfoText;
    public Button button;
    public Image characterImage;
    public TMP_Text upgradeNameText;


    [Header("Generator values")]
    public string upgradeName;
    public int startPrice = 15;
    public float upgradePriceMultiplayer;
    public float doorsPerUpgrade = 0.1f;
    [Header("Managers")]
    public GameManager gameManager;
    int level = 0;

    
    private void Start()
    {
        UpdateUI();
    }

    public void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSucces = gameManager.PurchaseAction(price);
        if (purchaseSucces)
        {
            level++;
            UpdateUI();
        }
    }


    public void UpdateUI()
    {
        priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + " X " + doorsPerUpgrade + "/s";
        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;
        bool isPurchased = level > 0;
        characterImage.color = isPurchased ? Color.white : Color.black;
        upgradeNameText.text = isPurchased ? upgradeName : "???";
    }

    int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplayer, level));
        return price;
    }

    public float CalculateIncomePerSecond()
    {
        return doorsPerUpgrade * level;
    }
}

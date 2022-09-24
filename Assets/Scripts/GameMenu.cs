using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject[] windows;

    private CharStats[] playerStats;

    public Text[] nameTexts, hpTexts, mpTexts, lvlTexts, expTexts;
    public Slider[] expSliders;
    public Image[] charImages;
    public GameObject[] charStatsHolder;

    public GameObject[] statusButtons;

    public Text statusName, statusHP, statusMP, statusStr, statusDext, statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, statusArmrPwr, statusExp;
    public Image statusImage;
    public ItemButton[] itemButtons;

    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;

    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    public Text goldText;

    public static GameMenu instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (menu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                menu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatsHolder[i].SetActive(true);

                nameTexts[i].text = playerStats[i].charName;
                hpTexts[i].text = $"HP: {playerStats[i].currentHP}/{playerStats[i].maxHP}";
                mpTexts[i].text = $"MP: {playerStats[i].currentMP}/{playerStats[i].maxMP}";
                lvlTexts[i].text = $"Lvl: {playerStats[i].playerLevel}";
                expTexts[i].text = $"{playerStats[i].currentExp}/{playerStats[i].expToNextLevel[playerStats[i].playerLevel]}";
                expSliders[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSliders[i].value = playerStats[i].currentExp;
                charImages[i].sprite = playerStats[i].charImage;
            }
            else
            {
                charStatsHolder[i].SetActive(false);
            }
        }

        goldText.text = GameManager.instance.currentGold.ToString("N0");
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }

        itemCharChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        menu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;

        itemCharChoiceMenu.SetActive(false);
    }

    public void OpenStatus()
    {
        UpdateMainStats();

        // update the information that is shown

        StatusChar(0);

        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            var statusButtonText = statusButtons[i].GetComponentInChildren<Text>();

            statusButtonText.text = playerStats[i].charName;
        }
    }

    public void StatusChar(int selected)
    {
        statusName.text = playerStats[selected].charName;
        statusHP.text = $"{playerStats[selected].currentHP}/{playerStats[selected].maxHP}";
        statusMP.text = $"{playerStats[selected].currentMP}/{playerStats[selected].maxMP}";
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defence.ToString();
        statusDext.text = playerStats[selected].dexterity.ToString();
        if (playerStats[selected].equippedWeapon != "")
        {
            statusWpnEqpd.text = playerStats[selected].equippedWeapon;
        }

        statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();

        if (playerStats[selected].equippedArmor != "")
        {
            statusArmrEqpd.text = playerStats[selected].equippedArmor;
        }

        statusArmrPwr.text = playerStats[selected].armrPwr.ToString();
        statusExp.text = $"{playerStats[selected].expToNextLevel[playerStats[selected].playerLevel]- playerStats[selected].currentExp}";
        statusImage.sprite = playerStats[selected].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i< itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;

                int amount = GameManager.instance.numberOfItems[i];

                itemButtons[i].amountText.text = amount != 1 ? amount.ToString() : "";
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        if (activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharChoice();
    }

    public void SaveGame()
    {
        GameManager.instance.SaveData();
        QuestManager.instance.SaveQuestData();
    }
}

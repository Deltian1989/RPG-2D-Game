using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int playerLevel = 1;
    public int currentExp;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    public int currentHP;
    public int maxHP = 100;

    public int currentMP;
    public int maxMP = 30;
    public int[] mpLvlBonus;
    public int strength;
    public int dexterity;
    public int defence;
    public int wpnPwr;
    public int armrPwr;
    public string equippedWeapon;
    public string equippedArmor;

    public Sprite charImage;


    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExpr(500);
        }
    }

    public void AddExpr(int expToAdd)
    {
        currentExp += expToAdd;
        if (playerLevel < maxLevel)
        {
            if (currentExp > expToNextLevel[playerLevel])
            {
                currentExp -= expToNextLevel[playerLevel];

                playerLevel++;

                // determine wether to add to str or def based on odd or even

                if (playerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }

                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                maxMP += mpLvlBonus[playerLevel];
                currentMP = maxMP;
            }
        }
        

        if (playerLevel >= maxLevel)
        {
            currentExp = 0;
        }
    }
}

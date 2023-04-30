using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private Player player;
    private int level = 1;
    private int experience = 0;
    private int money = 0;
    private int nbrSpaceShip = 0;

    [SerializeField] private Resource uranium;
    [SerializeField] private Resource artifact;
    [SerializeField] private Resource oil;
    [SerializeField] private Resource Iron;
    [SerializeField] private Resource GemStone;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerManager dans la scène");
            return;
        }
        instance = this;
    }

    private void test()
    {
        uranium.quantity = 1000000;
    }
    private void Start()
    {
        player = new Player(level, experience, money, nbrSpaceShip, uranium.quantity, artifact.quantity, oil.quantity, Iron.quantity, GemStone.quantity);
    }
    public void AddExperience(int experienceToAdd)
    {
        experience += experienceToAdd;
        if (experience >= 100)
        {
            level++;
            experience -= 100;
        }
    }
    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }
    public void AddSpaceShip(int nbrSpaceShipToAdd)
    {
        nbrSpaceShip += nbrSpaceShipToAdd;
    }
    public void AddUranium(int nbrUraniumToAdd)
    {
        uranium.quantity += nbrUraniumToAdd;
    }
    public void AddArtifacts(int nbrArtifactsToAdd)
    {
        artifact.quantity += nbrArtifactsToAdd;
    }
    public void AddPatrol(int nbrPatrolToAdd)
    {
        oil.quantity += nbrPatrolToAdd;
    }
    public void AddIron(int nbrIronToAdd)
    {
        Iron.quantity += nbrIronToAdd;
    }
    public void AddGemStone(int nbrGemStoneToAdd)
    {
        GemStone.quantity += nbrGemStoneToAdd;
    }

    public void LoseMoney(int money)
    {
        this.money -= money;
    }
    public void LoseSpaceShip(int nbrSpaceShip)
    {
        this.nbrSpaceShip -= nbrSpaceShip;
    }
    public void LoseUranium(int nbrUranium)
    {
        uranium.quantity -= nbrUranium;
    }
    public void LoseArtifacts(int nbrArtifacts)
    {
        artifact.quantity -= nbrArtifacts;
    }
    public void LosePatrol(int nbrPatrol)
    {
        oil.quantity -= nbrPatrol;
    }
    public void LoseIron(int nbrIron)
    {
        Iron.quantity -= nbrIron;
    }
    public void LoseGemStone(int nbrGemStone)
    {
        GemStone.quantity -= nbrGemStone;
    }

    public void SellUranium(int nbrUranium)
    {
        if (player.nbrUranium >= nbrUranium)
        {

        }
    }

    public int GetLevel()
    {
        return level;
    }
    public int GetExperience()
    {
        return experience;
    }
    public int GetMoney()
    {
        return money;
    }
    public int GetNbrSpaceShip()
    {
        return nbrSpaceShip;
    }
    public int GetNbrUranium()
    {
        return uranium.quantity;
    }
    public int GetNbrArtifacts()
    {
        return artifact.quantity;
    }
    public int GetNbrPatrol()
    {
        return oil.quantity;
    }
    public int GetNbrIron()
    {
        return Iron.quantity;
    }
    public int GetNbrGemStone()
    {
        return GemStone.quantity;
    }
}

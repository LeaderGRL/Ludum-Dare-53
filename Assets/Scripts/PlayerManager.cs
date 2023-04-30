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
    private int nbrUranium = 0;
    private int nbrArtifacts = 0;
    private int nbrPatrol = 0;
    private int nbrIron = 0;
    private int nbrGemStone = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerManager dans la scène");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        player = new Player(level, experience, money, nbrSpaceShip, nbrUranium, nbrArtifacts, nbrPatrol, nbrIron, nbrGemStone);
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
        nbrUranium += nbrUraniumToAdd;
    }
    public void AddArtifacts(int nbrArtifactsToAdd)
    {
        nbrArtifacts += nbrArtifactsToAdd;
    }
    public void AddPatrol(int nbrPatrolToAdd)
    {
        nbrPatrol += nbrPatrolToAdd;
    }
    public void AddIron(int nbrIronToAdd)
    {
        nbrIron += nbrIronToAdd;
    }
    public void AddGemStone(int nbrGemStoneToAdd)
    {
        nbrGemStone += nbrGemStoneToAdd;
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
        return nbrUranium;
    }
    public int GetNbrArtifacts()
    {
        return nbrArtifacts;
    }
    public int GetNbrPatrol()
    {
        return nbrPatrol;
    }
    public int GetNbrIron()
    {
        return nbrIron;
    }
    public int GetNbrGemStone()
    {
        return nbrGemStone;
    }

    public Player GetPlayer()
    {
        return player;
    }
}


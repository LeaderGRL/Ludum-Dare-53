using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;
    public int level;
    public int experience;
    public int money;
    public int nbrSpaceShip;
    public int nbrUranium;
    public int nbrArtifacts;
    public int nbrPatrol;
    public int nbrIron;
    public int nbrGemStone;

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
        player = new Player(level, experience, money, nbrSpaceShip, nbrUranium, nbrArtifacts, nbrPatrol, nbrIron, GemStone);
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
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Player
{
    public int level;
    public int experience;
    public int money;
    public int nbrSpaceShip;
    public int nbrUranium;
    public int nbrArtifacts;
    public int nbrPatrol;
    public int nbrIron;
    public int nbrGemStone;

    public Player(int level, int experience, int money, int nbrSpaceShip, int nbrUranium, int nbrArtifacts, int nbrPatrol, int nbrIron, int GemStone)
    {
        this.level = level;
        this.experience = experience;
        this.money = money;
        this.nbrSpaceShip = nbrSpaceShip;
        this.nbrUranium = nbrUranium;
        this.nbrArtifacts = nbrArtifacts;
        this.nbrPatrol = nbrPatrol;
        this.nbrIron = nbrIron;
        this.nbrGemStone = nbrGemStone;
    }
}
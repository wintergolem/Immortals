using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Account {

    public static Account instance;

    public PlayerType opponentType = PlayerType.AI;
    public FactionType factionToBePlayed;
    public FactionType factionOpponent;

    private Account(){
        factionOpponent = FactionType.Phalanx;
        factionToBePlayed = FactionType.Zombies;
    }

    public static void CreateAccount(){
        instance = new Account();
    }

    public static void AssignFaction( int player, int faction){
        FactionType assign;
        switch( faction){
            case 0:
                assign = FactionType.Priest;
                break;
            case 1:
                assign = FactionType.Phalanx;
                break;
            case 2:
                assign = FactionType.Zombies;
                break;
            default:
                assign = FactionType.Phalanx;
                break;
    }

        switch (player){
            case 0:
                Account.instance.factionToBePlayed = assign;
                break;
            case 1:
                Account.instance.factionOpponent = assign;
                break;
        }
    }
}

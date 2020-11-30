using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Account {

    public static Account instance = new Account();

    public PlayerType opponentType = PlayerType.Local;
    public ArmyList selectedList;
    public ArmyList opponentList;

    public List<ArmyList> savedLists;

    int silverTotal = 0;
    int goldTotal = 0;

    int threatIndicatorKey = -1;
    int moveIndicatorKey = -1;

    private Account()
    {
        savedLists = new List<ArmyList>();
        CreateAccount();
    }

    void CreateAccount(){
        selectedList = null;
        opponentList = null;

    }

    public ArmyList GetArmyList( int player ) {
        if (selectedList == null) {
            selectedList = null;
            opponentList = null;
        }
        return player == 0 ? selectedList : opponentList;
    }

    public static void AssignList( ArmyList list, int player){
        switch( player ) {
            case 0:
                instance.selectedList = list;
                break;
            case 1:
                instance.opponentList = list;
                break;
        }
    }

    public static int[] GetCurrency()
    {
        return new int[] { instance.silverTotal, instance.goldTotal };
    }

    public static void VerifyCurrency()
    {
        int[] serverCurrency = ServerContactManager.GetServerCurrency();

        instance.silverTotal = serverCurrency[0];
        instance.goldTotal = serverCurrency[1];
    }

    public void RunUnlockCheckOnIndicators(ref IndicatorBase[] indicators, bool checkingThreat = true)
    {
        int checkAgainst;
        if (checkingThreat) checkAgainst = threatIndicatorKey;
        else checkAgainst = moveIndicatorKey;

        foreach (IndicatorBase indicator in indicators)
        {
            indicator.SetLock((indicator.unlockKey & checkAgainst) == checkAgainst);
        }
    }
}

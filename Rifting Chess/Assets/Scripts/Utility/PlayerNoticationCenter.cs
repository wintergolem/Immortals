using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region Player Notication Center

public enum EventToTrigger { StartOfTurn , EndOfTurn , FriendlyCaptured , EnemyCaptured, PowerPressed  };

//public delegate void StartOfTurnDelegate(GameManager manager);
//public delegate void EndOfTurnDelegate(GameManager manager);
//public delegate void FriendlyPieceCaptured(GameManager manager);
//public delegate void EnemyPieceCaptured(GameManager manager);

public delegate void PowerButtonPressed(GameManager manager);

public class PlayerNoticationCenter  {
    public static PlayerNoticationCenter playerOne = new PlayerNoticationCenter();
    public static PlayerNoticationCenter playerTwo = new PlayerNoticationCenter();

    private PlayerNoticationCenter(){
    }

    public  List<Action> TurnStart = new List<Action>();
    public List<Action> TurnEnd = new List<Action>();
    public List<Action> FriendlyCaptured = new List<Action>();
    public List<Action> EnemyCaptured = new List<Action>();
    public List<Action> PowerPressed = new List<Action>();

    public NotificationQueueRunner runner = new NotificationQueueRunner();

    public void Purge()
    {
        TurnStart = new List<Action>();
        TurnEnd = new List<Action>();
        FriendlyCaptured = new List<Action>();
        EnemyCaptured = new List<Action>();
        PowerPressed = new List<Action>();
        runner = new NotificationQueueRunner();
    }

    public void TriggerEvent(EventToTrigger trigger){
        switch (trigger)  {
            case EventToTrigger.EndOfTurn:
                SetRunner(TurnEnd.ToArray());
                break;
            case EventToTrigger.StartOfTurn:
                SetRunner(TurnStart.ToArray());
                break;
            case EventToTrigger.FriendlyCaptured:
                SetRunner(FriendlyCaptured.ToArray());
                break;
            case EventToTrigger.EnemyCaptured:
               SetRunner(EnemyCaptured.ToArray());
                break;
            case EventToTrigger.PowerPressed:
                SetRunner(PowerPressed.ToArray());
                break;
        }
    }
    void SetRunner( Action[] actions) {
        foreach (Action a in actions){
            runner.AddAction(a);
        }

        if (!runner.running){
            runner.running = true;
            GameManager.instance.stuffTakingFocus.Add(this);
            runner.RunNext();
        }
    }
}


#endregion

#region Game Notication Center

public enum GameEventTrigger{ ClickedOnSquare, ClickedOnPiece, HoverSquare , RemoveHover ,
    PowerPressed, SwitchingPlayers, RightClick, PieceMoved}; 

//public delegate void HoverOverSquare(Vector2Int gridpoint);
//public delegate void ClickedOnSquare(Vector2Int gridpoint);
//public delegate void SwitchingPlayers();
//public delegate void RemoveHover();
//public delegate void RightClicked();

public class GameNoticationCenter {
    public static GameNoticationCenter instance = new GameNoticationCenter();

    public  List<Action> HoverSquare = new List<Action>();
    public  List<Action> ClickedSquare = new List<Action>();
    public List<Action> ClickedPiece = new List<Action>();
    public  List<Action> RemoveHover = new List<Action>();
    public  List<Action> SwitchingPlayers = new List<Action>();
    public  List<Action> RightClick = new List<Action>();
    public List<Action> PieceMoved = new List<Action>();

    public NotificationQueueRunner runner= new NotificationQueueRunner() ;

    public static void Purge()
    {
        instance.HoverSquare = new List<Action>();
        instance.ClickedSquare = new List<Action>();
        instance.ClickedPiece = new List<Action>();
        instance.RemoveHover = new List<Action>();
        instance.SwitchingPlayers = new List<Action>();
        instance.RightClick = new List<Action>();
        instance.PieceMoved = new List<Action>();
        instance.runner = new NotificationQueueRunner();
    }

    public static void TriggerEvent(GameEventTrigger trigger)
    {
        switch (trigger)
        {
            case GameEventTrigger.ClickedOnSquare:
                if (instance.ClickedSquare.Count != 0)
                    instance.SetRunner(instance.ClickedSquare.ToArray());
                break;
            case GameEventTrigger.ClickedOnPiece:
                if (instance.ClickedPiece.Count != 0)
                    instance.SetRunner(instance.ClickedPiece.ToArray());
                break;
            case GameEventTrigger.HoverSquare:
                if (instance.HoverSquare != null) //should be fine to just run this, some functions need to be on main thread
                    instance.HoverSquare.ForEach( x => {
                    x();
                } );
                //instance.SetRunner(instance.HoverSquare.ToArray());
                break;
            case GameEventTrigger.RemoveHover:
                if (instance.RemoveHover != null) //should be fine to just run this, some functions need to be on main thread
                    instance.RemoveHover.ForEach(x => {
                        x();
                    });
                break;
            case GameEventTrigger.SwitchingPlayers:
               // instance.SetRunner(instance.SwitchingPlayers.ToArray());
                break;
            case GameEventTrigger.RightClick:
                instance.SetRunner(instance.RightClick.ToArray());
                break;
            case GameEventTrigger.PieceMoved:
                instance.SetRunner(instance.PieceMoved.ToArray());
                break;
        }
    }

    void SetRunner(Action[] actions)  {
        foreach (Action a in actions){
            runner.AddAction(a);
        }

        if (!runner.running) {
            runner.running = true;
            GameManager.instance.stuffTakingFocus.Add(this);
            runner.RunNext();
        }
    }
    #endregion
}
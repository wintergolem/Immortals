using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NotificationQueueRunner {
    List<Action> actionList = new List<Action>();
    Action endAction;

    public bool running = false;

    public NotificationQueueRunner()
    {
    }

    public void AddAction( Action action) {
        actionList.Add(action);
    }

    public void SetEndAction( Action action){
        endAction = action;
    }

    public void RemovePiece(Piece piece)
    {
        for (int i = 0; i < actionList.Count; i++)
        {
            if ( (object)actionList[i].Target == piece)
            {
                actionList.RemoveAt(i);
                i--;
            }
        }

    }

    public void RunNext()
    {
        if (actionList.Count > 0)
        {
            var task = actionList[0];
            actionList.RemoveAt(0);
            if( task.Target != null)
                task();
        }  else {
            running = false;
            actionList = new List<Action>();
            GameManager.instance.stuffTakingFocus.Remove(this);
            if (endAction != null) endAction();
        }
    }
}

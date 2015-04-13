using Assets.Code.Controllers;
using Assets.Code.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.States
{
    public enum TransitionID
    {
        Null,
        Next,
        Previous
    }

    public class StateTransition
    {
        public delegate void EventsComplete(StateID nextStateID);
        public static event EventsComplete OnEventsComplete;

        public TransitionID TransitionID { get; private set; }
        public StateID NextStateID { get; private set; }
        public Dictionary<Events.Event, CoroutineID> EventToCoroutineIDMap { get; private set; }
        public bool EventsRunning { get; private set; }

        public StateTransition(TransitionID transitionID, StateID nextStateID)
        {
            this.TransitionID = transitionID;
            this.NextStateID = nextStateID;
            this.EventsRunning = false;

            RemoveAllEvents();
        }

        public void AddEvent(Events.Event e, CoroutineID coroutineID)
        {
            if (e != null && coroutineID != CoroutineID.Null && !EventToCoroutineIDMap.ContainsKey(e))
                EventToCoroutineIDMap.Add(e, coroutineID);
        }

        public void RemoveEvent(Events.Event e)
        {
            if (e != null && EventToCoroutineIDMap.ContainsKey(e))
                EventToCoroutineIDMap.Remove(e);
        }

        public void RemoveAllEvents()
        {
            EventToCoroutineIDMap = new Dictionary<Events.Event, CoroutineID>();
        }

        public void RunEvents()
        {
            if (!EventsRunning)
            {
                Job eventJob = Job.Make(ParentEvent(), false);
                eventJob.JobComplete += (wasKilled) =>
                {
                    Debug.Log("Events completed");
                    EventsRunning = false;

                    if (OnEventsComplete != null)
                        OnEventsComplete(NextStateID);
                };

                foreach (KeyValuePair<Events.Event, CoroutineID> e in EventToCoroutineIDMap)
                {
                    IEnumerator coroutine = GetCoroutine(e.Key, e.Value);
                    eventJob.CreateAndAddChildJob(coroutine);
                }

                EventsRunning = true;
                eventJob.Start();
            }
        }

        private IEnumerator ParentEvent()
        {
            yield return null;
        }

        private IEnumerator GetCoroutine(Events.Event e, CoroutineID coroutineID)
        {
            switch (coroutineID)
            {
                case CoroutineID.Execute:
                    Debug.Log("Executing " + e.EventID);
                    return e.Execute();
                case CoroutineID.Undo:
                    Debug.Log("Undoing " + e.EventID);
                    return e.Undo();
                case CoroutineID.Null:
                default:
                    Debug.Log("Doing nothing");
                    return null;
            }
        }
    }
}
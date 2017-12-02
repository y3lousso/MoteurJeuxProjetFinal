namespace MoteurJeuxProjetFinal.GameEngine
{
    
    /// <summary>
    /// IEventDispatcher is interface which can dispatch an incoming event to the listeners (with visitor pattern)
    /// To handle a new event : add a method with the corresponding event in the interface and 
    /// in the implemented class in the EventSystem
    /// </summary>
    interface IEventDispatcher
    {
        void Dispatch(GameStartEvent gameEvent);
        void Dispatch(GameFinishEvent gameEvent);
        void Dispatch(CollisionEvent gameEvent);
        void Dispatch(SceneChangeEvent gameEvent);

    }
    
    
    ///////////////////
    // LIST OF EVENT //
    ///////////////////
    
    internal interface IEvent
    {
        void onCall(IEventDispatcher dispatcher);
    }

    class GameStartEvent : IEvent
    {
        public void onCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }
    
    class GameFinishEvent : IEvent
    {
        public void onCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class CollisionEvent : IEvent
    {
        public void onCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class SceneChangeEvent : IEvent
    {
        public void onCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }
    
    
    
}
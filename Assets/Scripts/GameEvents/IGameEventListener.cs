namespace GameEvents
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T source);
    }
    
    public interface IGameEventListener<T0, T1>
    {
        void OnEventRaised(T0 param0, T1 param1);
    }
}
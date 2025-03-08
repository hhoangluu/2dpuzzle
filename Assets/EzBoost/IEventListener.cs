namespace EzBoost
{
    public interface IEventListener
    {
        
    }
    
    public interface IEventListener<T> where T : struct
    {
        void OnEzEvent(T eventData);
    }
}
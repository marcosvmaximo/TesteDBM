namespace Teste.Core.Notifications;

public class Notifiable : INotifiable
{
    private readonly List<Notification> _notifications = new List<Notification>();

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public bool IsValid => !_notifications.Any();

    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }
    
    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(IEnumerable<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }
}
namespace Teste.Core.Notifications;

public interface INotifiable
{
    IReadOnlyCollection<Notification> Notifications { get; }
    bool IsValid { get; }

    void AddNotification(string key, string message);
    void AddNotification(Notification notification);
    void AddNotifications(IEnumerable<Notification> notifications);
}
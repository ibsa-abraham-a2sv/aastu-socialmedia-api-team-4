namespace WebApi.Service.NotificationService;

public interface INotificationService
{
    Task SendNotificationToSingleUser(int userId, string message);
    public Task PostIsLiked(int postId, int postLiker);
    public Task PostIsCommentedOn(int postId, int commenterUserId);
    public Task UserIsFollowed(int followerUserId, int followedUserId);
}
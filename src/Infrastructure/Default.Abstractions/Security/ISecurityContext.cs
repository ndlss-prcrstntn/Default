namespace Default.Abstractions.Security;

public interface ISecurityContext
{
    Guid UserUid { get; init; }

    bool IsAdmin { get; }

    bool IsSystem { get; }

    string[] Roles { get; init; }

    TimeSpan TimeOffset { get; set; }

    bool IsCurrentUser(Guid userUid);
}
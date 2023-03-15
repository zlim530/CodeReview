using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

public record UserNameChangeNotification(string OldName, string NewName) : INotification;
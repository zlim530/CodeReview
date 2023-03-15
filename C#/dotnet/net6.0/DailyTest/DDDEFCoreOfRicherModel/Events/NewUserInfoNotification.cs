using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

public record NewUserInfoNotification(string UserName, DateTime CreatedDateTime) : INotification;
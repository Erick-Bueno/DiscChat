public record UserNotFoundError(string detail):AppError(ErrorType.Business.ToString(), nameof(UserNotFoundError));
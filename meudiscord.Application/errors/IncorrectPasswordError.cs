public record IncorrectPasswordError(string detail) : AppError(ErrorType.Validation.ToString(), nameof(IncorrectPasswordError));
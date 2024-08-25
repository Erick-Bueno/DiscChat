using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;

public static class ControllerBaseExtensions
{
    public static IActionResult HandlerAuthResponse(this ControllerBase controllerBase, OneOf<ResponseAuth, AppError> response)
    {
        return response.Match(
                response => controllerBase.Ok(response),
                error =>
                {
                    if (error.errorType == ErrorType.Validation.ToString())
                        return controllerBase.BadRequest(error);

                    else if (error.errorType == ErrorType.Business.ToString())
                        return controllerBase.Conflict(error);

                    return controllerBase.StatusCode(500, new InternalServerError());
                }
            );
    }
    public static IActionResult HandlerRegisterResponse(this ControllerBase controllerBase, OneOf<ResponseAuth, AppError> response)
    {
        return response.Match(
                      response => controllerBase.Created("Users/Guid", response),
                      error =>
                      {
                          if (error.errorType == ErrorType.Validation.ToString())
                              return controllerBase.BadRequest(error);
                          return controllerBase.StatusCode(500, new InternalServerError());
                      });
    }
    public static IActionResult HandlerGetAllChannelsInServerResponse(this ControllerBase controllerBase, OneOf<ResponseAllChannels, AppError> response)
    {
        return response.Match(
                response => controllerBase.Ok(response),
                error =>
                {
                    if (error.errorType == ErrorType.Validation.ToString())
                        return controllerBase.BadRequest(error);

                    return controllerBase.StatusCode(500, new InternalServerError());
                }
            );
    }
    public static IActionResult HandlerCreateChannelResponse(this ControllerBase controllerBase, OneOf<ResponseCreateChannel, AppError> response)
    {
        return response.Match(
              response => controllerBase.Created("localhost", response),
              error =>
              {
                  if (error.errorType == ErrorType.Validation.ToString())
                      return controllerBase.BadRequest(error);

                  return controllerBase.StatusCode(500, new InternalServerError());
              }
          );
    }
    public static IActionResult HandlerGetAllGuildsResponse(this ControllerBase controllerBase, OneOf<ResponseAllGuilds, AppError> response)
    {
        return response.Match(
              response => controllerBase.Ok(response),
              error =>
              {
                  if (error.errorType == ErrorType.Business.ToString())
                      return controllerBase.Conflict(error);
                  return controllerBase.StatusCode(500, new InternalServerError());
              }
          );
    }
    public static IActionResult HandlerCreateGuildResponse(this ControllerBase controllerBase, OneOf<ResponseCreateGuild, AppError> response)
    {
        return response.Match(
                response => controllerBase.Created("localhost", response),
                error =>
                {
                    if (error.errorType == ErrorType.Business.ToString())
                        return controllerBase.Conflict(error);
                    return controllerBase.StatusCode(500, new InternalServerError());
                }

            );
    }
    public static IActionResult HandlerDeleteGuildResponse(this ControllerBase controllerBase, OneOf<ResponseSuccessDefault, AppError> response)
    {
        return response.Match(
              response => controllerBase.Ok(response),
              error =>
              {
                  if (error.errorType == ErrorType.Business.ToString())
                      return controllerBase.Conflict(error);
                  return controllerBase.StatusCode(500, new InternalServerError());
              }
          );
    }
    public static IActionResult HandlerGetOldMessagesResponse(this ControllerBase controllerBase, OneOf<ResponseGetOldMessages, AppError> response)
    {
        return response.Match(
            response => controllerBase.Ok(response),
            error =>
            {
                if (error.errorType == ErrorType.Validation.ToString())
                    return controllerBase.BadRequest(error);
                return controllerBase.StatusCode(500, new InternalServerError());
            }
        );
    }
    public static IActionResult HandlerDeleteMessageInChannelResponse(this ControllerBase controllerBase, OneOf<ResponseSuccessDefault, AppError> response)
    {
        return response.Match(
            response => controllerBase.Ok(response),
            erro =>
            {
                if (erro.errorType == ErrorType.Business.ToString())
                    return controllerBase.Conflict(erro);
                
                return controllerBase.StatusCode(500, new InternalServerError());
            }
        );
    }
    public static IActionResult HandlerFindUserAuthenticatedResponse(this ControllerBase controllerBase, OneOf<ResponseUserData, AppError> response)
    {
        return response.Match(
            response => controllerBase.Ok(response),
            erro =>
            {
                if (erro.errorType == ErrorType.Business.ToString())
                        return controllerBase.Conflict(erro);
                
                return controllerBase.StatusCode(500, new InternalServerError());
            }
        );
    }
    public static IActionResult HandlerRefreshTokenResponse(this ControllerBase controllerBase, OneOf<ResponseNewTokens, AppError> response)
    {
        return response.Match(
           response => controllerBase.Ok(response),
           erro =>
           {
               if (erro.errorType == ErrorType.Validation.ToString())
                   return controllerBase.BadRequest(erro);
               return controllerBase.StatusCode(500, new InternalServerError());
           }
       );
    }
    public static IActionResult HandlerGetGuildByExternalIdResponse(this ControllerBase controllerBase, OneOf<ResponseGetGuildByExternalId, AppError> response){
         return response.Match(
           response => controllerBase.Ok(response),
           erro =>
           {
               if (erro.errorType == ErrorType.Validation.ToString())
                   return controllerBase.BadRequest(erro);
               return controllerBase.StatusCode(500, new InternalServerError());
           }
       );
    }
}
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;

public static class ResponseHelper
{
    public static IActionResult ResponseAuthHelper(this ControllerBase controllerBase, OneOf<ResponseAuth, AppError> response)
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
    public static IActionResult ResponseRegisterHelper(this ControllerBase controllerBase, OneOf<ResponseAuth, AppError> response)
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
    public static IActionResult ResponseGetAllChannelsInServerHelper(this ControllerBase controllerBase, OneOf<ResponseAllChannels, AppError> response)
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
    public static IActionResult ResponseCreateChannelHelper(this ControllerBase controllerBase, OneOf<ResponseCreateChannel, AppError> response)
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
    public static IActionResult ResponseGetAllGuildsHelper(this ControllerBase controllerBase, OneOf<ResponseAllGuilds, AppError> response)
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
    public static IActionResult ResponseCreateGuildHelper(this ControllerBase controllerBase, OneOf<ResponseCreateGuild, AppError> response)
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
    public static IActionResult ResponseDeleteGuildHelper(this ControllerBase controllerBase, OneOf<ResponseSuccessDefault, AppError> response)
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
    public static IActionResult ResponseGetOldMessagesHelper(this ControllerBase controllerBase, OneOf<ResponseGetOldMessages, AppError> response)
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
    public static IActionResult ResponseDeleteMessageInChannelHelper(this ControllerBase controllerBase, OneOf<ResponseSuccessDefault, AppError> response)
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
    public static IActionResult ResponseFindUserAuthenticatedHelper(this ControllerBase controllerBase, OneOf<ResponseUserData, AppError> response)
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
    public static IActionResult ResponseRefreshTokenHelper(this ControllerBase controllerBase, OneOf<ResponseNewTokens, AppError> response)
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
}
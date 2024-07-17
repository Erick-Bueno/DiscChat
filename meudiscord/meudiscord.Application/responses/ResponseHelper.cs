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
                    if (error.errorType == ErrorType.Validation)
                        return controllerBase.BadRequest(error);

                    else if (error.errorType == ErrorType.Business)
                        return controllerBase.Conflict(error);

                    return controllerBase.StatusCode(500, "Erro interno do servidor");
                }
            );
    }
    public static IActionResult ResponseRegisterHelper(this ControllerBase controllerBase, OneOf<ResponseAuth, AppError> response)
    {
        return response.Match(
                      response => controllerBase.Created("Users/Guid", response),
                      error =>
                      {
                          if (error.errorType == ErrorType.Validation)
                              return controllerBase.BadRequest(error);
                          return controllerBase.StatusCode(500, "Erro interno do servidor");
                      });
    }
    public static IActionResult ResponseGetAllChannelsInServerHelper(this ControllerBase controllerBase, OneOf<ResponseAllChannels, AppError> response)
    {
        return response.Match(
                response => controllerBase.Ok(response),
                error =>
                {
                    if (error.errorType == ErrorType.Validation)
                        return controllerBase.BadRequest(error);

                    return controllerBase.StatusCode(500, "Erro interno do servidor");
                }
            );
    }
    public static IActionResult ResponseCreateChannelHelper(this ControllerBase controllerBase, OneOf<ResponseCreateChannel, AppError> response)
    {
        return response.Match(
              response => controllerBase.Created("localhost",response),
              error =>
              {
                  if (error.errorType == ErrorType.Validation)
                      return controllerBase.BadRequest(error);

                  return controllerBase.StatusCode(500, "Erro interno do servidor");
              }
          );
    }
    public static IActionResult ResponseGetAllGuildsHelper(this ControllerBase controllerBase, OneOf<ResponseAllGuilds, AppError> response)
    {
        return response.Match(
              response => controllerBase.Ok(response),
              error =>
              {
                  if (error.errorType == ErrorType.Business)
                      return controllerBase.Conflict(error);
                  return controllerBase.StatusCode(500, "Erro interno do servidor");
              }
          );
    }
    public static IActionResult ResponseCreateGuildHelper(this ControllerBase controllerBase, OneOf<ResponseCreateGuild, AppError> response)
    {
        return response.Match(
                response => controllerBase.Created("localhost",response),
                error =>
                {
                    if (error.errorType == ErrorType.Business)
                        return controllerBase.Conflict(error);
                    return controllerBase.StatusCode(500, "Erro interno do servidor");
                }

            );
    }
    public static IActionResult ResponseDeleteGuildHelper(this ControllerBase controllerBase, OneOf<ResponseSuccessDefault, AppError> response)
    {
        return response.Match(
              response => controllerBase.Ok(response),
              error =>
              {
                  if (error.errorType == ErrorType.Business)
                      return controllerBase.Conflict(error);
                  return controllerBase.StatusCode(500, "Erro interno do servidor");
              }
          );
    }
    public static IActionResult ResponseGetOldMessagesHelper(this ControllerBase controllerBase, OneOf<ResponseGetOldMessages, AppError> response){
        return response.Match(
            response => controllerBase.Ok(response),
            error => {
                if(error.errorType == ErrorType.Validation)
                    return controllerBase.BadRequest(error);
                return controllerBase.StatusCode(500, "Erro interno do servidor");
            }
        );
    }
    public static IActionResult ResponseDeleteMessageInChannelHelper(this ControllerBase controllerBase, OneOf<ResponseSuccessDefault, AppError> response){
        return response.Match(
            response => controllerBase.Ok(response),
            erro => {
                if(erro.errorType == ErrorType.Business){
                    return controllerBase.Conflict(erro);
                }
                return controllerBase.StatusCode(500, "Erro interno do servidor");
            }
        );
    }
    public static IActionResult ResponseFindUserAuthenticatedHelper(this ControllerBase controllerBase, OneOf<ResponseUserData, AppError> response){
        return response.Match(
            response => controllerBase.Ok(response),
            erro => {
                if (erro.errorType == ErrorType.Business){
                    return controllerBase.Conflict(erro);
                }
                return controllerBase.StatusCode(500, "Erro interno do servidor");
            }
        );
    }
}
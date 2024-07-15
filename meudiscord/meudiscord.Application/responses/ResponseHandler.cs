using Microsoft.AspNetCore.Mvc;

public static class ResponseHandler
{
   public static IActionResult ToActionResult(this ControllerBase controller, Response response)
    {

        return response.status switch
        {
            200 => controller.Ok(response),
            201 => controller.Created("localhost:7000",response),
            400 => controller.BadRequest(response),
            401 => controller.Unauthorized(response),
            404 => controller.NotFound(response),
            _ => controller.StatusCode(500, new { error = "Ocorreu um erro ao processar a solicitação." }),
        };
    }

}
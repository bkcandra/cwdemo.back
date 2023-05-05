using cwdemo.infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace cwdemo.api.Controllers
{
    /// <summary>
    /// A <see cref="ControllerBase"/> that implements Authorization and token claim access
    /// </summary>
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Handle service response and return api response to external instance
        /// </summary>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        protected IActionResult HandleServiceResponse<T>(T response) where T : ServiceResponse
        {
            if (response == null) return BadRequest();
            response.CorrelationToken = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            switch (response.StatusCode)
            {
                case (int)HttpStatusCode.BadRequest:
                    {
                        var noContentResponse = new ServiceResponse
                        {
                            CorrelationToken = response.CorrelationToken,
                            Message = response.Message,
                            StatusCode = response.StatusCode,
                            Valid = response.Valid,
                        };
                        return new ObjectResult(noContentResponse) { StatusCode = 400 };
                    }
                case (int)HttpStatusCode.NotFound:
                    {
                        var noContentResponse = new ServiceResponse
                        {
                            CorrelationToken = response.CorrelationToken,
                            Message = response.Message,
                            StatusCode = response.StatusCode,
                            Valid = response.Valid,
                        };
                        return new ObjectResult(noContentResponse) { StatusCode = 404 };
                    };
                case (int)HttpStatusCode.Forbidden:
                    {
                        var noContentResponse = new ServiceResponse
                        {
                            CorrelationToken = response.CorrelationToken,
                            Message = response.Message,
                            StatusCode = response.StatusCode,
                            Valid = response.Valid,
                        };
                        return new ObjectResult(noContentResponse) { StatusCode = 403 };
                    };
                case (int)HttpStatusCode.Unauthorized:
                    {
                        var noContentResponse = new ServiceResponse
                        {
                            CorrelationToken = response.CorrelationToken,
                            Message = response.Message,
                            StatusCode = response.StatusCode,
                            Valid = response.Valid,
                        };
                        return new ObjectResult(noContentResponse) { StatusCode = 401 };
                    }
                default: return Ok(response);
            }
        }
    }
}
﻿using API.Models.Respuesta;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Services
{
    public class ManejarRespuestaDeErrorService
    {
        public IActionResult ManejarRespuestaDeError(ResultadoOperacion resultado, Respuesta respuesta)
        {
            switch (resultado.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(respuesta);
                case HttpStatusCode.Conflict:
                    return new ConflictObjectResult(respuesta);
                default:
                    return new BadRequestObjectResult(respuesta);
            }
        }
    }
}

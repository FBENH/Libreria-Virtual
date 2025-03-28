﻿using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Data
{
    public class AutorData : IAutorData
    {
        private readonly LibreriaContext _context;
        private readonly IDataHelper _dataHelper;
        public AutorData(LibreriaContext context, IDataHelper dataHelper)
        {
            _context = context;
            _dataHelper = dataHelper;
        }

        public async Task<ResultadoOperacion> RegistrarAutor(Autor autor)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            Autor a = await _dataHelper.BuscarAutor(autor.Id);
            if (a != null)
            {
                resultado.Errores.Add($"Ya existe un autor registrado con el id {autor.Id}");
                resultado.StatusCode = HttpStatusCode.Conflict;
                return resultado;
            }

            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }

        public async Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            var a = await _context.Autores.Where(a => a.Id == idAutor)
                    .Select(a => new
                    {
                        Nombre = a.Nombre,
                        Nacionalidad = a.Nacionalidad,
                        FechaNacimiento = a.FechaNacimiento,
                        CantidadSuscritos = a.Suscripciones.Count(),
                        Libros = a.Libros
                    }).FirstOrDefaultAsync();                

            if (a == null)
            {
                resultado.Errores.Add($"No existe un autor con el id {idAutor}");
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }

            resultado.Exito = true;
            resultado.Data.Add(a);
            return resultado;
        }
    }
}

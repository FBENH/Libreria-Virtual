using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Models;
using Shared.Library.Enums;
using System;

namespace API.Configurations
{
    public static class DatosDePrueba
    {
        public static void SeedDatabase(LibreriaContext context)
        {
            if (!context.Autores.Any())
            {
                context.Autores.AddRange(
                    new Autor { Nombre = "Horacio Quiroga", Nacionalidad = "Uruguayo", FechaNacimiento = new DateTime(1878, 12, 31) },
                    new Autor { Nombre = "Jorge Luis Borges", Nacionalidad = "Argentino", FechaNacimiento = new DateTime(1899, 8, 24) },
                    new Autor { Nombre = "Gabriel García Márquez", Nacionalidad = "Colombiano", FechaNacimiento = new DateTime(1927, 3, 6) },
                    new Autor { Nombre = "Mario Vargas Llosa", Nacionalidad = "Peruano", FechaNacimiento = new DateTime(1936, 3, 28) },
                    new Autor { Nombre = "Julio Cortázar", Nacionalidad = "Argentino", FechaNacimiento = new DateTime(1914, 8, 26) },
                    new Autor { Nombre = "Isabel Allende", Nacionalidad = "Chilena", FechaNacimiento = new DateTime(1942, 8, 2) },
                    new Autor { Nombre = "Pablo Neruda", Nacionalidad = "Chileno", FechaNacimiento = new DateTime(1904, 7, 12) },
                    new Autor { Nombre = "Rubén Darío", Nacionalidad = "Nicaragüense", FechaNacimiento = new DateTime(1867, 1, 18) },
                    new Autor { Nombre = "Carlos Fuentes", Nacionalidad = "Mexicano", FechaNacimiento = new DateTime(1928, 11, 11) },
                    new Autor { Nombre = "Juan Rulfo", Nacionalidad = "Mexicano", FechaNacimiento = new DateTime(1917, 5, 16) }
                );
                context.SaveChanges();
            }

            if (!context.Libros.Any())
            {
                context.Libros.AddRange(
                    new Libro { Titulo = "Cuentos de la selva", Paginas = 120, Editorial = "Losada", ISBN = "978-987-566-882-0", FechaPublicacion = new DateTime(1918, 1, 1), Calificacion = 4.5, IdAutor = 1 },
                    new Libro { Titulo = "El almohadón de plumas", Paginas = 80, Editorial = "Planeta", ISBN = "978-950-742-400-6", FechaPublicacion = new DateTime(1917, 1, 1), Calificacion = 4.3, IdAutor = 1 },
                    new Libro { Titulo = "Anaconda", Paginas = 150, Editorial = "Emece", ISBN = "978-950-039-452-8", FechaPublicacion = new DateTime(1921, 1, 1), Calificacion = 4.4, IdAutor = 1 },
                    new Libro { Titulo = "Ficciones", Paginas = 174, Editorial = "Emece", ISBN = "978-950-039-023-0", FechaPublicacion = new DateTime(1944, 1, 1), Calificacion = 4.8, IdAutor = 2 },
                    new Libro { Titulo = "El Aleph", Paginas = 180, Editorial = "Alianza", ISBN = "978-842066800-2", FechaPublicacion = new DateTime(1949, 1, 1), Calificacion = 4.7, IdAutor = 2 },
                    new Libro { Titulo = "El libro de arena", Paginas = 190, Editorial = "Debolsillo", ISBN = "978-987-1138-61-4", FechaPublicacion = new DateTime(1975, 1, 1), Calificacion = 4.6, IdAutor = 2 },
                    new Libro { Titulo = "Cien años de soledad", Paginas = 417, Editorial = "Sudamericana", ISBN = "978-843760494-7", FechaPublicacion = new DateTime(1967, 1, 1), Calificacion = 4.9, IdAutor = 3 },
                    new Libro { Titulo = "El amor en los tiempos del cólera", Paginas = 368, Editorial = "Diana", ISBN = "978-030738714-1", FechaPublicacion = new DateTime(1985, 1, 1), Calificacion = 4.8, IdAutor = 3 },
                    new Libro { Titulo = "Crónica de una muerte anunciada", Paginas = 144, Editorial = "Mondadori", ISBN = "978-987-566-457-9", FechaPublicacion = new DateTime(1981, 1, 1), Calificacion = 4.7, IdAutor = 3 }
                );
                context.SaveChanges();
            }

            if (!context.Usuarios.Any())
            {
                var usuarios = new List<Usuario>
            {
                new Usuario { Id = new Guid("7182db59-7b04-461c-9794-990155e112cc"), Nombre = "Carlos", Email = "carlos@mail", UrlFoto = "foto1.jpg" },
                new Usuario { Id = new Guid("29087ab7-7169-4fc1-9e67-9ef18c8d3fd8"), Nombre = "María", Email = "maria@mail", UrlFoto = "foto2.jpg" },
                new Usuario { Id = new Guid("c62f22e2-22ef-4c49-adf4-f5d2b48dcb75"), Nombre = "Juan", Email = "juan@mail", UrlFoto = "foto3.jpg" },
                new Usuario { Id = new Guid("9ea299fe-5d26-41e4-beb3-5ce1ea5006db"), Nombre = "Lucía", Email = "lucia@mail", UrlFoto = "foto4.jpg" },
                new Usuario { Id = new Guid("e58c58b9-bc97-42ff-ba70-c4efb4b1c964"), Nombre = "Pedro", Email = "pedro@mail", UrlFoto = "foto5.jpg" },
                new Usuario { Id = new Guid("4face336-4750-432d-9022-f6c2d8c9d664"), Nombre = "Sofía", Email = "sofia@mail", UrlFoto = "foto6.jpg" },
                new Usuario { Id = new Guid("2494bc3d-4f76-44f2-8c40-b91f7bc7bf64"), Nombre = "Fernando", Email = "fernando@mail", UrlFoto = "foto7.jpg" },
                new Usuario { Id = new Guid("229db461-8e1d-4017-baef-e72b06153c79"), Nombre = "Laura", Email = "laura@mail", UrlFoto = "foto8.jpg" },
                new Usuario { Id = new Guid("697800ce-6c9f-4ce5-b938-452a57533371"), Nombre = "Diego", Email = "diego@mail", UrlFoto = "foto9.jpg" },
                new Usuario { Id = new Guid("e84cb6cf-f25b-44d1-b81d-b32d4f11f31d"), Nombre = "Valentina", Email = "valentina@mail", UrlFoto = "foto10.jpg" }
            };
                context.Usuarios.AddRange(usuarios);
                context.SaveChanges();

                context.Reviews.AddRange(
                    new Review { Opinion = "Gran libro, me encantó", Fecha = new DateTime(2024, 3, 30), Calificacion = Calificacion.Excelente, IdLibro = 1, IdUsuario = usuarios[0].Id },
                    new Review { Opinion = "Un poco denso, pero interesante", Fecha = new DateTime(2024, 3, 29), Calificacion = Calificacion.Bueno, IdLibro = 2, IdUsuario = usuarios[0].Id },
                    new Review { Opinion = "No me atrapó la historia", Fecha = new DateTime(2024, 3, 28), Calificacion = Calificacion.Regular, IdLibro = 3, IdUsuario = usuarios[0].Id },
                    new Review { Opinion = "Un clásico imprescindible", Fecha = new DateTime(2024, 3, 27), Calificacion = Calificacion.Excelente, IdLibro = 4, IdUsuario = usuarios[1].Id },
                    new Review { Opinion = "Maravilloso, me hizo reflexionar", Fecha = new DateTime(2024, 3, 26), Calificacion = Calificacion.Excelente, IdLibro = 5, IdUsuario = usuarios[1].Id },
                    new Review { Opinion = "Lo recomiendo", Fecha = new DateTime(2024, 3, 25), Calificacion = Calificacion.Bueno, IdLibro = 6, IdUsuario = usuarios[1].Id },
                    new Review { Opinion = "Obra maestra", Fecha = new DateTime(2024, 3, 24), Calificacion = Calificacion.Excelente, IdLibro = 7, IdUsuario = usuarios[2].Id },
                    new Review { Opinion = "Increíble narrativa", Fecha = new DateTime(2024, 3, 23), Calificacion = Calificacion.Excelente, IdLibro = 8, IdUsuario = usuarios[2].Id },
                    new Review { Opinion = "Interesante, pero esperaba más", Fecha = new DateTime(2024, 3, 22), Calificacion = Calificacion.Bueno, IdLibro = 9, IdUsuario = usuarios[2].Id }
                );
                context.SaveChanges();
            }

            if (!context.Suscripciones.Any())
            {
                context.Suscripciones.AddRange(
                    new Suscripcion { IdAutor = 1, IdUsuario = new Guid("7182db59-7b04-461c-9794-990155e112cc") },
                    new Suscripcion { IdAutor = 2, IdUsuario = new Guid("7182db59-7b04-461c-9794-990155e112cc") },
                    new Suscripcion { IdAutor = 3, IdUsuario = new Guid("7182db59-7b04-461c-9794-990155e112cc") },
                    new Suscripcion { IdAutor = 1, IdUsuario = new Guid("c62f22e2-22ef-4c49-adf4-f5d2b48dcb75") },
                    new Suscripcion { IdAutor = 2, IdUsuario = new Guid("c62f22e2-22ef-4c49-adf4-f5d2b48dcb75") },
                    new Suscripcion { IdAutor = 5, IdUsuario = new Guid("c62f22e2-22ef-4c49-adf4-f5d2b48dcb75") }
                );
                context.SaveChanges();
            }
        }
    }
}

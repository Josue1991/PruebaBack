using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using Microsoft.EntityFrameworkCore;
using Model.Modelos;
using Servicios.impl;

namespace Servicios
{
    public class EventoService : IEventoService
    {
        private readonly tiendaEntities1 _context;

        public EventoService(tiendaEntities1 context)
        {
            _context = context;
        }

        // Crear un nuevo evento
        public async Task<bool> crearEvento(EventosDTO nuevo)
        {
            try
            {
                var evento = new Evento
                {
                    idEvento = nuevo.Id,
                    fecha = nuevo.Fecha,
                    lugar = nuevo.Lugar,
                    descripcion = nuevo.Descripcion,
                    precio = nuevo.Precio,
                    estado = 1
                };

                _context.Evento.Add(evento);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepción o log
                Console.WriteLine($"Error al crear evento: {ex.Message}");
                return false;
            }
        }

        // Actualizar un evento existente
        public async Task<bool> actualizarEvento(EventosDTO elemento)
        {
            try
            {
                var evento = await _context.Evento
                    .FirstOrDefaultAsync(e => e.idEvento == elemento.Id && e.estado == 1);

                if (evento == null) return false;

                evento.fecha = elemento.Fecha;
                evento.lugar = elemento.Lugar;
                evento.descripcion = elemento.Descripcion;
                evento.precio = elemento.Precio;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepción o log
                Console.WriteLine($"Error al actualizar evento: {ex.Message}");
                return false;
            }
        }

        // Listar todos los eventos activos
        public async Task<List<EventosDTO>> ListarEventos()
        {
            try
            {
                var eventos = await _context.Evento
                    .Where(e => e.estado == 1)
                    .Select(e => new EventosDTO
                    {
                        Id = e.idEvento,
                        Fecha = e.fecha,
                        Lugar = e.lugar,
                        Descripcion = e.descripcion,
                        Precio = e.precio
                    })
                    .ToListAsync();

                return eventos;
            }
            catch (Exception ex)
            {
                // Manejo de excepción o log
                Console.WriteLine($"Error al listar eventos: {ex.Message}");
                return null;
            }
        }

        // Obtener un evento por su ID
        public async Task<List<EventosDTO>> listaEventosID(int id)
        {
            try
            {
                var eventos = await _context.Evento
                    .Where(e => e.idEvento == id && e.estado == 1)
                    .Select(e => new EventosDTO
                    {
                        Id = e.idEvento,
                        Fecha = e.fecha,
                        Lugar = e.lugar,
                        Descripcion = e.descripcion,
                        Precio = e.precio
                    })
                    .ToListAsync();

                return eventos;
            }
            catch (Exception ex)
            {
                // Manejo de excepción o log
                Console.WriteLine($"Error al obtener evento por ID: {ex.Message}");
                return null;
            }
        }

        // Eliminación lógica de un evento (marcar como inactivo)
        public async Task<bool> eliminarEvento(int id)
        {
            try
            {
                var evento = await _context.Evento
                    .FirstOrDefaultAsync(e => e.idEvento == id);

                if (evento == null) return false;

                evento.estado = 0;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepción o log
                Console.WriteLine($"Error al eliminar evento: {ex.Message}");
                return false;
            }
        }
    }
}

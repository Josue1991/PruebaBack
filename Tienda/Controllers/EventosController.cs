using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Modelos;
using Servicios.impl;  // Asegúrate de que la interfaz esté en el namespace correcto
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly ILogger<EventosController> _logger;
        private readonly IEventoService _eventosService;  // Asegúrate de usar el nombre correcto de la interfaz

        public EventosController(ILogger<EventosController> logger, IEventoService eventosService)
        {
            _logger = logger;
            _eventosService = eventosService;
        }

        // GET: api/Eventos
        [HttpGet(Name = "GetEventos")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventosService.ListarEventos();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error obteniendo eventos: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/Eventos
        [HttpPost(Name = "CrearEventos")]
        public async Task<IActionResult> Crear([FromBody] EventosDTO nuevoEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool creado = await _eventosService.crearEvento(nuevoEvento);
                if (creado)
                {
                    return Ok("Evento creado exitosamente");
                }
                else
                {
                    return StatusCode(500, "No se pudo crear el evento");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creando evento: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/Eventos
        [HttpPut(Name = "ActualizarEvento")]
        public async Task<IActionResult> Actualizar([FromBody] EventosDTO eventoActualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool actualizado = await _eventosService.actualizarEvento(eventoActualizado);
                if (actualizado)
                {
                    return Ok("Evento actualizado exitosamente");
                }
                else
                {
                    return NotFound("Evento no encontrado");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error actualizando evento: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}

using Model.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.impl
{
    public interface IEventoService
    {
        Task<List<EventosDTO>> ListarEventos();
        Task<List<EventosDTO>> listaEventosID(int id);
        Task<bool> crearEvento(EventosDTO nuevo);
        Task<bool> actualizarEvento(EventosDTO elemento);
        Task<bool> eliminarEvento(int id);
    }
}

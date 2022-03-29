using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CodersCoffee.MinApi.Service
{
    public class PedidoService : IPedidoService
    {
        private PedidoDbContext _context;

        public PedidoService(PedidoDbContext context)
        {
            _context = context;
        }

        public List<Pedido> Get()
        {
            return _context.Pedidos.ToList();
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);            
        }

        public Pedido Add(Pedido item)
        {
            _context.Pedidos.Add(item);
            _context.SaveChanges();

            return item;
        }

        public void Update(int id, Pedido newItem)
        {
            var pedido = _context.Pedidos.FirstOrDefault(x => x.Id == id);
            pedido.Descricao = newItem.Descricao;                        
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var pedido = _context.Pedidos.FirstOrDefault(x => x.Id == id);
            _context.Pedidos.Remove(pedido);

            _context.SaveChanges();
        }
    }
}

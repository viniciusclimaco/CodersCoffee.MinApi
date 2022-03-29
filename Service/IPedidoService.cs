namespace CodersCoffee.MinApi.Service
{    public interface IPedidoService
    {
        Pedido Add(Pedido item);
        void Delete(int id);
        Task<Pedido?> GetByIdAsync(int id);
        List<Pedido> Get();
        void Update(int id, Pedido newItem);
    }
}

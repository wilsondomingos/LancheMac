using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories
{
    public class PedidoRepository : IPedidosRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
           pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems;

            foreach (var Carrinhoitem in carrinhoCompraItens)
            {
                var PedidoDetalhe = new PedidoDetalhe
                {
                    Quantidade = Carrinhoitem.Quantidade,
                    LancheId = Carrinhoitem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = Carrinhoitem.Lanche.Preco
                };
                _appDbContext.PedidoDetalhes.Add(PedidoDetalhe);
               // _appDbContext.PedidosDetalhes.Add(PedidoDetalhe);
            }
            _appDbContext.SaveChanges();
        }
    }
}

using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
         private readonly IPedidosRepository _pedidoRepository;
         private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidosRepository petosRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = petosRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        [HttpGet]

        public IActionResult Checkout()
        {
            return View();  
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totaItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            // Obtem os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = items;

            //Verificar se existem itens de pedido

            if(_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho está vazio, que tal incluir um lanche");
            
            }

            // Se existir itens no carrinho vou calcular o total de itens e o total do pedido
            foreach(var item in items)
            {
                totaItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            // Vou atribuir os valores obtidos ao pedido
            pedido.TotalItensPedido = totaItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //Validar os dados do pedido

            if(ModelState.IsValid)
            {
                //Criar o pedido e os detalhes
                _pedidoRepository.CriarPedido(pedido);

                //Definir mensagem ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                // limpar o carrinho do Cliente
                _carrinhoCompra.LimparCarrinho();

                //Exibe a view com dados do cliente e do pedido

                return View("~/Views/Pedido/CheckoutCompleto.cshtml",pedido);


            }

            return View(pedido);

        }
    }
}

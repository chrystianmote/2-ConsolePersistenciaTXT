using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vendas2
{
    class ItemVenda
    {
        public int Quantidade;
        public Venda Venda;
        public Produto Produto;

        public void CadastrarItens(Produto p, Cliente c, ref List<ItemVenda> lista)
        {
            Console.WriteLine("--------------------------------------------\n");
            if (!ItemVenda.ProdutoRepetido(p, lista))
            {
                Console.WriteLine("Código venda: {0:D3}", this.Venda.Codigo);
                Console.WriteLine("Cliente: {0}", c.Nome);
                this.Venda.Comprador = c;
                this.Venda.Data = DateTime.Now;
                Console.WriteLine("Data venda: {0}\n", DateTime.Now);
                Console.Write("Informe o ICMS do produto: ");
                while (!double.TryParse(Console.ReadLine(), out this.Venda.ICMS))
                {
                    Console.WriteLine("Inválido. Digite Novamente: ");
                }
                Console.Write("\nDigite a Quantidade do Produto: ");
                while (!int.TryParse(Console.ReadLine(), out this.Quantidade))
                {
                    Console.WriteLine("Inválido. Digite Novamente: ");
                }
                int est = (p.Estoque - this.Quantidade);
                while (est < 0)
                {
                    Console.Write("Quantidade acima do Estoque! Digite novamente: ");
                    while (!int.TryParse(Console.ReadLine(), out this.Quantidade))
                    {
                        Console.WriteLine("Inválido. Digite Novamente: ");
                    }
                    est = (p.Estoque - this.Quantidade);
                }
                p.Estoque = (p.Estoque - this.Quantidade);

                this.Produto = p;

                this.Venda.ValorTotal = (this.Produto.Precovenda * this.Quantidade);
                if (lista.Count != 0)
                {
                    decimal valor = this.Venda.ValorTotal;
                    this.Venda.ValorTotal = lista[lista.Count - 1].Venda.ValorTotal + valor;
                }
                Console.WriteLine(" Até o momento o valor total é {0}", this.Venda.ValorTotal);
                lista.Add(this);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("-------------------------------------\n");
                Console.WriteLine("Código venda: {0:D3}", this.Venda.Codigo);
                Console.WriteLine("Cliente: {0}", c.Nome);
                Console.WriteLine("ICMS: {0:F2}% ", this.Venda.ICMS);
                Console.Write("\nDigite a Quantidade a mais do Produto: ");
                while (!int.TryParse(Console.ReadLine(), out this.Quantidade))
                {
                    Console.WriteLine("Inválido. Digite Novamente: ");
                }
                int est = (p.Estoque - this.Quantidade);
                while (est < 0)
                {
                    Console.Write("Quantidade acima do Estoque! Digite novamente: ");
                    while (!int.TryParse(Console.ReadLine(), out this.Quantidade))
                    {
                        Console.WriteLine("Inválido. Digite Novamente: ");
                    }
                    est = (p.Estoque - this.Quantidade);
                }
                p.Estoque = (p.Estoque - this.Quantidade);

                this.Produto = p;

                this.Venda.ValorTotal = (this.Produto.Precovenda * this.Quantidade);
                if (lista.Count != 0)
                {
                    decimal valor = this.Venda.ValorTotal;
                    this.Venda.ValorTotal = lista[lista.Count - 1].Venda.ValorTotal + valor;
                }
                Console.WriteLine(" Até o momento o valor total é {0}", this.Venda.ValorTotal);
                lista.Add(this);
                Console.ReadKey();
            }
        }

        private static bool ProdutoRepetido(Produto p, List<ItemVenda> lista)
        {
            foreach (var iv in lista)
            {
                if (iv.Produto == p)
                {
                    lista.Remove(iv);
                    return true;
                }
            }
            return false;
        }


    }

}

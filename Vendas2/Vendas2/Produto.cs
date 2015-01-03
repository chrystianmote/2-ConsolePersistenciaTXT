using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Vendas2
{
    class Produto
    {
        public int Codigo;
        public string Nome;
        public decimal Precocusto;
        public decimal Precovenda;
        public int Estoque;
        public List<ItemVenda> Itens;

        public void CadastrarProduto(ref List<Produto> lista)
        {
            Console.Title = "Cadastro de Produto";
            if (lista.Count != 0)
            {
                this.Codigo = lista.Count + 1;
            }
            else
            {
                this.Codigo = 1;
            }
            Console.WriteLine("Codigo: {0:D3}\n\n", this.Codigo);
            Console.Write("Digite Nome do Produto: ");
            this.Nome = Console.ReadLine();
            do
            {
                Console.Write("Digite Preço de custo: ");
                while (!decimal.TryParse(Console.ReadLine(), out this.Precocusto))
                {
                    Console.WriteLine("Inválido Digite Novamente: ");
                }
                Console.Write("Digite Preço de venda: ");
                while (!decimal.TryParse(Console.ReadLine(), out this.Precovenda))
                {
                    Console.WriteLine("Inválido Digite Novamente: ");
                }
            } while (this.Precocusto > this.Precovenda);

            Console.Write("Digite Estoque: ");
            while (!int.TryParse(Console.ReadLine(), out this.Estoque))
            {
                Console.WriteLine("Inválido Digite Novamente: ");
            }
            this.Itens = new List<ItemVenda>();
            Console.WriteLine("\nCadastro Efetuado com Sucesso!!");
            Console.ReadKey();
            lista.Add(this);

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Produtos.txt"))
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Produtos.txt");

            Produto.Escrever(lista);
        }


        public static void Escrever(List<Produto> listaP)
        {
           StreamWriter swp = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Produtos.txt");
            
            foreach (Produto p in listaP)
            {
                swp.WriteLine("{0}|{1}|{2}|{3}|{4}",
                    p.Codigo,
                    p.Nome,
                    p.Estoque,
                    p.Precocusto,
                    p.Precovenda);

            }
            swp.Close();
        }

        public static void Loading(ref List<Produto> listaP)
        {
            StreamReader srp = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Produtos.txt");
            while (!srp.EndOfStream)
            {
                string linha = srp.ReadLine();
                string[] dados = linha.Split('|');
                Produto p = new Produto();
                p.Codigo = Convert.ToInt32(dados[0]);
                p.Nome = dados[1];
                p.Estoque = Convert.ToInt32(dados[2]);
                p.Precocusto = Convert.ToDecimal(dados[3]);
                p.Precovenda = Convert.ToDecimal(dados[4]);
                p.Itens = new List<ItemVenda>();
                listaP.Add(p);
            }
            srp.Close();
        }

        public static void ListarProduto(List<Produto> lista)
        {
            Console.Clear();
            if (lista.Count != 0)
            {
                Console.WriteLine(" ");
                Console.WriteLine("\t           Lista Produtos");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Cód  Nome               Preço Venda        Estoque");
                Console.WriteLine("---------------------------------------------------\n\n");
                foreach (Produto p in lista)
                {
                    Console.WriteLine("{0:D3} {1}  R${2:F2}              {3}", p.Codigo, 
                        p.Nome.PadRight(20),p.Precovenda,p.Estoque.ToString().PadRight(20));
                }
            }
            else
            {
                Console.WriteLine("\n\n Produto não Cadastrado!");
            }

        }
        public static Produto Pesquisar(string nome, List<Produto> list)
        {
            int cod;
            if (int.TryParse(nome, out cod))
            {
                foreach (Produto p in list)
                {
                    if (p.Codigo == cod)
                    {
                        return p;
                    }
                }
            }
            else
            {
                foreach (Produto p in list)
                {
                    if (p.Nome.ToLower() == nome.ToLower())
                    {
                        return p;
                    }
                }
            }
            return null;
        }
    }
}

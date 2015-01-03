using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Vendas2
{
    class Program
    {
        static byte Opcoes()
        {
            Console.Clear();
            Console.Title = "Controle de Vendas";
            Console.WriteLine("\n---------------------------------- ");
            Console.WriteLine("\tCadastro de Vendas  ");
            Console.WriteLine("----------------------------------\n\n");
            Console.WriteLine(" 1 - Cadastrar Cliente ");
            Console.WriteLine(" 2 - Cadastrar Produto ");
            Console.WriteLine(" 3 - Cadastrar Venda ");
            Console.WriteLine(" 4 - Listar Cliente");
            Console.WriteLine(" 5 - Listar Produto ");
            Console.WriteLine(" 6 - Listar Venda ");
            Console.WriteLine(" 7 - Persistir todas as Alterações Básicas");
            Console.WriteLine(" 8 - Sair \n\n");
            Console.WriteLine("---------------------");
            Console.Write(" Informe a Opção Desejada: ");
            byte op;
            while (!byte.TryParse(Console.ReadLine(), out op))
            {
                Console.Write("\n\n\t Opção Inválida! Digite novamente: ");
            }
            if (op != 8)
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("\n\n Obrigado por utilizar este programa...");
                Console.ReadKey();
            }
            return op;
        }
         static List<Cliente> Clientes = new List<Cliente>();
         static List<Produto> Produtos = new List<Produto>();
         static List<Venda> Vendas = new List<Venda>();

        static void Main(string[] args)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt"))
                Cliente.Loading(ref Clientes);
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Produtos.txt"))
                Produto.Loading(ref Produtos);
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Vendas.txt"))
                Venda.Loading(ref Vendas, Clientes);

            bool sair = false;
            Produto p;
            Cliente c;
            do
            {
                switch (Opcoes())
                {
                    case 1:
                        c = new Cliente();
                        c.CadastrarCliente(ref Clientes);
                        break;
                    case 2:
                        p = new Produto();
                        p.CadastrarProduto(ref Produtos);
                        break;
                    case 3:
                        Venda.CadastrarVenda(ref Vendas,Clientes,ref Produtos);                   
                        break;
                    case 4:
                        Cliente.ListarClientes(Clientes);
                        Console.ReadKey();
                        break;
                    case 5:
                        Produto.ListarProduto(Produtos);
                        Console.ReadKey();
                        break;
                    case 6:
                        Venda.ListarVenda(Vendas);
                        Console.ReadKey();
                        break;
                    case 7:
                         bool operacao = Program.PersistirAlteracoes(Clientes, Produtos, Vendas);
                         if (operacao)
                             Console.WriteLine("Alteração realizada com Sucesso!");
                         else
                             Console.WriteLine("Nenhum Uso!");
                         Console.ReadKey();
                        break;
                    default:
                        sair = true;
                        break;
                }
            } while (!sair);
        }


        private static bool PersistirAlteracoes(List<Cliente> C, List<Produto> P, List<Venda> V)
        {
            bool ret = false;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Produtos.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Produtos.txt");
                Produto.Escrever(P);
                ret = true;
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt");
                Cliente.Escrever(C);
                ret = true;
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Vendas.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Vendas.txt");
                Venda.Escrever(V);
                ret = true;
            }
            return ret;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace CadastroCliente
{
    class Program
    {
        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("\t---------------------");
            Console.WriteLine("\t-CADASTRO DE CLIENTE-");
            Console.WriteLine("\t---------------------");
            Console.WriteLine("\t1 - Inserir Novo Cliente");
            Console.WriteLine("\t2 - Alterar Cliente");
            Console.WriteLine("\t3 - Excluir Cliente");
            Console.WriteLine("\t4 - Listar Cliente");
            Console.WriteLine("\t5 - Pesquisar Cliente");
            Console.WriteLine("\t6 - Sair");
            Console.WriteLine("\t---------------------");
        }
        static string ProcessarOpcao()
        {
            Console.Write("\tOpção Desejada: ");
            string op = Console.ReadLine();
            switch (op)
            {
                case "1":
                    Cliente obj = new Cliente();
                    int IdCliente;
                    if (obj.Inserir(ref ListaCliente, out IdCliente))
                    {
                        Console.WriteLine("O Cliente foi inserido com código {0}.", IdCliente);
                        Console.ReadKey();
                    }
                    Cliente.Persistir(ListaCliente);
                    break;
                case "2":
                    break;
                case "3":
                    Cliente.Excluir(ref ListaCliente);
                    break;
                case "4":
                    Cliente.ListarCliente(ListaCliente);
                    break;
                case "5":
                    break;
                case "6":
                    Console.WriteLine("Programa Finalizado...");
                    break;
                default:
                    break;
            }
            return op;
        }

        static List<Cliente> ListaCliente = new List<Cliente>();

        static void Main(string[] args)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt"))
            {
                Cliente.Carregar(ref ListaCliente);
            }
            string op = "";
            do
            {
                MostrarMenu();
                op = ProcessarOpcao();
            } while (op != "5");
        }
    }
}

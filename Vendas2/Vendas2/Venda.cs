using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Vendas2
{
    class Venda
    {
        public int Codigo;
        public DateTime Data;
        public double ICMS;
        public decimal ValorTotal;
        public Cliente Comprador;
        public List<ItemVenda> Itens;

        private static byte Opcao()
        {
            Console.Clear();
            byte op;
            Console.Title = "Fechando Venda...";
            Console.WriteLine("\n---------------------------------- ");
            Console.WriteLine("\tCadastro de Vendas  ");
            Console.WriteLine("----------------------------------\n\n");
            Console.WriteLine(" 1 - Informar Cliente ");
            Console.WriteLine(" 2 - Informar Produtos ");
            Console.WriteLine(" 3 - Fechar Venda");
            Console.WriteLine(" 4 - Sair \n\n");

            Console.WriteLine("---------------------");
            Console.Write(" \nInforme a Opção Desejada: ");
            while (!byte.TryParse(Console.ReadLine(), out op))
            {
                Console.Write("\n\n\t Opção Inválida! Digite novamente: ");
            }
            if (op != 4)
            {
                Console.Clear();
            }
            else 
            {
                Console.WriteLine("\n\nVENDA NÃO CONCLUÍDA...");
                Console.ReadKey();
            }
                
            return op;
        }
        public static void CadastrarVenda(ref List<Venda> lista, List<Cliente> client, ref List<Produto> prod)
        {
            Venda v = new Venda();
            v.Itens = new List<ItemVenda>();
            ItemVenda it;
            bool sair = false;
            do
            {
                switch (Opcao())
                {
                    case 1:
                        Cliente.ListarClientes(client);
                        Console.Write("\n\nInforme Código ou Nome do Cliente: ");
                        v.Comprador = Cliente.Pesquisar(Console.ReadLine(), client);
                        if (v.Comprador != null)
                        {
                            Console.WriteLine("Etapa 1: Concluído! press \"ENTER\"");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("\n\n\\nEste Cliente não existe!");
                            Console.ReadKey();
                            Console.Clear();
                            goto case 1;
                        }
                        break;
                    case 2:
                        bool cont = false;
                        do
                        {
                            Produto.ListarProduto(prod);
                            Console.WriteLine("\n\nInforme Código ou Nome do Produto: ");
                            Console.Write("Digite: ");
                            it = new ItemVenda();
                            it.Produto = Produto.Pesquisar(Console.ReadLine(), prod);
                            if (it.Produto != null)
                            {
                                if (lista.Count != 0)
                                {
                                    v.Codigo = lista.Count + 1;
                                    it.Venda = new Venda();
                                    it.Venda.Codigo = v.Codigo;
                                }
                                else
                                {
                                    v.Codigo = 1;
                                    it.Venda = v;
                                }
                                
                                it.CadastrarItens(it.Produto, v.Comprador, ref v.Itens);
                                Venda.AlterarEstoque(ref prod, it.Produto);
                                Console.WriteLine("Adicionar mais produto? S ou N");
                                if (Console.ReadLine().ToUpper().Equals("N"))
                                {
                                    v = it.Venda;
                                    v.Comprador.Compras.Add(v);
                                    Console.WriteLine("Etapa 2: Concluído! press \"ENTER\"");
                                    Console.ReadKey();
                                    cont = false;
                                }
                                else
                                {
                                    cont = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n\n\nEste Produto não existe!");
                                Console.ReadKey();
                                cont = true;
                            }
                        } while (cont);
                        break;
                    case 3:
                        Console.WriteLine("Deseja realmente fechar os produtos adicionados? S ou N");
                        if (Console.ReadLine().ToUpper().Equals("S"))
                        {
                            lista.Add(v);
                            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Vendas.txt"))
                                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Vendas.txt");

                            Venda.Escrever(lista);
                            Console.WriteLine("Etapa 3: Venda Cadastrada!");
                            Console.ReadKey();
                        }
                        break;
                    case 4:
                        sair = true;
                        break;
                }
            } while (!sair);
        }

        public static void Escrever(List<Venda> listaV)
        {
            StreamWriter swv = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Vendas.txt");
            foreach (Venda v in listaV)
            {
                swv.WriteLine("{0}|{1}|{2}|{3}|{4}",
                    v.Codigo,
                    v.Comprador.Nome,
                    v.ICMS,
                    v.ValorTotal,
                    v.Data.ToShortDateString());
            }
            swv.Close();
        }

        public static void Loading(ref List<Venda> listaV,List<Cliente> clientes)
        {
            StreamReader srv = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Vendas.txt");
            while (!srv.EndOfStream)
            {
                string linha = srv.ReadLine();
                string[] dados = linha.Split('|');
                Venda v = new Venda();
                v.Codigo = Convert.ToInt32(dados[0]);

                v.Comprador = new Cliente();
                v.Comprador = Cliente.Pesquisar(dados[1],clientes);

                v.ICMS = Convert.ToDouble(dados[2]);
                v.ValorTotal = Convert.ToDecimal(dados[3]);
                v.Data = Convert.ToDateTime(dados[4]);
                v.Itens = new List<ItemVenda>();
                listaV.Add(v);
            }
            srv.Close();
        }


        private static void AlterarEstoque(ref List<Produto> prod, Produto produto)
        {
            foreach (Produto p in prod)
            {
                if (p.Codigo == produto.Codigo)
                {
                    prod.Remove(p);
                    prod.Add(produto);
                    break;
                }
            }
        }


        public static void ListarVenda(List<Venda> lista)
        {
            if (lista.Count != 0)
            {
                Console.WriteLine(" ");
                Console.WriteLine("\t Lista Vendas");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Cód Comprador                Valor Total         Data");
                Console.WriteLine("---------------------------------------------------------\n\n");
                foreach (Venda v in lista)
                {
                    Console.WriteLine("{0:D3} {1} {2:F2}            {3:dd/MM/yyyy}", v.Codigo, 
                        v.Comprador.Nome.PadRight(25), v.ValorTotal, v.Data);
                }
            }
            else
            {
                Console.WriteLine("\n\n Vendas não Cadastradas!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Vendas2
{
    class Cliente
    {
        public int Codigo;
        public string Nome;
        public DateTime DataNasc;
        public string Telefone;
        public string Email;
        public List<Venda> Compras;

        public void CadastrarCliente(ref List<Cliente> cl)
        {
            Console.Title = "Cadastro de Cliente";
            if (cl.Count != 0)
                this.Codigo = cl.Count + 1;
            else
                this.Codigo = 1;

            Console.WriteLine("Codigo: {0:D3}\n\n", this.Codigo);
            Console.Write("Digite um Nome: ");
            this.Nome = Console.ReadLine();
            Console.Write("Digite Data de Nascimento (dd/MM/yyyy): ");
            while (!DateTime.TryParse(Console.ReadLine(), out this.DataNasc))
            {
                Console.WriteLine("Inválido Digite Novamente: ");
            }
            Console.Write("Digite o Telefone: ");
            this.Telefone = Console.ReadLine();

            Console.Write("Digite um E-mail: ");
            string vefemail = Console.ReadLine();
            char[] vfmail = vefemail.ToArray();
            bool ok = false;
            do
            {
                for (int i = 0; i < vfmail.Length; i++)
                {
                    if ((vfmail[i] == '@'))
                    {
                        for (int j = 0; j < vfmail.Length; j++)
                        {
                            if ((vfmail[j] == '.'))
                            {
                                this.Email = vefemail;
                                ok = true;
                                break;
                            }

                        }
                        break;
                    }
                }
                if (!ok)
                {
                    Console.Write("Digite um E-mail Válido: ");
                    vefemail = Console.ReadLine();
                    vfmail = vefemail.ToArray();
                }
            } while (!ok);

            this.Compras = new List<Venda>();
            cl.Add(this);

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt"))
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt");

            Cliente.Escrever(cl);

            Console.WriteLine("\n Cadastro Efetuado com Sucesso!!");
            Console.ReadKey();
        }

        public static void Escrever(List<Cliente> listaC)
        {
            StreamWriter swc = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt");
            foreach (Cliente c in listaC)
            {
                swc.WriteLine("{0}|{1}|{2}|{3}|{4}",
                     c.Codigo,
                     c.Nome,
                     c.Telefone,
                     c.DataNasc.ToShortDateString(),
                     c.Email);
            }
            swc.Close();
            
        }

        public static void Loading(ref List<Cliente> listaC)
        {
            StreamReader src = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt");
            while (!src.EndOfStream)
            {
                string linha = src.ReadLine();
                string[] dados = linha.Split('|');
                Cliente c = new Cliente();
                c.Codigo = Convert.ToInt32(dados[0]);
                c.Nome = dados[1];
                c.Telefone = dados[2];
                c.DataNasc = Convert.ToDateTime(dados[3]);
                c.Email = dados[4];
                c.Compras = new List<Venda>();
                listaC.Add(c);
            }
            src.Close();
        }


        public static void ListarClientes(List<Cliente> lista)
        {
            if (lista.Count != 0)
            {
                Console.WriteLine(" ");
                Console.WriteLine("\t Lista Clientes");
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Cód  Nome                  Telefone                E-mail");
                Console.WriteLine("-----------------------------------------------------------------------\n\n");
                foreach (Cliente c in lista)
                {
                    Console.WriteLine("{0:D3} {1} {2} {3}", c.Codigo, c.Nome.PadRight(20),
                        c.Telefone.PadRight(25), c.Email.PadRight(20));
                }
            }
            else
            {
                Console.WriteLine("\n\n Cliente não Cadastrado!");
            }
        }
        public static Cliente Pesquisar(string nome, List<Cliente> list)
        {
            int cod;
            if (int.TryParse(nome, out cod))
            {
                foreach (Cliente c in list)
                {
                    if (c.Codigo == cod)
                    {
                        return c;
                    }
                }
            }
            else
            {
                foreach (Cliente c in list)
                {
                    if (c.Nome.ToLower() == nome.ToLower())
                    {
                        return c;
                    }
                }
            }
            return null;
        }
    }

}

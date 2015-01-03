using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CadastroCliente
{
    enum Estatus
    {
        Pendente = 1,
        Ativo,
        Inativo
    }
    class Cliente
    {
        public int IdCliente;
        public string Nome;
        public string Endereco;
        public string Email;
        public decimal Saldo;
        public Estatus Status;
        public DateTime DataNascimento;

        private static void MostrarListaStatus()
        {
            for (Estatus i = Estatus.Pendente; i <= Estatus.Inativo; i++)
            {
                Console.WriteLine();
                MessageBox.Show(i.ToString());
            }
        }
        private static int ObterMaiorId(List<Cliente> lista)
        {
            int maior = 0;
            foreach (Cliente c in lista)
            {
                if (c.IdCliente > maior)
                    maior = c.IdCliente;
            }
            return maior;
        }
        public static void Excluir(ref List<Cliente> lista)
        {
            int idCliente;
            while (!int.TryParse(Console.ReadLine(), out idCliente))//retorna um true ou false e o out para a variável escolhida
            {
                Console.WriteLine("Código Inválido. Novo Código");
            }
            bool excluiu = false;
            foreach (var c in lista)
            {
                if (c.IdCliente == idCliente)
                {
                    lista.Remove(c);
                }
            }
            if (excluiu)
            {
                Console.WriteLine("Cliente excluído com sucesso");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado...");
            }
        }
        public static void ListarCliente(List<Cliente> lista)
        {
            Console.Clear();
            Console.WriteLine("\t---------------------");
            Console.WriteLine("\t--LISTA DE CLIENTES--");
            Console.WriteLine("\t---------------------");
            Console.WriteLine("\tCódigo  Nome");
            foreach (Cliente c in lista)
            {
                Console.WriteLine("\t{0:D4} {1}", c.IdCliente, c.Nome);
            }
            Console.ReadKey();
        }
        public static void Persistir(List<Cliente> lista)
        {
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt");// Pasta destino do excecutavel
            foreach (Cliente c in lista)
            {
                sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                     c.IdCliente,
                     c.Nome,
                     c.Endereco,
                     c.Email,
                     c.Saldo,
                     (int)c.Status,
                     c.DataNascimento.ToShortDateString());
            }
            sw.Close();
        }
        public static void Carregar(ref List<Cliente> lista)
        {
            StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Clientes.txt");
            while (!sr.EndOfStream)//até o fim do arquivo
            {
                string linha = sr.ReadLine();
                string[] dados = linha.Split('|');// o Split ele corta os pedaços da strin jogando pro objeto
                Cliente c = new Cliente();
                c.IdCliente = Convert.ToInt32(dados[0]);
                c.Nome = dados[1];
                c.Endereco = dados[2];
                c.Email = dados[3];
                c.Saldo = Convert.ToDecimal(dados[4]);
                Enum.TryParse<Estatus>(dados[5], out c.Status);
                c.DataNascimento = Convert.ToDateTime(dados[6]);
                lista.Add(c);
            }
            sr.Close();
        }
        /// <summary>
        /// Insere um novo Cliente em uma lista passada como parametro
        /// </summary>
        /// <param name="lista">Lista em que o cliente será inserido</param>
        /// <param name="idCliente">valor do atributo IdCliente gerado automaticamente</param>
        /// <returns>True se der certo o cadastro senao False</returns>
        public bool Inserir(ref List<Cliente> lista, out int idCliente)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("--------------------------------");
                Console.WriteLine("--------INSERIR CLIENTE---------");
                Console.WriteLine("--------------------------------");

                this.IdCliente = Cliente.ObterMaiorId(lista) + 1;
                idCliente = this.IdCliente;

                Console.Write("Nome: ");
                this.Nome = Console.ReadLine();
                Console.Write("Endereço: ");
                this.Endereco = Console.ReadLine();
                Console.Write("Email: ");
                this.Email = Console.ReadLine();
                Console.Write("Saldo: ");
                while (!decimal.TryParse(Console.ReadLine(), out this.Saldo))
                {
                    Console.Write("Valor Inválido. Novo Saldo: ");
                }
                Console.WriteLine("Escolha o Status: ");
                Cliente.MostrarListaStatus();
                Console.Write("Status: ");
                while (!Enum.TryParse<Estatus>(Console.ReadLine(), out this.Status))
                {
                    Console.Write("Status Inválido. Novo Status: ");
                }
                Console.Write("Data de Nascimento: ");
                while (!DateTime.TryParse(Console.ReadLine(), out this.DataNascimento))
                {
                    Console.Write("Data Inválida. Nova Data: ");
                }
                lista.Add(this);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um Erro ao Inserir um Cliente: {0}", e.Message);
                idCliente = 0;
                return false;
            }
        }
    }
}

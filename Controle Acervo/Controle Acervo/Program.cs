using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Controle_Acervo
{
    enum Pesquisa
    {
        Por_Código = 1,
        Por_Título,
        Por_Autor
    }
    class Program
    {
        static byte Opcoes()
        {
            Console.Clear();
            Console.Title = "Controle de Acervo";
            Console.WriteLine("\n---------------------------------- ");
            Console.WriteLine("\tCadastro de Exemplares  ");
            Console.WriteLine("----------------------------------\n\n");
            Console.WriteLine(" 1 - Cadastrar um Exemplar ");
            Console.WriteLine(" 2 - Excluir Exemplar ");
            Console.WriteLine(" 3 - Alterar Exemplar ");
            Console.WriteLine(" 4 - Pesquisar Exemplares");
            Console.WriteLine(" 5 - Listar Exemplares ");
            Console.WriteLine(" 6 - Extrair um Exemplar em Arquivo ");
            Console.WriteLine(" 7 - Sair \n\n");
            Console.WriteLine("---------------------");
            Console.Write(" Informe a Opção Desejada: ");
            byte op;
            while (!byte.TryParse(Console.ReadLine(), out op))
            {
                Console.Write("\n\n\t Opção Inválida! Digite novamente: ");
            }
            if (op != 7)
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

        static List<Exemplar> Exemplares = new List<Exemplar>();

        static void Main(string[] args)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Exemplares.txt"))
                Exemplar.Loading(ref Exemplares);

            List<Exemplar> resultado;
            Exemplar ex;
            bool sair = false;
            do
            {
                switch (Opcoes())
                {
                    case 1:
                        ex = new Exemplar();
                        ex.InserirAlterar(ref Exemplares);
                        break;
                    case 2:
                        if (Exemplares.Count != 0)
                        {
                            ex = new Exemplar();
                            Exemplar.Listar(Exemplares);
                            ex.ExcluirExemplar(ref Exemplares);
                        }
                        else
                        {
                            Console.WriteLine("\n\n\t Cadastre um Exemplar!");
                            Console.ReadKey();
                            goto case 1;
                        }
                        break;
                    case 3:
                        if (Exemplares.Count != 0)
                        {
                            int ind = -1;
                            Exemplar excl = null;
                            Exemplar alt = new Exemplar();
                            Console.WriteLine("\n\n");
                            Console.WriteLine("\t-------------------------------------");
                            Console.Write("\n\t Digite o código do Exemplar para Alterar: ");
                            int cdgo;
                            while (!int.TryParse(Console.ReadLine(), out cdgo))
                            {
                                Console.Write("\n\n\t Código Inválido! Digite novamente: ");
                            }
                            for (int i = 0; i < Exemplares.Count; i++)
                            {
                                if (Exemplares[i].IdExemplar == cdgo)
                                {
                                    ind = i;
                                    excl = Exemplares[i];
                                }
                            }
                            if (excl != null)
                            {
                                excl.ExcluirExemplar(ref Exemplares, ind);
                                alt.InserirAlterar(ref Exemplares, cdgo, ind);
                            }
                            else
                            {
                                Console.WriteLine("\n\n");
                                Console.WriteLine("\t--------------------------");
                                Console.WriteLine("\t Nenhum Exemplar Encontrado!");
                                Console.WriteLine("\t--------------------------");
                                Console.ReadKey();
                            }
                        }
                        break;
                    case 4:
                        Console.Title = "PESQUISAR EXEMPLAR";
                        for (Pesquisa i = Pesquisa.Por_Código; i <= Pesquisa.Por_Autor; i++)
                        {
                            Console.WriteLine("\n\t {0} - {1}", (int)i, i.ToString().Replace("_", " "));
                        }
                        Console.WriteLine("\t--------------------------");
                        Console.Write("\tEscolha a sua opção de Pesquisa: ");
                        byte op;
                        while (!byte.TryParse(Console.ReadLine(), out op))
                        {
                            Console.Write("\n\n\t Opção Inválida! Digite novamente: ");
                        }
                        switch (op)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("\n\n");
                                Console.WriteLine("\t-------------------------------------");
                                Console.Write("\n\t Digite o código do Exemplar: ");
                                Exemplar obj = Exemplar.Pesquisar(Convert.ToInt32(Console.ReadLine()), Exemplares);
                                if (obj != null)
                                {
                                    Console.WriteLine("\n\n");
                                    Console.WriteLine("\t--------------------------------------------");
                                    Console.WriteLine("\tCódigo: {0:D4}", obj.IdExemplar);
                                    Console.WriteLine("\tCódMid//Tipo: {0:D3} - {1}", obj.TipoMidia.IdTipoMidia, obj.TipoMidia.Nome);
                                    Console.WriteLine("\tTítulo: {0}", obj.Titulo);
                                    Console.WriteLine("\tAutor: {0}", obj.Estado);
                                    Console.WriteLine("\tEstado: {0}", obj.Autor);
                                    Console.WriteLine("\tLançamento: {0:yyyy}", obj.DataLancamento);
                                    Console.WriteLine("\t--------------------------------------------");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("\n\n");
                                    Console.WriteLine("\t--------------------------");
                                    Console.WriteLine("\t Nenhum Exemplar Encontrado!");
                                    Console.WriteLine("\t--------------------------");
                                    Console.ReadKey();
                                }
                                break;
                            case 2:
                                Console.Clear();
                                resultado = new List<Exemplar>();
                                Console.WriteLine("\n\n");
                                Console.WriteLine("\t-------------------------------------");
                                Console.Write("\n\t Digite o Título do Exemplar: ");
                                resultado = Exemplar.Pesquisar(Console.ReadLine(), Exemplares);
                                if (resultado.Count != 0)
                                {
                                    Exemplar.Listar(resultado);
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("\n\n");
                                    Console.WriteLine("\t--------------------------");
                                    Console.WriteLine("\t Nenhum Exemplar Encontrado!");
                                    Console.WriteLine("\t--------------------------");
                                    Console.ReadKey();
                                }
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("\n\n");
                                Console.WriteLine("\t-------------------------------------");
                                Console.Write("\n\t Digite o Autor do Exemplar: ");
                                Exemplar.Pesquisar(Console.ReadLine(), Exemplares, out resultado);
                                if (resultado.Count != 0)
                                {
                                    Exemplar.Listar(resultado);
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("\n\n");
                                    Console.WriteLine("\t--------------------------");
                                    Console.WriteLine("\t Nenhum Exemplar Encontrado!");
                                    Console.WriteLine("\t--------------------------");
                                    Console.ReadKey();
                                }
                                break;
                            default:
                                Console.Write("\n\n\t Opção Inválida!");
                                Console.ReadKey();
                                break;
                        }
                        break;
                    case 5:
                        Exemplar.Listar(Exemplares);
                        Console.ReadKey();
                        break;
                    case 6:
                        StreamWriter valor = new StreamWriter("C:\\Exemplar.txt", true, Encoding.Unicode);
                        if (Exemplares.Count != 0) 
                        {
                            Exemplar.Listar(Exemplares);
                            Console.WriteLine("\n\n");
                            Console.WriteLine("\t-------------------------------------");
                            Console.Write("\n\t Digite o código do Exemplar: ");
                            int cdgo;
                            while (!int.TryParse(Console.ReadLine(),out cdgo))
                            {
                                Console.Write("\n\n\t Código Inválido! Digite novamente: ");
                            }
                            try
                            {
                                string es = "  ";
                                valor.WriteLine("-------------------------------------------------------------------------------");
                                valor.WriteLine("Cód  Título               Autor                Estado      Tipo      Lançamento");
                                valor.WriteLine("-------------------------------------------------------------------------------");
                                valor.WriteLine(es);
                                foreach (Exemplar e in Exemplares)
                                {
                                    if (e.IdExemplar == cdgo)
                                    {
                                        valor.Write(e.IdExemplar.ToString());
                                        valor.Write(es);
                                        valor.Write(e.Titulo.ToString().PadRight(20));
                                        valor.Write(es);
                                        valor.Write(e.Autor.ToString().PadRight(20));
                                        valor.Write(es);
                                        valor.Write(e.Estado.ToString().PadRight(10));
                                        valor.Write(es);
                                        valor.Write(e.TipoMidia.Nome.ToString().PadRight(10));
                                        valor.Write("{0:dd/MM/yyyy}", e.DataLancamento);
                                        valor.WriteLine(es);
                                    }
                                }
                                valor.Close();
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine("Exception: {0}", e.Message);
                                Console.ReadKey();
                            }
                            finally
                            {
                                Console.WriteLine("\tO Arquivo foi gravado em C:\\");
                                Console.WriteLine("\tArquivo gravado com sucesso!");
                                Console.ReadKey();
                            }
                        }
                        
                        break;
                    case 7:
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("\n\n\t Opção Inválida!");
                        Console.ReadKey();
                        break;
                }
            } while (!sair);
        }
    }
}

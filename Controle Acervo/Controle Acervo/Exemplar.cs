using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Controle_Acervo
{
    enum EnumEstados
    {
        Ruim = 1,
        Regular,
        Bom,
        Excelente
    }
    class Exemplar
    {
        public int IdExemplar;
        public string Autor;
        public string Titulo;
        public DateTime DataLancamento;
        public EnumEstados Estado;
        public TipoMidia TipoMidia;

        public void InserirAlterar(ref List<Exemplar> lista, int cdgo = 0, int ind = -1)
        {
            Console.Title = "CADASTRO DE EXEMPLAR";
            Console.WriteLine("\n\t-----------------------------------------------");
            Console.WriteLine("\t-----------------------------------------------");
            TipoMidia tm = new TipoMidia();
            tm.Escolher(out tm.IdTipoMidia, out tm.Nome);
            this.TipoMidia = tm;
            if (cdgo == 0)
            {
                if (lista.Count != 0) 
                {
                    this.IdExemplar = lista[lista.Count - 1].IdExemplar + 1;
                    Console.WriteLine("\t Código do Exemplar {0:D4}", this.IdExemplar);
                }
                else
                {
                    this.IdExemplar = 1;
                    Console.WriteLine("\t Código do Exemplar {0:D4}", this.IdExemplar);
                }
            }
            else
            {
                this.IdExemplar = cdgo;
            }
            Console.Write("\t Digite o Título do Exemplar: ");
            this.Titulo = Console.ReadLine();
            Console.Write("\t Digite o Nome do Autor: ");
            this.Autor = Console.ReadLine();
            Console.Write("\t Digite a Data do Lançamento do Exemplar: ");
            while (!DateTime.TryParse(Console.ReadLine(), out this.DataLancamento))
            {
                Console.Write("\n\t Data Inválida! Digite a Data Novamente: ");
            }
            Console.WriteLine("\n");
            for (EnumEstados i = EnumEstados.Ruim; i <= EnumEstados.Excelente; i++)
            {
                Console.WriteLine("\t{0} - {1}", (int)i, i);
            }
            Console.Write("\n\t Digite o Estado da Mídia: ");
            while (!Enum.TryParse<EnumEstados>(Console.ReadLine(), out this.Estado))
            {
                Console.Write("\n\t Estado Inválido! Digite o Estado Novamente: ");
            }
            if (cdgo == 0)
            {
                lista.Add(this);
                Exemplar.Escrever(lista);
                Console.WriteLine("\n\t Cadastro Efetuado com Sucesso!");
                Console.ReadKey();
            }
            else
            {
                lista.Insert(ind, this);
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Exemplares.txt");
                Exemplar.Escrever(lista);
                Console.WriteLine("\n\t Cadastro Alterado com Sucesso!");
                Console.ReadKey();
            }

        }

        private static void Escrever(List<Exemplar> lista)
        {
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Exemplares.txt");
            foreach (Exemplar e in lista)
            {
                sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                     e.IdExemplar,
                     e.Titulo,
                     e.Autor,
                     e.DataLancamento.ToShortDateString(),
                     e.Estado,
                     e.TipoMidia.IdTipoMidia,
                     e.TipoMidia.Nome);
            }
            sw.Close();
        }

        public static void Loading(ref List<Exemplar> lista)
        {
            StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Exemplares.txt");
            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                string[] dados = linha.Split('|');
                Exemplar e = new Exemplar();
                TipoMidia tm = new TipoMidia();
                e.IdExemplar = Convert.ToInt32(dados[0]);
                e.Titulo = dados[1];
                e.Autor = dados[2];
                e.DataLancamento = Convert.ToDateTime(dados[3]);
                Enum.TryParse<EnumEstados>(dados[4], out e.Estado);
                tm.IdTipoMidia = Convert.ToInt32(dados[5]);
                tm.Nome = dados[6];
                e.TipoMidia = tm;
                lista.Add(e);
            }
            sr.Close();
        }

        public static void Listar(List<Exemplar> lista)
        {
            Console.Title = "LISTA DE EXEMPLARES";
            Console.WriteLine("\t----------------------------------------------------------------------------");
            Console.WriteLine("\tCód  Título               Autor                Estado     Tipo    Lançamento");
            Console.WriteLine("\t----------------------------------------------------------------------------\n\n");
            foreach (Exemplar e in lista)
            {
                Console.WriteLine("\t{0:D4} {1} {2} {3} {4} {5:yyyy}", e.IdExemplar, e.Titulo.PadRight(20),
                    e.Autor.PadRight(20), e.Estado.ToString().PadRight(10), e.TipoMidia.Nome.PadRight(10),
                    e.DataLancamento);
            }
        }

        public void ExcluirExemplar(ref List<Exemplar> lista, int ind = -1)
        {
            Console.Title = "EXCLUSÃO DO ACERVO";
            Exemplar excl = null;
            if (ind == -1)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("\t-------------------------------------");
                Console.Write("\n\t Digite o código do Exemplar para excluir: ");
                this.IdExemplar = Convert.ToInt32(Console.ReadLine());
                foreach (Exemplar ex in lista)
                {
                    if (ex.IdExemplar == this.IdExemplar)
                    {
                        excl = ex;
                        break;
                    }
                }
                if (excl != null)
                {
                    lista.Remove(this);
                    Exemplar.Escrever(lista);
                    Console.WriteLine("\n\n\t Exemplar Excluído com sucesso!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\n\n\t Exemplar não Encontrado!");
                    Console.ReadKey();
                }
            }
            else
            {
                lista.RemoveAt(ind);
            }

        }

        public static Exemplar Pesquisar(int cod, List<Exemplar> listaA)
        {
            foreach (Exemplar item in listaA)
            {
                if (item.IdExemplar == cod)
                {
                    return item;
                }
            }
            return null;
        }

        public static List<Exemplar> Pesquisar(string titulo, List<Exemplar> listB)
        {
            List<Exemplar> resultado = new List<Exemplar>();
            foreach (Exemplar item in listB)
            {
                if (item.Titulo.Contains(titulo))
                {
                    resultado.Add(item);
                }
            }
            return resultado;
        }

        public static void Pesquisar(string autor, List<Exemplar> listB, out List<Exemplar> resultado)
        {
            resultado = new List<Exemplar>();
            foreach (Exemplar item in listB)
            {
                if (item.Autor.Contains(autor))
                {
                    resultado.Add(item);
                }
            }
        }
    }
}

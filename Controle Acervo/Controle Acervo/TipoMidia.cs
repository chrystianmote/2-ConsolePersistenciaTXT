using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controle_Acervo
{
    class TipoMidia
    {
        enum Midia
        {
            CD = 1,
            Livro,
            Revista,
            Jornal,
            Outro
        }
        public int IdTipoMidia;
        public string Nome;
        public void Escolher(out int id, out string nome)
        {
            for (Midia i = Midia.CD; i <= Midia.Outro; i++)
            {
                Console.WriteLine("\n\t{0} - {1}", (int)i, i);
            }
            Console.Write("\n\t Digite o Tipo do Acervo: ");
            switch (Convert.ToByte(Console.ReadLine()))
            {
                    
                case 1:
                    Console.Clear();
                    nome = Midia.CD.ToString();
                    id = 1;
                    Console.WriteLine("\n\n\t Código da Mídia {0:D4}", id);
                    break;
                case 2:
                    Console.Clear();
                    nome = Midia.Livro.ToString();
                    id = 2;
                    Console.WriteLine("\n\n\t Código da Mídia {0:D4}", id);
                    break;
                case 3:
                    Console.Clear();
                    nome = Midia.Revista.ToString();
                    id = 3;
                    Console.WriteLine("\n\n\t Código da Mídia {0:D4}", id);
                    break;
                case 4:
                    Console.Clear();
                    nome = Midia.Jornal.ToString();
                    id = 4;
                    Console.WriteLine("\n\n\t Código da Mídia {0:D4}", id);
                    break;
                default:
                    Console.Clear();
                    Console.Write("\n\t Digite o Nome do Tipo da Mídia: ");
                    nome = Console.ReadLine();
                    Console.Write("\n\t Digite um Código: ");
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Write("\n\t Código Inválido! Digite o Código Novamente: ");
                    }
                    break;
            }
        }
    }
}

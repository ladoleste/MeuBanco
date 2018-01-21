using System.Collections.Generic;

namespace MeuBanco
{
    public class Cedula
    {
        public static readonly List<Cedula> CedulasCadastradas = new List<Cedula>
        {
            new Cedula {Nome = "Cem Reais", Valor = 100},
            new Cedula {Nome = "Cinquneta Reais", Valor = 50},
            new Cedula {Nome = "Vinte Reais", Valor = 20},
            new Cedula {Nome = "Dez Reais", Valor = 10},
            new Cedula {Nome = "Cinco Reais", Valor = 5}
        };
        
        public string Nome { get; set; }
        public int Valor { get; set; }
    }
}
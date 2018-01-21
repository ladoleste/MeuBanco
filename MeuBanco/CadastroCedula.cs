using System.Collections.Generic;

namespace MeuBanco
{
    public class CadastroCedula
    {
        public static List<Cedula> CedulasCadastradas = new List<Cedula>
        {
            new Cedula {Nome = "Cem Reais", Valor = 100},
            new Cedula {Nome = "Cinquneta Reais", Valor = 50},
            new Cedula {Nome = "Vinte Reais", Valor = 20},
            new Cedula {Nome = "Dez Reais", Valor = 10},
            new Cedula {Nome = "Cinco Reais", Valor = 5}
        };
    }
}
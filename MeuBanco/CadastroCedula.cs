using System.Collections.Generic;

namespace MeuBanco
{
    /// <summary>
    /// Cédulas as quais o caixa eletrônico está autorizado a trabalhar.
    /// </summary>
    public static class CadastroCedula
    {
        public static readonly List<Cedula> CedulasCadastradas = new List<Cedula>
        {
            new Cedula {Nome = "Cem Reais", Valor = 100},
            new Cedula {Nome = "Cinquneta Reais", Valor = 50},
            new Cedula {Nome = "Vinte Reais", Valor = 20},
            new Cedula {Nome = "Dez Reais", Valor = 10},
            new Cedula {Nome = "Cinco Reais", Valor = 5}
        };
    }
}
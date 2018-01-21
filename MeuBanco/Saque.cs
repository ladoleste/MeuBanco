using System;

namespace MeuBanco
{
    public class Saque : Lancamento
    {
        public Saque(decimal saque)
        {
            DataHora = DateTime.Now;
            Valor = saque * -1;
        }
    }
}
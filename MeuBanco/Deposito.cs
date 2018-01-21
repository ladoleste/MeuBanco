using System;

namespace MeuBanco
{
    public class Deposito : Lancamento
    {
        public Deposito(decimal dep)
        {
            DataHora = DateTime.Now;
            Valor = dep;
        }
    }
}
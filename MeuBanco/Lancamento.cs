using System;

namespace MeuBanco
{
    public abstract class Lancamento
    {
        public DateTime DataHora { get; protected set; }
        public decimal Valor { get; protected set; }
    }
}
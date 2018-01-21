using System;
using System.Collections.Generic;

namespace MeuBanco
{
    public abstract class Lancamento
    {
        public DateTime DataHora { get; set; }
        public decimal Valor { get; set; }
    }

    public class Saque : Lancamento
    {
        public Saque(decimal saque)
        {
            DataHora = DateTime.Now;
            Valor = saque * -1;
        }
    }

    public class Deposito : Lancamento
    {
        public Deposito(decimal dep)
        {
            DataHora = DateTime.Now;
            Valor = dep;
        }
    }

    public class Cedula
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }

    public class SaldoCedula
    {
        public int QtdeCedula { get; set; }
    }

    public class CaixaEletronico
    {
        private decimal _saldo;
        private readonly List<Lancamento> _lancamentos = new List<Lancamento>();

        public IEnumerable<Lancamento> ExibirExtrato()
        {
            return _lancamentos;
        }

        public decimal ExibirSaldo()
        {
            return _saldo;
        }

        public void RealizaDeposito(Deposito deposito)
        {
            _lancamentos.Add(deposito);
            _saldo += deposito.Valor;
        }

        public void RealizaSaque(Saque saque)
        {
            _lancamentos.Add(saque);
            _saldo += saque.Valor;
        }
    }
}
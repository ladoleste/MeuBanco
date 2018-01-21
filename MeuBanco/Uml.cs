using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuBanco
{
    public abstract class Lancamento
    {
        public DateTime DataHora { get; protected set; }
        public decimal Valor { get; protected set; }
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

    public class CaixaEletronico
    {
        private readonly List<Lancamento> _lancamentos = new List<Lancamento>();
        private List<Cedula> Cedulas { get; set; } = new List<Cedula>();
        
        public IEnumerable<Lancamento> ExibirExtrato()
        {
            return _lancamentos;
        }

        public List<Cedula> OberCedulas()
        {
            return Cedulas;
        }

        public int ExibirSaldo()
        {
            return Cedulas.Sum(x => (int) x);
        }

        public void Depositar(List<Cedula> cedulas)
        {
            var valor = 0;
            cedulas.ForEach(x => valor += (int)x);
            _lancamentos.Add(new Deposito(valor));
            Cedulas.AddRange(cedulas);
        }

        public void Sacar(Saque saque)
        {
            var lista = Cedulas.OrderByDescending(x => (int) x).ToList();
            var lista2 = new List<Cedula>(lista);
            var valorSaque = Math.Abs(saque.Valor);
            foreach (var cedula in lista2)
            {
                if((int)cedula > valorSaque)
                    continue;
                lista.Remove(cedula);
                valorSaque -= (int) cedula;
                if(valorSaque == 0)
                    break;
            }

            Cedulas = lista;
            _lancamentos.Add(saque);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuBanco
{
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

        public int ObterSaldo()
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
            var cedulas = Cedulas.OrderByDescending(x => (int) x).ToList();
            var copiaCedulas = new List<Cedula>(cedulas);
            var valorSaque = Math.Abs(saque.Valor);
            foreach (var cedula in copiaCedulas)
            {
                if((int)cedula > valorSaque)
                    continue;
                cedulas.Remove(cedula);
                valorSaque -= (int) cedula;
                if(valorSaque == 0)
                    break;
            }

            Cedulas = cedulas;
            _lancamentos.Add(saque);
        }
    }
}
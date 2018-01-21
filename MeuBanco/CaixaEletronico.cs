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
            return Cedulas.Sum(x => x.Valor);
        }

        public void Depositar(IEnumerable<int> valores)
        {
            var lista = valores.Select(valor => CadastroCedula.CedulasCadastradas.Single(x => x.Valor == valor)).ToList();
            Depositar(lista);
        }

        public void Depositar(List<Cedula> cedulas)
        {
            var valor = 0;
            cedulas.ForEach(x => valor += x.Valor);
            _lancamentos.Add(new Deposito(valor));
            Cedulas.AddRange(cedulas);
        }

        /// <summary>
        /// Realiza o saque.
        /// Ao chegar nesse ponto já é sabido que a quantidade de notas vai atender ao pedido do cliente.
        /// É feita apenas uma ordenação para garantir que as notas mais altas serão utilizadas primeiro.
        /// </summary>
        /// <param name="saque">Valor a sacar</param>
        public void Sacar(Saque saque)
        {
            var cedulas = Cedulas.OrderByDescending(x => x.Valor).ToList();
            var copiaCedulas = new List<Cedula>(cedulas);
            var valorSaque = Math.Abs(saque.Valor);

            foreach (var cedula in copiaCedulas)
            {
                if (cedula.Valor > valorSaque)
                    continue;
                cedulas.Remove(cedula);
                valorSaque -= cedula.Valor;
                if (valorSaque == 0)
                    break;
            }

            Cedulas = cedulas;
            _lancamentos.Add(saque);
        }
    }
}
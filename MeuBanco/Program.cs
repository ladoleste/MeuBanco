using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuBanco
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var caixa = new CaixaEletronico();
            
            //Carga inicial
            caixa.Depositar(new[] {100, 50, 50, 20, 20, 20, 10, 10, 10, 10, 5, 5, 5, 5, 5});

            while (true)
            {
                MostrarMenu();

                Enum.TryParse(Console.ReadKey().KeyChar.ToString(), true, out Menu opcao);

                switch (opcao)
                {
                    case Menu.Extrato:
                        Console.Clear();
                        ExibirExtrato(caixa);
                        Fim();
                        break;
                    case Menu.Saldo:
                        Console.Clear();
                        ExibirSaldo(caixa);
                        Fim();
                        break;
                    case Menu.Deposito:
                        Console.Clear();
                        RealizaDeposito(caixa);
                        Fim();
                        break;
                    case Menu.Saque:
                        Console.Clear();
                        RealizaSaque(caixa);
                        Fim();
                        break;
                    case Menu.Sair:
                        Console.Clear();
                        Console.WriteLine("\n\nVolte sempre!\n");
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine(" não é uma opção válida!");
                        Fim();
                        break;
                }
            }
        }

        /// <summary>
        /// Realiza o saque considerando as notas disponíveis.1
        /// 
        /// </summary>
        /// <param name="caixa">O caixa eletrônico que processará o saque</param>
        private static void RealizaSaque(CaixaEletronico caixa)
        {
            if (caixa.ObterSaldo() == 0)
            {
                Console.Write("\nSaque indisponível");
                return;
            }

            while (true)
            {
                var valorSaque = 0;
                while (valorSaque == 0)
                {
                    Console.Write("\nInforme o valor a sacar: ");
                    valorSaque = Console.ReadLine().ToInt();

                    if (valorSaque <= 0) Console.WriteLine("Valor inválido");
                }

                if (valorSaque > caixa.ObterSaldo())
                    Console.WriteLine(string.Format("Valor indisponível\nEste equipamento só dispõe de {0:C2}",
                        caixa.ObterSaldo()));
                else
                {
                    var menorCedula = caixa.OberCedulas().Min(x => x.Valor);
                    if (valorSaque % menorCedula > 0)
                    {
                        Console.WriteLine("O valor precisa ser multiplo de " + menorCedula);
                        continue;
                    }

                    caixa.Sacar(new Saque(valorSaque));
                    Console.WriteLine("Saque realizado com sucesso!");
                }

                break;
            }
        }

        /// <summary>
        /// Realiza um depósito considerando as notas informadas.
        /// Deve-se informar primeiro o tipo de nota e na sequencia a quantidade de notas desse tipo.
        /// </summary>
        /// <param name="caixa">O caixa eletrônico que processará o depósito</param>
        private static void RealizaDeposito(CaixaEletronico caixa)
        {
            Console.WriteLine("\nTipos de notas aceitas:\n");

            foreach (var tipoCedula in CadastroCedula.CedulasCadastradas)
                Console.WriteLine(string.Format("{1} ({0})", tipoCedula.Nome, tipoCedula.Valor));

            Cedula cedula = null;
            while (cedula == null)
            {
                Console.Write("\nInforme o valor da nota: ");
                var valor = Console.ReadLine().ToInt();
                cedula = CadastroCedula.CedulasCadastradas.SingleOrDefault(x => x.Valor == valor);
                if (cedula == null)
                    Console.WriteLine("Tipo não suportado!");
            }

            var qtd = 0;
            while (qtd <= 0)
            {
                Console.Write("\nInforme a quantidade de notas a depositar: ");
                int.TryParse(Console.ReadLine(), out qtd);
                if (qtd <= 0)
                    Console.WriteLine("Quantidade inválida!");
            }

            var cedulas = new List<Cedula>();
            for (var i = 0; i < qtd; i++)
                cedulas.Add(cedula);

            caixa.Depositar(cedulas);

            Console.WriteLine("Depósito realizado com sucesso!");
        }

        /// <summary>
        /// Exibe um relatório de notas disponíveis e o saldo atual.
        /// </summary>
        /// <param name="caixa">O caixa que processará a informação</param>
        private static void ExibirSaldo(CaixaEletronico caixa)
        {
            Console.WriteLine("\n*** Cédulas disponíveis ***\n");

            foreach (var cedula in CadastroCedula.CedulasCadastradas)
            {
                var count = caixa.OberCedulas().Count(x => x == cedula);
                if (count > 0)
                    Console.WriteLine(string.Format("{0} nota(s) de {1}", count, cedula.Nome));
            }

            Console.WriteLine(string.Format("\nValor total: {0:C2}", caixa.ObterSaldo()));
        }

        /// <summary>
        /// Exibe o histórico de lançamentos com data e hora.
        /// </summary>
        /// <param name="caixa">O caixa que processará a informação</param>
        private static void ExibirExtrato(CaixaEletronico caixa)
        {
            Console.WriteLine("\n*** Últimos Lançamentos ***\n");
            Console.WriteLine("Tipo  Data       Hora  Valor");

            foreach (var lancamento in caixa.ObterLancamentos())
                Console.WriteLine(
                    string.Format("{0}     {1} {2}  {3:C2}",
                        lancamento.Valor > 0 ? "C" : "D",
                        lancamento.DataHora.ToShortDateString(),
                        lancamento.DataHora.ToShortTimeString(),
                        lancamento.Valor));
        }

        /// <summary>
        /// Exibe uma mensagem padrão para o fim de um ação.
        /// </summary>
        private static void Fim()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...\n");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Exibe o menu principal do programa.
        /// </summary>
        private static void MostrarMenu()
        {
            Console.Clear();

            Console.WriteLine("\n*** MENU PRINCIPAL ***\n");

            foreach (var menu in Enum.GetValues(typeof(Menu)))
                Console.WriteLine(string.Format("{0}. {1}", (int) menu, menu.ToString()));

            Console.Write("\nInforme a opção desejada: ");
        }
    }
}
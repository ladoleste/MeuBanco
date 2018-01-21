using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace MeuBanco
{
    internal static class Program
    {
// ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            var caixa = new CaixaEletronico();
            caixa.RealizaDeposito(new Deposito(5));
            caixa.RealizaDeposito(new Deposito(25));
            caixa.RealizaDeposito(new Deposito(35));
            caixa.RealizaSaque(new Saque(100));

            while (true)
            {
                MostrarMenu();

                var opcao = Enum.Parse(typeof(Menu), Console.ReadKey().KeyChar.ToString());

                switch (opcao)
                {
                    case Menu.Extrato:
                        Console.Clear();
                        Console.WriteLine("\n*** Últimos Lançamentos ***\n");
                        Console.WriteLine("Tipo  Data        Valor");

                        foreach (var lancamento in caixa.ExibirExtrato())
                        {
                            Console.WriteLine(
                                string.Format("{2}     {0:d}  {1:C2}", lancamento.DataHora, lancamento.Valor,
                                    lancamento.Valor > 0 ? "C" : "D"));
                        }

                        Fim();
                        break;
                    case Menu.Saldo:
                        Console.Clear();
                        Console.WriteLine(string.Format("\nSeu saldo atual é de: {0:C2}", caixa.ExibirSaldo()));
                        Fim();
                        break;
                    case Menu.Deposito:
                        Console.Clear();
                        Console.Write("\nInforme o valor a depositar: ");
                        var valorDeposito = Console.ReadLine().ToDecimal();

                        if (valorDeposito > 0)
                        {
                            caixa.RealizaDeposito(new Deposito(valorDeposito));
                            Console.WriteLine("Depósito realizado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido");
                        }

                        Fim();
                        break;
                    case Menu.Saque:
                        Console.Clear();
                        Console.Write("\nInforme o valor a sacar: ");
                        var valorSaque = Console.ReadLine().ToDecimal();

                        if (valorSaque > 0)
                        {
                            caixa.RealizaSaque(new Saque(valorSaque));
                            Console.WriteLine("Saque realizado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido");
                        }

                        Fim();
                        break;
                    case Menu.Carregamento:
                        Console.Clear();
                        Console.WriteLine("\n\nNão disponível no momento!\n");
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

        private static void Fim()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...\n");
            Console.ReadKey(true);
        }

        private static void MostrarMenu()
        {
            Console.Clear();

            Console.WriteLine("\n*** MENU PRINCIPAL ***\n");

            foreach (Menu foo in Enum.GetValues(typeof(Menu)))
            {
                Console.WriteLine(string.Format("{0}. {1}", (int) foo, foo.ToString()));
            }

            Console.Write("\nInforme a opção desejada: ");
        }
    }
}
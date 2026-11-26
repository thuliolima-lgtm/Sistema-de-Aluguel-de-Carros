
using System;
using System.Collections.Generic;
using System.IO;
using LocadoraCarros;

class Program
{
    static void Main()
    {
        List<CarroAluguel> listaCarros = new List<CarroAluguel>();
        carregarDados(listaCarros, "carros.csv");

        int opcao;

        do
        {
            opcao = menu();

            switch (opcao)
            {
                case 1:
                    addCarro(listaCarros);
                    break;

                case 2:
                    mostrarCarros(listaCarros);
                    break;

                case 3:
                    Console.WriteLine("Valor de busca da placa:");
                    string placaBusca = Console.ReadLine();

                    bool placa = buscarPorPlaca(listaCarros, placaBusca);

                    if (placa == false)
                    {
                        Console.WriteLine("Não encontrada!");
                    }
                    break;

                case 4:
                    // Chame a função verificarMaiorMenorValor
                    break;

                case 5:
                    mostrarCarrosDisponiveis(listaCarros);
                    break;

                case 6:
                    mostrarCarrosAlugados(listaCarros);
                    break;

                case 0:
                    Console.WriteLine("Salvando dados e encerrando...");
                    salvarDados(listaCarros, "carros.csv");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            if (opcao != 0)
            {
                Console.ReadKey();
                Console.Clear();
            }

        } while (opcao != 0);
    }

    static int menu()
    {
        Console.WriteLine("=== SISTEMA DE ALUGUEL DE CARROS ===");
        Console.WriteLine("1 - Cadastrar Novo Carro");
        Console.WriteLine("2 - Mostrar Todos os Carros da Frota");
        Console.WriteLine("3 - Buscar Carro pela Placa");
        Console.WriteLine("4 - Verificar Carro de Maior e Menor Valor de Aluguel");
        Console.WriteLine("5 - Mostrar Apenas Carros Disponíveis");
        Console.WriteLine("6 - Mostrar Apenas Carros Alugados");
        Console.WriteLine("0 - Salvar e Sair");
        Console.Write("Escolha uma opção: ");

        return int.Parse(Console.ReadLine());
    }

    static void salvarDados(List<CarroAluguel> listaCarros, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);

        foreach (CarroAluguel c in listaCarros)
        {
            writer.WriteLine($"{c.modelo};{c.placa};{c.ano};{c.valorDiaria};{c.alugado}");
        }

        writer.Dispose();
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void carregarDados(List<CarroAluguel> listaCarros, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);

            foreach (string linha in linhasDoArquivo)
            {
                string[] campos = linha.Split(';');

                CarroAluguel c = new CarroAluguel();

                c.modelo = campos[0];
                c.placa = campos[1];
                c.ano = int.Parse(campos[2]);
                c.valorDiaria = double.Parse(campos[3]);
                c.alugado = bool.Parse(campos[4]);

                listaCarros.Add(c);
            }

            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo CSV não encontrado :(");
        }
    }

    static void addCarro(List<CarroAluguel> listaCarros)
    {
        CarroAluguel novoCarro = new CarroAluguel();

        Console.WriteLine("=== CADASTRAR NOVO CARRO ===");

        Console.Write("Modelo do veículo: ");
        novoCarro.modelo = Console.ReadLine();

        Console.Write("Placa do veículo: ");
        novoCarro.placa = Console.ReadLine();

        Console.Write("Ano de fabricação: ");
        novoCarro.ano = int.Parse(Console.ReadLine());

        Console.Write("Valor da diária (R$): ");
        novoCarro.valorDiaria = double.Parse(Console.ReadLine());

        Console.Write("Está alugado no momento? (1-Sim / 2-Não): ");
        int opAlugado = int.Parse(Console.ReadLine());

        novoCarro.alugado = (opAlugado == 1);

        listaCarros.Add(novoCarro);

        Console.WriteLine("Carro cadastrado com sucesso!");
    }

    static void mostrarCarros(List<CarroAluguel> listaCarros)
    {
        Console.WriteLine("=== FROTA COMPLETA DE VEÍCULOS ===");

        for (int i = 0; i < listaCarros.Count; i++)
        {
            string status = listaCarros[i].alugado ? "ALUGADO" : "DISPONÍVEL";

            Console.WriteLine($"[{i + 1}] {listaCarros[i].modelo} | Placa: {listaCarros[i].placa} | Ano: {listaCarros[i].ano} | Diária: R$ {listaCarros[i].valorDiaria:F2} | Status: {status}");
        }
    }

    static bool buscarPorPlaca(List<CarroAluguel> listaCarros, string placaBusca)
    {
        bool encontrada = false;

        foreach (CarroAluguel v in listaCarros)
        {
            if (v.placa.ToUpper().Contains(placaBusca.ToUpper()))
            {
                Console.WriteLine($"Modelo: {v.modelo}");
                Console.WriteLine($"Placa: {v.placa}");
                Console.WriteLine($"Ano: {v.ano}");
                Console.WriteLine($"Valor da diária: {v.valorDiaria}");
                Console.WriteLine($"Alugado: {v.alugado}");

                encontrada = true;
            }
        }

        return encontrada;
    }

  
    static void mostrarCarrosDisponiveis(List<CarroAluguel> listaCarros)
    {
        Console.WriteLine("=== CARROS DISPONÍVEIS ===");

        foreach (CarroAluguel v in listaCarros)
        {
            if (v.alugado == false)
            {
                Console.WriteLine($"Modelo: {v.modelo}");
                Console.WriteLine($"Placa: {v.placa}");
                Console.WriteLine($"Ano: {v.ano}");
                Console.WriteLine($"Valor da diária: R$ {v.valorDiaria:F2}");
            }
        }
    }

    static void mostrarCarrosAlugados(List<CarroAluguel> listaCarros)
    {
        Console.WriteLine("=== CARROS ALUGADOS ===");

        foreach (CarroAluguel v in listaCarros)
        {
            if (v.alugado == true)
            {
                Console.WriteLine($"Modelo: {v.modelo}");
                Console.WriteLine($"Placa: {v.placa}");
                Console.WriteLine($"Ano: {v.ano}");
                Console.WriteLine($"Valor da diária: R$ {v.valorDiaria:F2}");
            }
        }
    }
}

using System;
using DIO.Filmes;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static FilmeRepositorio repositorioDeFilmes = new FilmeRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						InserirSerie();
						break;
					case "2":
						ListarSeries();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "6":
						MenuFilmes();
						break;
					case "C":
						Console.Clear();
						break;
					default:
						Console.WriteLine("MSG 001 - Opção inválida.");
						break;
				}
				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("MSG 999 - Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }
        private static void InserirSerie()
		{
			Console.WriteLine("MSG 004 - Inserir nova série");

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			int entradaGenero = 0;
			string entradaTitulo = "";
			int entradaAno = 0;
			string entradaDescricao;
			ObterDados(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);

			int indiceAux = repositorio.ProximoId() +1;

			Serie novaSerie = new Serie(id: indiceAux,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
			Console.WriteLine("===================================");
			Console.WriteLine("* MSG 005 - Nova série cadastrada *");
			Console.WriteLine("===================================");			
		}
        private static void ListarSeries()
		{
			Console.WriteLine("MSG 006 - Listar séries");
			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("-------------------------------------");
				Console.WriteLine("* MSG 007 Nenhuma série cadastrada. *");
				Console.WriteLine("-------------------------------------");				
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluída*" : "Ativa"));
			}
			Console.WriteLine("===============================");
			Console.WriteLine("* MSG 008 - Consulta efetuada *");
			Console.WriteLine("===============================");	
		}
		private static void AtualizarSerie()
		{
			int indiceSerie = obterIndiceSerie();

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			Console.WriteLine("=================================");
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.WriteLine("=================================");
			int entradaGenero = 0;
			string entradaTitulo = "";
			int entradaAno = 0;
			string entradaDescricao;
			ObterDados(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);
			
			Serie atualizaSerie = new Serie(id: indiceSerie + 1,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
			Console.WriteLine("================================");
			Console.WriteLine("* MSG 009 - Alteração efetuada *");
			Console.WriteLine("================================");	
		}
        private static void ExcluirSerie()
		{
			int indiceSerie = obterIndiceSerie();
	
   			if (repositorio.RetornaPorId(indiceSerie) != null)
			{
				repositorio.Exclui(indiceSerie);
				Console.WriteLine("===========================================");
				Console.WriteLine("* MSG 011 - Status da Série {0} alterado. *", indiceSerie);
				Console.WriteLine("===========================================");	
			}
		}
		private static void VisualizarSerie()
		{
			int indiceSerie = obterIndiceSerie();
			var serie = repositorio.RetornaPorId(indiceSerie);
			if (serie != null)
			{
				Console.WriteLine(serie);
				Console.WriteLine("===============================");
				Console.WriteLine("* MSG 012 - Consulta efetuada *");
				Console.WriteLine("===============================");	
			}
		}
        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1 - Inserir nova série");
			Console.WriteLine("2 - Listar séries");
			Console.WriteLine("3 - Atualizar série");
			Console.WriteLine("4 - Excluir série");
			Console.WriteLine("5 - Visualizar série");
			Console.WriteLine("6 - Menu manutenção filmes");
			Console.WriteLine("C - Limpar Tela");
			Console.WriteLine("X - Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
		private static int obterIndiceSerie()
		{
			Console.Write("MSG 013 - Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());
			return indiceSerie -1;
		}
		private static void ObterDados(out int entradaGenero, out string entradaTitulo, out int entradaAno, out string entradaDescricao)
		{
			Console.Write("MSG 014 - Digite o gênero entre as opções acima: ");
			entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("MSG 015 - Digite o Título da série/filme       : ");
			entradaTitulo = Console.ReadLine();

			Console.Write("MSG 016 - Digite o Ano de Início da série/filme: ");
			entradaAno = int.Parse(Console.ReadLine());

			Console.Write("MSG 017 - Digite a Descrição da série/filme    : ");
			entradaDescricao = Console.ReadLine();
		}
		private static void MenuFilmes()
		{
            string opcaoFilme = ObterOpcaoFilme();

			while (opcaoFilme.ToUpper() != "X" && opcaoFilme.ToUpper() != "6")
			{
				switch (opcaoFilme)
				{
					case "1":
  						InserirFilme();
						break;
					case "2":
  						ListarFilmes();
						break;
					case "3":
						AtualizarFilme();
						break;
					case "4":
						ExcluirFilme();
						break;
					case "5":
						VisualizarFilme();
						break;
					case "6":
						Console.WriteLine("MSG 003 - Retornando para o menu principal ...");
						break;
					case "C":
						Console.Clear();
						break;
					default:
						Console.WriteLine("MSG 002 - Opção inválida.");
						break;
				}

				opcaoFilme = ObterOpcaoFilme();
			}

			Console.ReadLine();
		}	
		private static void InserirFilme()
		{
			Console.WriteLine("MSG 018 - Inserir novo filme");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			int entradaGenero = 0;
			string entradaTitulo = "";
			int entradaAno = 0;
			string entradaDescricao;
			ObterDados(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);

			int indiceAux = repositorioDeFilmes.ProximoId() + 5001;

			Filme novoFilme = new Filme(id: indiceAux,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorioDeFilmes.Insere(novoFilme);
			Console.WriteLine("===================================");
			Console.WriteLine("* MSG 019 - Novo filme cadastrado *");
			Console.WriteLine("===================================");			
		}
		private static void ListarFilmes()
		{
			Console.WriteLine("MSG 020 - Listar filmes");
			var lista = repositorioDeFilmes.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("------------------------------------");
				Console.WriteLine("* MSG 022 - Nenhum filme cadastrado.");
				Console.WriteLine("------------------------------------");				
				return;
			}

			foreach (var filme in lista)
			{
                var excluido = filme.retornaExcluido();
				Console.WriteLine("#ID {0}: - {1} {2}", filme.retornaId(), filme.retornaTitulo(), (excluido ? "*Excluído*" : "Ativo"));
			}
			Console.WriteLine("===============================");
			Console.WriteLine("* MSG 023 - Consulta efetuada *");
			Console.WriteLine("===============================");	
		}
		private static void AtualizarFilme()
		{
			int indiceFilme = obterIndiceFilme();

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			int entradaGenero = 0;
			string entradaTitulo = "";
			int entradaAno = 0;
			string entradaDescricao;
			ObterDados(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);
			
			Filme atualizaFilme = new Filme(id: indiceFilme + 5001,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorioDeFilmes.Atualiza(indiceFilme, atualizaFilme);
			Console.WriteLine("================================");
			Console.WriteLine("* MSG 024 - Alteração efetuada *");
			Console.WriteLine("================================");	

		}
		private static void ExcluirFilme()
		{
			int indiceFilme = obterIndiceFilme();

			if (repositorioDeFilmes.RetornaPorId(indiceFilme) != null)
			{
				repositorioDeFilmes.Exclui(indiceFilme);
				Console.WriteLine("===========================================");
				Console.WriteLine("* MSG 026 - Status do filme {0} alterado. *", indiceFilme);
				Console.WriteLine("===========================================");
			}
		}
        private static void VisualizarFilme()
		{
			int indiceFilme = obterIndiceFilme();
			var filme = repositorioDeFilmes.RetornaPorId(indiceFilme);

			if (filme != null)
			{
				Console.WriteLine(filme);
				Console.WriteLine("===============================");
				Console.WriteLine("* MSG 027 - Consulta efetuada *");
				Console.WriteLine("===============================");	
			}
		}
		private static string ObterOpcaoFilme()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1 - Inserir novo filme");
			Console.WriteLine("2 - Listar filmes");
			Console.WriteLine("3 - Atualizar filme");
			Console.WriteLine("4 - Excluir filme");
			Console.WriteLine("5 - Visualizar filme");
			Console.WriteLine("6 - Menu anterior");
			Console.WriteLine("C - Limpar Tela");
			Console.WriteLine("X - Sair");
			Console.WriteLine();

			string opcaoFilme = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoFilme;
		}
		private static int obterIndiceFilme()
		{
			Console.Write("MSG 021 - Digite o id do filme: ");
			int indiceFilme = int.Parse(Console.ReadLine());
			return indiceFilme -1;
		}
    }
}

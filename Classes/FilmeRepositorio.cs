using System;
using System.Collections.Generic;
using DIO.Series.Interfaces;

namespace DIO.Filmes
{
    public class FilmeRepositorio : IRepositorio<Filme>
    {
		private List<Filme> listaFilme= new List<Filme>();
		public void Atualiza(int id, Filme objeto)
		{
			listaFilme[id] = objeto;
		}

		public void Exclui(int id)
		{
			listaFilme[id].Excluir();
		}

		public void Insere(Filme objeto)
		{
			listaFilme.Add(objeto);
		}

		public List<Filme> Lista()
		{
			return listaFilme;
		}

		public int ProximoId()
		{
			return listaFilme.Count;
		}

		public Filme RetornaPorId(int id)
		{
			try
			{
				return listaFilme[id];
			}
			catch
			{
				Console.WriteLine("-----------------------------------");
				Console.WriteLine("* MSG 024 - Filme não cadastrado. *");
				Console.WriteLine("-----------------------------------");
				return null;
			}
		}
    }

}
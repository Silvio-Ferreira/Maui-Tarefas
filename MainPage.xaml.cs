using Tarefas.Constantes;
using Tarefas.Models;
using Tarefas.Servicos;

namespace maui;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		var tarefaServico = new DatabaseServico<Tarefa>(Db.DB_PATH);
		var tarefa = new Tarefa()
		{
			Titulo = "Lavar o carro",
			Descricao = "Pegar o carro na garagem e levar para lavar",
			Status = Tarefas.Enums.Status.Backlog,
			UsuarioId = UsuariosServico.Instancia().Todos()[0].Id
		};

		await tarefaServico.IncluirAsync(tarefa);

		var quantidade = await tarefaServico.QuantidadeAsync();


		count++;

		if (count == 1)
			CounterBtn.Text = $"Cliquei aqui {count} vez - E tenho {quantidade} cadastrado no SQLite";
		else
			CounterBtn.Text = $"Cliquei aqui {count} vezes - E tenho {quantidade} cadastrados no SQLite";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


using System;
using TarefasX.Controllers;
using TarefasX.Controllers.Exceptions;
using TarefasX.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TarefasX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private string Prioridade { get; set; }
        public Register()
        {
            InitializeComponent();
        }

        public void PrioridadeSelectAction(object sender, EventArgs args)
        {
            var stacks = SLPrioridades.Children;

            foreach (var linha in stacks)
            {
                Label label = ((StackLayout)linha).Children[1] as Label;
                label.TextColor = Color.Gray;
            }

            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource source = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;
            string prioridade = source.ToString().Replace("File: Resources/", "").Replace(".png", "");
            Prioridade = prioridade;
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            try
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Nome = txtNome.Text;
                tarefa.Prioridade = Prioridade;

                new TarefaController().Salvar(tarefa);

                App.Current.MainPage = new NavigationPage(new Main());
            }
            catch (TarefasException e)
            {
                DisplayAlert("Erro", e.Message, "OK");
            }
        }
    }
}
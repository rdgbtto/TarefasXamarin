using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasX.Controllers;
using TarefasX.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TarefasX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentPage
    {
        public Main()
        {
            InitializeComponent();

            Today.Text = DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.ToString("dd/MM");
            CarregarTarefas();
        }

        public void AdicionarTarefa(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Register());
        }

        private void CarregarTarefas()
        {
            SLTarefas.Children.Clear();
            List<Tarefa> lista = new TarefaController().ListarTarefas();
            foreach (Tarefa tarefa in lista)
            {
                ListaStackLayout(tarefa);
            }
        }

        public void ListaStackLayout(Tarefa tarefa)
        {
            Image check = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("nochecked.png"), HeightRequest = 20, WidthRequest = 20 };
            if (Device.RuntimePlatform == Device.UWP)
                check.Source = ImageSource.FromFile("Resources/nochecked.png");

            View stackTarefa;
            if (tarefa.DataFinalizacao == null)
            {
                stackTarefa = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Text = tarefa.Nome };
            }
            else
            {
                stackTarefa = new StackLayout() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Spacing = 0};
                ((StackLayout)stackTarefa).Children.Add(new Label() { Text = tarefa.Nome, TextColor = Color.Gray });
                ((StackLayout)stackTarefa).Children.Add(new Label() { Text = "Finalizado em " + tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy - hh:mm") + "hrs", TextColor = Color.Gray, FontSize = 10 });
            }
            

            Image prioridade = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile(tarefa.Prioridade + ".png"), HeightRequest = 20, WidthRequest = 20 };
            if (Device.RuntimePlatform == Device.UWP)
                prioridade.Source = ImageSource.FromFile("Resources/" + tarefa.Prioridade + ".png");

            Image lixeira = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("lixeira.png"), HeightRequest = 50, WidthRequest = 50 };
            if (Device.RuntimePlatform == Device.UWP)
                lixeira.Source = ImageSource.FromFile("Resources/lixeira.png");

            StackLayout linha = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15
            };

            linha.Children.Add(check);
            linha.Children.Add(stackTarefa);
            linha.Children.Add(prioridade);
            linha.Children.Add(lixeira);

            SLTarefas.Children.Add(linha);
        }
    }
}
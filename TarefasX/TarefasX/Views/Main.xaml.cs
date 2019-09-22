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
            int i = 0;
            foreach (Tarefa tarefa in lista)
            {
                ListaStackLayout(tarefa, i);
                i++;
            }
        }

        public void ListaStackLayout(Tarefa tarefa, int index)
        {
            Image checkImage = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("nochecked.png"), HeightRequest = 20, WidthRequest = 20 };
            if (Device.RuntimePlatform == Device.UWP)
                checkImage.Source = ImageSource.FromFile("Resources/nochecked.png");
            if (tarefa.DataFinalizacao != null)
            {
                checkImage.Source = ImageSource.FromFile("checked.png");
                checkImage.IsEnabled = false;
                if (Device.RuntimePlatform == Device.UWP)
                    checkImage.Source = ImageSource.FromFile("Resources/checked.png");
            }   
            TapGestureRecognizer checkTap = new TapGestureRecognizer();
            checkTap.Tapped += delegate
            {
                new TarefaController().Finalizar(index, tarefa);
                CarregarTarefas();
            };
            checkImage.GestureRecognizers.Add(checkTap); //checkImage

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
            } //stackTarefa
            

            Image prioridadeImage = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile(tarefa.Prioridade + ".png"), HeightRequest = 20, WidthRequest = 20 };
            if (Device.RuntimePlatform == Device.UWP)
                prioridadeImage.Source = ImageSource.FromFile("Resources/" + tarefa.Prioridade + ".png"); //prioridadeImage

            Image lixeiraImage = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("lixeira.png"), HeightRequest = 50, WidthRequest = 50 };
            if (Device.RuntimePlatform == Device.UWP)
                lixeiraImage.Source = ImageSource.FromFile("Resources/lixeira.png");
            TapGestureRecognizer deleteTap = new TapGestureRecognizer();
            deleteTap.Tapped += delegate
            {
                new TarefaController().Deletar(index);
                CarregarTarefas();
            };
            lixeiraImage.GestureRecognizers.Add(deleteTap); //lixeiraImage

            StackLayout linha = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15
            };

            linha.Children.Add(checkImage);
            linha.Children.Add(stackTarefa);
            linha.Children.Add(prioridadeImage);
            linha.Children.Add(lixeiraImage);

            SLTarefas.Children.Add(linha);
        }

        private void DeleteTap_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
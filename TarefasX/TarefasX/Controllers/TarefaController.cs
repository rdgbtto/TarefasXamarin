using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TarefasX.Models;

namespace TarefasX.Controllers
{
    public class TarefaController
    {
        private List<Tarefa> Lista { get; set; }

        public void Salvar(Tarefa tarefa)
        {
            if (tarefa.Nome == null || tarefa.Nome.Length <= 0)
            {
                throw new Exceptions.TarefasException("Tarefa sem nome");
            }
            if (tarefa.Prioridade.Length <= 0)
            {
                throw new Exceptions.TarefasException("Tarefa sem prioridade");
            }
            Lista = ListarTarefas();
            Lista.Add(tarefa);
            AtualizarLista(Lista);
        }

        public void Finalizar(int index, Tarefa tarefa)
        {
            Lista = ListarTarefas();
            Lista.RemoveAt(index);
            tarefa.DataFinalizacao = DateTime.Now;
            Lista.Add(tarefa);
            AtualizarLista(Lista);
        }

        public void Deletar(int index)
        {
            Lista = ListarTarefas();
            Lista.RemoveAt(index);
            AtualizarLista(Lista);
        }

        public List<Tarefa> ListarTarefas()
        {
            return BuscarLista();
        }

        private void AtualizarLista(List<Tarefa> lista)
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                App.Current.Properties.Remove("Tarefas");
            }

            string jsonValue = JsonConvert.SerializeObject(lista);
            App.Current.Properties.Add("Tarefas", jsonValue);
        }

        private List<Tarefa> BuscarLista()
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                string jsonValue = (string)App.Current.Properties["Tarefas"];

                List<Tarefa> lista = JsonConvert.DeserializeObject<List<Tarefa>>(jsonValue);
                return lista;
            }

            return new List<Tarefa>();
        }
    }
}

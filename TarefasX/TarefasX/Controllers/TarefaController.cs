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
            if (tarefa.Prioridade <= 0)
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
            Lista.Add(tarefa);
            AtualizarLista(Lista);
        }

        public void Deletar(Tarefa tarefa)
        {
            Lista = ListarTarefas();
            Lista.Remove(tarefa);
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
            App.Current.Properties.Add("Tarefas", lista);
        }

        private List<Tarefa> BuscarLista()
        {
            return App.Current.Properties.ContainsKey("Tarefas") ? (List<Tarefa>)App.Current.Properties["Tarefas"] : new List<Tarefa>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WPFToDo.Services;

namespace WPFToDo.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ToDoViewModel> items;

        public ObservableCollection<ToDoViewModel> Items
        {
            get
            {
                return items ??= LoadItems();
            }
        }

        public MainViewModel()
        {
        }

        private ObservableCollection<ToDoViewModel> LoadItems()
        {
            return new ObservableCollection<ToDoViewModel>(ToDoRepository.Instance.GetAll().Select(x => new ToDoViewModel(x)));
            //Repository repos = Repository.Instance;
            //return new ObservableCollection<ToDoViewModel>(repos.Get().Select(td => new ToDoViewModel(td)));
        }
    }
}

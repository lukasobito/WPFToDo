using System;
using System.Collections.Generic;
using System.Text;
using WPFToDo.Model;

namespace WPFToDo.ViewModels
{
    class ToDoViewModel : ViewModelBase
    {
        private ToDo entity;
        private string titre;
        private string description;
        private bool isDone;
        public bool IsDone
        {
            get { return isDone; }
            set
            {
                if (isDone != value)
                {
                    isDone = value;
                    RaisePropertyChanged(nameof(IsDone));
                }
            }
        }

        public string Titre
        {
            get
            {
                return titre;
            }

            set
            {
                if (titre != value)
                {
                    titre = value;
                    RaisePropertyChanged(nameof(Titre));
                }
            }
        }
        public string Description
        {
            get { return description; }

            set
            {
                if (description != value)
                {
                    description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }
        
        public ToDoViewModel(ToDo entity)
        {
            this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
            Titre = entity.Titre;
            Description = entity.Description;
        }
    }
}

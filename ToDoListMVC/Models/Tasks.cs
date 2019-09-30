using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListMVC.Models
{
    public class Tasks
    {
        public Tasks()
        {

        }
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime LimeDate { get; set; }

        public DateTime Reminder { get; set; }

        public string Note { get; set; }

    }
}

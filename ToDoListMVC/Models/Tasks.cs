using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListMVC.Models
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha a descrição!")]
        public string Description { get; set; }
    }
}
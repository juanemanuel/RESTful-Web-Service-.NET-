using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTfulWebApi.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de Inicio")]
        public DateTime? StartingDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de Fim")]
        public DateTime? EndingDate { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [DisplayName("Criador")]
        public string Creator { get; set; }
        public bool IsCreation { get; set; }
    }
}
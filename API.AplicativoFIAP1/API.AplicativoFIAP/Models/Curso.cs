using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.AplicativoFIAP.Models
{
    public class Curso
    {
        public Curso()
        {
            Disciplinas = new List<Disciplina>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
    }

    public class Disciplina
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Carga { get; set; }
        public Professor Professor { get; set; }
    }    
}
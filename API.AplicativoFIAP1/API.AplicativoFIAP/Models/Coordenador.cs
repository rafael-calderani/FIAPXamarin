using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.AplicativoFIAP.Models
{
    public class Coordenador
    {
        public Coordenador()
        {
            Cursos = new List<Curso>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}
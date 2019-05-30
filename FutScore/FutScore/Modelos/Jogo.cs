using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore
{
    public class Jogo
    {

        public int id { get; set; }

        public Equipe mandante { get; set; }

        public Equipe visitante { get; set; }

        public DateTime data { get; set; }
    }

}
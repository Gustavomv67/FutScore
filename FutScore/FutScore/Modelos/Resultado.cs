using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore
{
    public class Resultado
    {

        public int id { get; set; }

        public Jogo jogo { get; set; }

        public int gols1 { get; set; }

        public int gols2 { get; set; }

        public Equipe time { get; set; }

        public int posse1 { get; set; }

        public int posse2 { get; set; }


    }

}
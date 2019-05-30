using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore
{
    public class Escanteio
    {
        public int id { get; set; }

        public Jogo jogo { get; set; }

        public Equipe time { get; set; }

    }

}
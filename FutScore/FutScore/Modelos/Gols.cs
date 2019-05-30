using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore
{
    public class Gols
    {

        public int id { get; set; }

        public Jogo jogo { get; set; }

        public Jogador jogador { get; set; }

        public Equipe time { get; set; }

        public bool falta { get; set; }

        public bool penalti { get; set; }

        public bool escanteio { get; set; }

        public Gols()
        {
            falta = false;
            penalti = false;
            escanteio = false;
        }

    }

}
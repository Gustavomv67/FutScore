using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore
{
    public class Jogador
    {
        public int id { get; set; }

        public string nome { get; set; }

        public Equipe time { get; set; }

        public int idade { get; set; }

        public int numero { get; set; }

        public Posicoes posicao { get; set; }
    }

}
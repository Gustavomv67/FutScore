using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore
{
    public class Subtituicao
    {

        public int id { get; set; }

        public Jogo jogo { get; set; }

        public Jogador jogadorSaiu { get; set; }

        public Jogador jogadorEntrou { get; set; }

        public Equipe time { get; set; }

    }

}
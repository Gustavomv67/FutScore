using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore
{
    public class Cartao
    {
        public int id { get; set; }

        public Jogo jogo { get; set; }

        public Jogador jogador { get; set; }

        public CartaoTipo cartao { get; set; }

    }

}
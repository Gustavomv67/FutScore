using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FutScore
{
    public class Escalacao
    {
        public int id { get; set; }

        public Jogo jogo { get; set; }

        public Equipe time { get; set; }

        public Jogador jogador { get; set; }

        public Posicoes posicao { get; set; }


    }
}

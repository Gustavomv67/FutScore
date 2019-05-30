using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore
{
    public class Equipe
    {
        public int id { get; set; }

        public string nome { get; set; }

        public string pais { get; set; }

        public string estado { get; set; }

        public string cidade { get; set; }

        public string sigla { get; set; }


    }

}
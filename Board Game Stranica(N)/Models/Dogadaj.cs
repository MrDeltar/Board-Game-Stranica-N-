using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Board_Game_Stranica_N_.Models
{
    //koju bazu koristi
    [Table("dogadaji")]
    public class Dogadaj
    {
        // id dogadaja
        [Display(Name = "ID drustvene igre")]
        [Key]
        public int Id { get; set; }

        // naziv dogadaja
        private string naziv;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ime igre je obavezno")]
        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }
        //mjesto odrzavanja
        [Column("mjesto")]
        [Display(Name = "Mjesto održavanja")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Mjesto { get; set; }

        // Datum odrzavanja
        [Column("datum")]
        [Display(Name = "Datum održavanja")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} je obavezan")]
        public DateTime DatumOdrzavanja { get; set; }

        //organizator
        [Column("organizator")]
        [Display(Name = "Organizator")]
        [Required(ErrorMessage = "Ime i prezime organizatora su obavezni")]
        public string Organizator { get; set; }

        //organizator
        [Column("kratki_opis")]
        [Display(Name = "Detaljniji opis")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string KratkiOpis { get; set; }


    }
}
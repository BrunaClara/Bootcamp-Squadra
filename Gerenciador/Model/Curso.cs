using Gerenciador.Data.Responses;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gerenciador.Model
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter de 3 a 60 caracteres.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter de 3 a 60 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "A duração deve ser maior que zero.")]
        public double DuracaoDias { get; set; }

        [MaxLength(1024, ErrorMessage = "Este campo deve conter no máximo 1024 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public DateTime IniciadoEm { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public DateTime FinalizadoEm { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public StatusTipo Status { get; set; }

        public enum StatusTipo
        {
            Previsto,
            Em_Andamento,
            Finalizado
        }

        public CursoResponse AsResponse() => new()
        {
            Id=Id,
            Titulo=Titulo,
            DuracaoDias=DuracaoDias,
            Descricao=Descricao,
            IniciadoEm=IniciadoEm,
            FinalizadoEm=FinalizadoEm,
            Status = Enum.GetName(typeof(StatusTipo), Status)
        };
    }
 
}

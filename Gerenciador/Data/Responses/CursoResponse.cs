using System;

namespace Gerenciador.Data.Responses
{
    public class CursoResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public double DuracaoDias { get; set; }
        public string Descricao { get; set; }
        public DateTime IniciadoEm { get; set; }
        public DateTime FinalizadoEm { get; set; }
        public string Status { get; set; }
    }
}

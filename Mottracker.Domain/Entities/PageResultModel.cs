namespace Mottracker.Domain.Entities
{
    public class PageResultModel<T>
    {
        public T Data { get; set; }

        public int Deslocamento { get; set; }
        public int RegistrosRetornado { get; set; }
        public int TotalRegistros { get; set; }
    }
}
namespace JocsDeGuerra.Models
{
    public class DBResponseDTO<T>
    {
        public string Id { get; set; }
        public T data { get; set; }
    }
}

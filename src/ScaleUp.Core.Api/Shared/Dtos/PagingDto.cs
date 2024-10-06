namespace ScaleUp.Core.Api.Shared.Dtos
{
    public sealed record PagingDto
    {
        public int From { get; set; }
        public int Size { get; set; }
        public object Sort { get; set; }
    }
}

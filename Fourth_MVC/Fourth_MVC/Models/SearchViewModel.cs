namespace Fourth_MVC.Models
{
    public class SearchViewModel
    {
        public string? Query { get; set; }

        public List<Aqarat>? Aqarats { get; set; }
        public List<Land>? Lands { get; set; }
        public List<Complex>? Complexes { get; set; }
        public List<Field>? Fields { get; set; }
    }
}

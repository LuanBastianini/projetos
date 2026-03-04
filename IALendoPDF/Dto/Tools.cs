namespace IALendoPDF;

public class Tools
{
    public required string type { get; set; }
    public required string[] vector_store_ids { get; set; }
    public int max_num_results { get; set; }
}

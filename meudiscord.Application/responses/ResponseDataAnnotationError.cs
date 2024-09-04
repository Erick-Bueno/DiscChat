public class ResponseDataAnnotationError
{
    public ResponseDataAnnotationError(int status, Dictionary<string, List<string>> errors)
    {
        this.status = status;
        this.errors = errors;
    }

    public int status { get; set; }
    public Dictionary<string, List<string>> errors {get; set;}
    
}
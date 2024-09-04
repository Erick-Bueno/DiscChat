public abstract class Response {
    public int status { get; set; }
    public string message { get; set; }
    protected Response (int status, string message) 
    {
        this.status = status;
        this.message = message;
    }   
    
}
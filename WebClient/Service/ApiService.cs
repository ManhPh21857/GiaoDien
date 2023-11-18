namespace WebClient.Service; 

public class ApiService {
    public void asd(HttpContext context) {
        var d = context.Session;
        d.GetString("");
    }
}
namespace Inferno.src.Adapters.Inbound.Controllers.Model
{
    public class APIResponse<Tdata>
    {
        public bool Status { get; set; }
        public Tdata? Data { get; set; }
        public string Message { get; set; }

        public APIResponse() { }

        public APIResponse(string message)
        {
            Status = true;
            Message = message;
        }

        public APIResponse(Tdata data, string message)
        {
            Status = true;
            Data = data;
            Message = message;
        }
    }
}

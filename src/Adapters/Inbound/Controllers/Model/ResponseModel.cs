namespace Inferno.src.Adapters.Inbound.Controllers.Model
{
    public class ResponseModel<Tdata>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Tdata Data { get; set; }

        public ResponseModel()
        {
            Status = true;
        }
    }
}

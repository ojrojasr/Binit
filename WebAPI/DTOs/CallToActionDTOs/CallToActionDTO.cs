namespace WebAPI.DTOs.CallToActionDTOs
{
    public class CallToActionDTO<T> where T : class
    {
        public string Route { get; set; }
        public T Params { get; set; }

        public CallToActionDTO(string route)
        {
            this.Route = route;
            this.Params = null;
        }

        public CallToActionDTO(string route, T parameters)
        {
            this.Route = route;
            this.Params = parameters;
        }
    }
}
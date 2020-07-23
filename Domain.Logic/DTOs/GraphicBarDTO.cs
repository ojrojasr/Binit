using Domain.Entities.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Logic.DTOs
{
    public class GraphicBarDTO
    {

        public string title { get; set; }
        public string subtitle { get; set; }
        public List<string> categories { get; set; }
        public List<double> series { get; set; }
        public List<string> colors { get; set; }
    }
}
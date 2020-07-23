using Domain.Entities.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Logic.DTOs
{
    public class GraphicPieDTO
    {
        public string title { get; set; }
        public List<PieDTO> series { get; set; }
        public List<string> colors { get; set; }
    }
}
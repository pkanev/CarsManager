using System.Collections.Generic;
using Client.Core.Models;

namespace Client.Core.Dtos
{
    public class GetModelsDto
    {
        public IList<ModelModel> Models { get; set; } = new List<ModelModel>();
    }
}

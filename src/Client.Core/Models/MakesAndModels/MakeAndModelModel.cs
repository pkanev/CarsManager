using System.Collections.Generic;

namespace Client.Core.Models.MakesAndModels
{
    public class MakeAndModelModel : MakeModel
    {
        public IList<ModelModel> Models { get; set; } = new List<ModelModel>();
    }
}

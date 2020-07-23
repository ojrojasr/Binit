using Binit.Framework.Interfaces.DAL;

namespace Domain.Entities.Model.Views
{
    public class ProductFeaturesView : IDbView
    {
        public const string Name = "View_ProductFeaturesCounts";
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int FeatureCount { get; set; }
    }
}

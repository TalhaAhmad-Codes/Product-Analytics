using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.Enums;
using ProductAnalytics.Utils;

namespace ProductAnalytics.DTOs.ProductDTOs
{
    public sealed class ProductUpdateDto : BaseDto
    {
        private string name;

        public byte[] Image { get; init; }
        public string Name
        {
            get => name;
            init => name = Misc.Simplify(value);
        }
        public decimal Price { get; init; }
        public StockStatus StockStatus { get; init; }
    }
}

using ShoppingApi.Models.Curbside;
using System.Threading.Tasks;

namespace ShoppingApi
{
    public interface IProcessCurbsideOrders
    {
        Task<GetCurbsideDetailsResponse> PlaceOrderAsync(PostCurbsideRequest request);
        Task<GetCurbsideDetailsResponse> GetByIdAsync(int id);
    }
}
using OfferService.ApiReference.ResponseModels;
using Refit;
namespace OfferService.ApiReference;

public interface IUsersApiReference
{
    [Get("/users/{userId}")]
    Task<UserResponse> GetUserById([AliasAs("userId")] Guid userId);
}
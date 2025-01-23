namespace OfferService.ApiReference.ResponseModels;

public sealed class UserResponse
{
    public Guid Id {get; set;}
    public string Email { get; set; }
    public string Name { get; set; }
    public bool IsAdmin { get; set; }

    public UserResponse()
    {
        
    }
}
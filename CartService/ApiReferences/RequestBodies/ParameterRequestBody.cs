namespace CartService.ApiReferences.RequestBodies;

public record ParameterRequestBody
{
    public IEnumerable<long> OfferIds;
    
    public ParameterRequestBody(IEnumerable<long> offerIds)
    {
        OfferIds = offerIds;
    }
}
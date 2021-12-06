using System.Threading.Tasks;
using CarSharing_Database_GraphQL.Mutations.Records.ListingRecords;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Mutations
{
    public partial class Mutation
    {
        public async Task<Listing> AddListing([Service] IListingRepo listingRepo, AddListingInput input)
        {
            var listing = new Listing
            {
                ListedDate = input.ListedDate,
                Price = input.Price,
                Location = input.Location,
                DateFrom = input.DateFrom,
                DateTo = input.DateTo,
                Vehicle = new Vehicle
                {
                    LicenseNo = input.Vehicle.LicenseNo
                }
            };
            
            return await listingRepo.AddAsync(listing);

        }
        
        public async Task<Listing> UpdateListing([Service] IListingRepo listingRepo, UpdateListingInput input)
        {
            var listing = new Listing
            {
                Id = input.Id,
                Price = input.Price,
                Location = input.Location,
                DateFrom = input.DateFrom,
                DateTo = input.DateTo,
                Vehicle = new Vehicle
                {
                    LicenseNo = input.Vehicle.LicenseNo
                }
            };
            return await listingRepo.UpdateAsync(listing);
        }
        
        public async Task<bool> RemoveListing([Service] IListingRepo listingRepo, int id)
        {
            return await listingRepo.RemoveAsync(id);
        }
    }
}
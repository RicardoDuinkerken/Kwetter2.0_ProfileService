using ProfileService.Dal.Models;

namespace ProfileService.Core.Services.Interfaces;

public interface IProfileService
{
    Task<Profile> CreateProfileAsync(Profile profile);
    Task<Profile> UpdateProfileAsync(Profile profile);
    Task<long> DeleteProfileAsync(long profileId);
}
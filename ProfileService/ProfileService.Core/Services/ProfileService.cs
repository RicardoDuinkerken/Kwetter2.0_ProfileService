using GenericDal;
using Microsoft.Extensions.Logging;
using ProfileService.Core.Services.Interfaces;
using ProfileService.Dal.Models;

namespace ProfileService.Core.Services;

public class ProfileService : IProfileService
{
    private ILogger<ProfileService> _logger;
    private readonly IAsyncRepository<Profile, long> _profileRepository;

    public ProfileService(ILogger<ProfileService> logger, IAsyncRepository<Profile, long> profileRepository)
    {
        _logger = logger;
        _profileRepository = profileRepository;
    }
    
    public async Task<Profile> CreateProfileAsync(Profile profile)
    {
        profile = await _profileRepository.CreateAsync(profile);

        _logger.LogInformation("Profile created with id: {Id}", profile.Id);
        
        return profile;
    }
    
    public async Task<Profile> UpdateProfileAsync(Profile profile)
    {
        profile = await _profileRepository.UpdateAsync(profile);
        _logger.LogInformation("Profile updated with id: {Id}", profile.Id);
        
        return profile;
    }

    public async Task<long> DeleteProfileAsync(long profileId)
    {
        if (!await _profileRepository.DeleteAsync(profileId))
            throw new ArgumentException($"No profile found with id: {profileId}");
        
        return profileId;
    }
}
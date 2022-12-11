using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProfileService.Api.Mappers;
using ProfileService.Core.Services.Interfaces;
using ProfileService.Grpc;

namespace ProfileService.Api.Controllers;

public class ProfileController : Grpc.ProfileService.ProfileServiceBase
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IProfileService _profileService;

    public ProfileController(ILogger<ProfileController> logger, IProfileService profileService)
    {
        _logger = logger;
        _profileService = profileService;
    }
    
    public override async Task<ProfileResponse> CreateProfile(CreateProfileRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("CreateProfile invoked");
        
        try
        {
            return ProfileMapper.ProfileToProfileResponse(
                await _profileService.CreateProfileAsync(
                    ProfileMapper.CreateProfileRequestToProfile(request)));
        }
        catch (Exception e)
        {
            _logger.LogError("{E}", e);
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
    
    public override async Task<ProfileResponse> UpdateProfile(UpdateProfileRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("UpdateProfile invoked");
        
        try
        {
            return ProfileMapper.ProfileToProfileResponse(
                await _profileService.UpdateProfileAsync(
                    ProfileMapper.UpdateProfileRequestToProfile(request)));
        }
        catch (Exception e)
        {
            _logger.LogError("{E}", e);
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
    
    public override async Task<DeleteProfileResponse> DeleteProfile(DeleteProfileRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("DeleteAccount invoked");
        
        try
        {
            return ProfileMapper.ProfileToDeleteProfileResponse(
                await _profileService.DeleteProfileAsync(
                    ProfileMapper.DeleteProfileRequestToId(request)));
        }
        catch (Exception e)
        {
            _logger.LogError("{E}", e);
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
    
}
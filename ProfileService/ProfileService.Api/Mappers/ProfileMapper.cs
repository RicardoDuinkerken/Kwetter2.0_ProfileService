using ProfileService.Dal.Models;
using ProfileService.Grpc;

namespace ProfileService.Api.Mappers;

public class ProfileMapper
{
    public static Profile CreateProfileRequestToProfile(CreateProfileRequest request)
    {
        return new()
        {
            Username = request.Username
        };
    }
    
    public static Profile UpdateProfileRequestToProfile(UpdateProfileRequest request)
    {
        return new()
        {
           Id = request.Id,
           Username = request.Username,
           Age = request.Age,
           Bio = request.Bio,
           Location = request.Location
        };
    }

    public static ProfileResponse ProfileToProfileResponse(Profile profile)
    {
        return new()
        {
            Id = profile.Id,
            Username = profile.Username,
            Age = profile.Age,
            Bio = profile.Bio,
            Location = profile.Location
        };
    }

    public static long DeleteProfileRequestToId(DeleteProfileRequest request)
    {
        return request.Id;
    }

    public static DeleteProfileResponse ProfileToDeleteProfileResponse(long profileId)
    {
        return new()
        {
            Id = profileId
        };
    }
}
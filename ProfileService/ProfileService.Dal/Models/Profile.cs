namespace ProfileService.Dal.Models;

public class Profile
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Location { get; set; }
    public string Age{ get; set; }
    public string WebLink { get; set; }
    public string Bio { get; set; }
}
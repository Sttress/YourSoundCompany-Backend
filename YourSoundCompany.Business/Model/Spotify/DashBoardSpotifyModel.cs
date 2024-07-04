
namespace YourSoundCompany.Business.Model.Spotify
{
    public class DashBoardSpotifyModel
    {
        public List<Gender> Gender { get; set; }
    }

    public class Gender
    {
        public string Name { get; set; }
        public List<Aritsts>? Artists { get; set; }
    }

    public class Aritsts
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? UrlSpotify {  get; set; }
        public long  Followers { get; set; }
    }
}

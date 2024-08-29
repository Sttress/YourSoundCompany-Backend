
namespace YourSoundCompany.Business.Model.User.DTO
{
    public class UserDashBoardDTO
    {
        List<Itens> Itens { get; set; }
    }

    public class Itens
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Generos { get; set; }
    }
}

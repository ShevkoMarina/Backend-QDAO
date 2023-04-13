namespace QDAO.Domain
{
    public record User(int Id, string Login, string Password, string Address, Roles Role);
}

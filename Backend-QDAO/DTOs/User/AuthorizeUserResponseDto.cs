namespace QDAO.Endpoint.DTOs.User
{
    public class AuthorizeUserResponseDto
    {
        public long UserId { get; set; }
        public short Role { get; set; }

        public string Account { get; set; }
    }
}

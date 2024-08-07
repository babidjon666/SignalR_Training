namespace backend.DTOModel.Login.DTO
{
    public class LoginResult
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserSurname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public Result? Result { get; set; }
    }
}
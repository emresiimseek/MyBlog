namespace MyBlog.EntityFramework.Messages
{
    public enum MessagesCodes
    {
        UsernameandPasswordNotMatch = 100,
        UserNameIsAlreadyExist = 101,
        UserIsNotActive = 102,
        EmailAlreadyExist = 103,
        ErrorRegister = 105,
        UserAlreadyActive = 106,
        ActivateIdDoesNotExist = 107,
        Success = 200,
        UnexpectedError = 201
    }
}

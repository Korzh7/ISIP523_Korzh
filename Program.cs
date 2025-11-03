
using ISIP523_Korzh;
public static class PasswordHasher
{
    public static string HashPasswordBCrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPasswordBCrypt(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
class Program
{
    static void Main(string[] args)
    {

    }

    
    static void Registration()
    {
        using (var context = new Pr8GordovMainContext())
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Введите никнейм: ");
                var nickName = Console.ReadLine();
                Console.WriteLine("Введите логин: ");
                var login = Console.ReadLine();
                Console.WriteLine("Введите пароль: ");
                var password = Console.ReadLine();
                Console.WriteLine("Введите пароль повторно");
                var password_ = Console.ReadLine();
                if (password == password_)
                {
                    Console.WriteLine("Вы успешно зарегестрировались!");
                    var hashedPassword = PasswordHasher.HashPasswordBCrypt(password);
                    var user = new User
                    {
                        Id = 1,
                        Name = nickName,
                        Login = login,
                        PasswordHash = hashedPassword
                    };
                    context.SaveChanges();
                    break;

                }
                else Console.WriteLine("Пароли не совпадают, попробуйте еще раз.");
            }
            Console.WriteLine("Вы превысили лимит неудачных попыток. Попробуйте позже.");
            
        }
    }
}


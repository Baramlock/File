using System.Text;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        //Выведите платёжные ссылки для трёх разных систем платежа: 
        //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
        //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
        //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}

        var system1 = new PaymentSystem("pay.system1.ru/order?amount=12000RUB&hash=", new HashOrderId(),new HashMD5());
        Console.WriteLine(system1.GetPayingLink(new Order(5,2000)));
        var system2 = new PaymentSystem("order.system2.ru/pay?hash=", new HashOrderIDAmound(), new HashMD5());
        Console.WriteLine(system2.GetPayingLink(new Order(5, 2000)));
        var system3 = new PaymentSystem("system3.com/pay?amount=12000&curency=RUB&hash=", new HashOrderIdAmoundSecretKey("asdmafknds345345jfnhfghfs"), new HashMD5());
        Console.WriteLine(system3.GetPayingLink(new Order(5, 2000)));
    }
}

public class Order
{
    public readonly int Id;
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
}

public interface IPaymentSystem
{
    public string GetPayingLink(Order order);
}

public interface IHashLogick
{
    public string GetStringToHash(Order order);
}

public interface IHash
{
    public string GetHash(string line);
}

public class PaymentSystem : IPaymentSystem
{
    public readonly string StartLink;
    private readonly IHashLogick _hashLogick;
    private readonly IHash _hash;

    public PaymentSystem(string startLink, IHashLogick hashLogick,IHash hash )
    {
        StartLink = startLink;
        _hashLogick = hashLogick;
        _hash = hash;
    }

    public string GetPayingLink(Order order) => StartLink + _hash.GetHash(_hashLogick.GetStringToHash(order));
}

class HashOrderId : IHashLogick
{
    public string GetStringToHash(Order order) => order.Id.ToString();
}

class HashOrderIDAmound : IHashLogick
{
    public string GetStringToHash(Order order)=> (order.Id + order.Amount).ToString();
}

class HashOrderIdAmoundSecretKey : IHashLogick
{
    private readonly string _seacretKey;

    public HashOrderIdAmoundSecretKey(string secretKey)
    {
        _seacretKey = secretKey;
    }

    public string GetStringToHash(Order order) => ((order.Id + order.Amount).ToString() + _seacretKey);
}

public class HashMD5 : IHash
{
    public string GetHash(string input)
    {
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hash);
    }
}

public class HashSHA1 : IHash
{
    public string GetHash(string input)
    {
            var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
    }
}

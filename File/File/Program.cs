namespace IMJunior
{
    internal class Program
    {
        private static void Main()
        {
            var paymentHandler = new PaymentHandler();
            var orderForm = new OrderForm(new List<PaymentSytem>()
            { new PaymentSytem("QIWI", new TransitWebPage(), "Перевод на страницу QIWI...",new UsualVerification(), "Проверка платежа через QIWI..."),
            new PaymentSytem("Webmoney", new TransitAPI(), "Вызов API WebMoney...",new UsualVerification(), "Проверка платежа через WebMoney..."),
            new PaymentSytem("Card", new TransitAPI(), "Вызов API банка эмитера карты Card...",new UsualVerification(), "Проверка платежа через Card...")});

            var payment = orderForm.ShowForm();
            payment.Transit();
            paymentHandler.ShowPaymentResult(payment);
        }
    }

    public class OrderForm
    {
        private readonly List<PaymentSytem> _payments;

        public OrderForm(List<PaymentSytem> payments)
        {
            _payments = payments;
        }

        public PaymentSytem ShowForm()
        {
            Console.WriteLine("Мы принимаем:");
            foreach (var payment in _payments)
            {
                Console.WriteLine(" " + payment.SystemId);
            }
            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            while (true)
            {
                var systemId = Console.ReadLine();
                foreach (var payment in _payments)
                {
                    if (payment.SystemId == systemId)
                    {
                        return payment;
                    }
                }
                Console.WriteLine("Имя банка введено не корректно попробуйте снова");
            }
        }

    }
    public class PaymentSytem
    {
        public string SystemId { get; private set; }
        private readonly ITransit _transit;
        private readonly string _transitNotice;
        private readonly IPaymentVerification _paymentVerification;
        private readonly string _chectPaymantNotice;

        public PaymentSytem(string systemId, ITransit transit, string transitNotice, IPaymentVerification paymentVerification, string chectPaymantNotice)
        {
            SystemId = systemId;
            _transit = transit;
            _transitNotice = transitNotice;
            _paymentVerification = paymentVerification;
            _chectPaymantNotice = chectPaymantNotice;
        }

        public void Transit()
        {
            _transit.Transit();
            Console.WriteLine(_transitNotice);
        }
        public void ChectPayment()
        {
            _paymentVerification.Verification();
            Console.WriteLine(_chectPaymantNotice);
        }
    }

    public interface ITransit
    {
        void Transit();
    }

    public class TransitWebPage : ITransit
    {
        public void Transit()
        {
            //Перевод на страницу
        }
    }

    public class TransitAPI : ITransit
    {
        public void Transit()
        {
            //Вызов Api
        }
    }
    public interface IPaymentVerification
    {
        void Verification();
    }

    public class UsualVerification : IPaymentVerification
    {
        public void Verification()
        {
            //Проверка платежа 
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(PaymentSytem paymentSystem)
        {
            Console.WriteLine($"Вы оплатили с помощью {paymentSystem.SystemId}");
            paymentSystem.ChectPayment();
            Console.WriteLine("Оплата прошла успешно!");
        }
    }
}
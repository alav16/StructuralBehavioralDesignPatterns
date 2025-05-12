/*
You are building a payment system that supports:
Different types of payments:
BasicPayment
SubscriptionPayment
Different payment methods (gateways):
Stripe
PayPal
CryptoWallet
You must use the Bridge Design Pattern to separate payment types from payment processors.
✅ Your code should allow adding new Payment types or new Processors without modifying existing classes.
 ✅ Respect Open/Closed Principle (OCP).

🛠 Tasks:
Create an interface IPaymentProcessor with a method to process a payment.
Create three concrete processors:
StripeProcessor
PayPalProcessor
CryptoWalletProcessor
Create an abstract class Payment.
Create two concrete payments:
BasicPayment
SubscriptionPayment
Payments should use the processor via constructor injection.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prob2Bridge
{

    public interface IPaymentProcessor
    {
        void PaymentMethod(double amount);
    }

    public class StripeProcessor : IPaymentProcessor
    {
        public void PaymentMethod(double amount)
        {
            Console.WriteLine($"Strip {amount}");
        }

    }


    public class PayPalProcessor : IPaymentProcessor
    {
        public void PaymentMethod(double amount)
        {
            Console.WriteLine($"PayPal {amount}");
        }
    }


    public class CryptoWalletProcessor : IPaymentProcessor
    {
        public void PaymentMethod(double amount)
        {
            Console.WriteLine($"Crypto {amount}");
        }
    }


    public abstract class Payment
    {
        protected IPaymentProcessor processor;

        protected Payment(IPaymentProcessor processor)
        {
            this.processor = processor;
        }
        public abstract void Pay(double amount);
    }


    public class BasicPayment : Payment
    {
        public BasicPayment(IPaymentProcessor processor) : base(processor) { }

        public override void Pay(double amount)
        {
            Console.WriteLine("BasicPayment made");
            processor.PaymentMethod(amount);
        }
    }


    public class SubscriptionPayment : Payment
    {
        public SubscriptionPayment(IPaymentProcessor processor) : base(processor) { }

        public override void Pay(double amount)
        {
            Console.WriteLine("SubscriptionPayment made");
            processor.PaymentMethod(amount);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var stripeProcessor = new StripeProcessor();
            var payPalProcessor = new PayPalProcessor();
            var cryptoProcessor = new CryptoWalletProcessor();

            var oneTimePayment = new BasicPayment(stripeProcessor);
            oneTimePayment.Pay(49.99);
            var monthlySubscription = new SubscriptionPayment(payPalProcessor);
            monthlySubscription.Pay(9.99);

            var cryptoOneTime = new BasicPayment(cryptoProcessor);
            cryptoOneTime.Pay(99.99);

        }
    }
}

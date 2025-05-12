/*
You are developing a Payment System.
There are two types of users:
Admin users (can make and refund payments)
Regular users (can only make payments, but cannot refund)
✅ You must protect access to the refund() operation:
 Only Admin users can issue refunds.
 ✅ Use the Protection Proxy Pattern to enforce access control.
🛠 Tasks:
Define an interface IPaymentService:
makePayment(amount: number): void
refundPayment(amount: number): void
Create a class RealPaymentService that:
Implements the payment and refund logic (e.g., console logs the actions).
Create a User class:
Properties:
username: string
role: "admin" | "user"
Create a PaymentServiceProxy class that:
Implements IPaymentService.
Has a reference to the RealPaymentService.
Accepts a User object.
Allows makePayment() for everyone.
Allows refundPayment() only for admin users; otherwise throws an error or prints "Access Denied".
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace prob7Proxy
{
    public enum Role
    {
        Admin,
        User
    }

    interface IPaymentService
    {
        void MakePayment(int amount);
        void RefundPayment(int amount);
    }

    public class RealPaymentService : IPaymentService
    {
        private string userName;

        public RealPaymentService(string userName)
        {
            this.userName = userName;
        }
        public void MakePayment(int amount)
        {
            Console.WriteLine($"{userName} {amount}");
        }

        public void RefundPayment(int amount)
        {
            Console.WriteLine($"{userName} refund {amount}");
        }
    }

    public class User
    {
        public string UserName {  get; set; }

        public Role Role { get; set; }

        public User(string userName, Role role)
        {
            UserName = userName;
            Role = role;
        }
    }

    public class PaymentServiceProxy : IPaymentService
    {
        private readonly User _user;
        private readonly RealPaymentService _realService;
        public PaymentServiceProxy(User user)
        {
            _user = user;
            _realService = new RealPaymentService(user.UserName);
        }

        public void MakePayment(int amount)
        {
            _realService.MakePayment(amount);
        }

        public void RefundPayment(int amount)
        {
            if (_user.Role == Role.Admin)
            {
                _realService.RefundPayment(amount);
            }
            else
            {
                Console.WriteLine("Not access");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var admin = new User("ani", Role.Admin);
            var user = new User("Mont", Role.User);

            var admPay = new PaymentServiceProxy(admin);
            var userPay = new PaymentServiceProxy(user);

            admPay.MakePayment(100);
            admPay.RefundPayment(80);

            userPay.MakePayment(200);
            userPay.RefundPayment(20);
        }
    }
}

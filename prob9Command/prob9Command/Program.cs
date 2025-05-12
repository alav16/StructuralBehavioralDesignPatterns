/*
Command
You are building a banking application that supports different types of transactions:
deposit, withdraw, and transfer. To support undo functionality and allow for transaction history,
you'll use the Command design pattern to encapsulate each operation as an object.
Each command will operate on a BankAccount receiver,
and a central TransactionInvoker will execute and track commands.
🧱 Structure Overview
1. Receiver
BankAccount
Methods: Deposit(amount), Withdraw(amount)
2. Command Interface
ITransactionCommand
Execute()
Undo()
3. Concrete Commands
DepositCommand
WithdrawCommand
TransferCommand (optional)
Each command holds reference to the BankAccount and required data.
4. Invoker
TransactionInvoker
Method: ExecuteCommand(ITransactionCommand cmd)
Maintains a history stack for undo
*/

using System;
using System.Collections.Generic;


public class BankAccount
{
    public string Owner { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string owner, decimal initialBalance)
    {
        Owner = owner;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
}


public interface ITransactionCommand
{
    void Execute();
    void Undo();
}


public class DepositCommand : ITransactionCommand
{
    private readonly BankAccount _account;
    private readonly decimal _amount;

    public DepositCommand(BankAccount account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        _account.Deposit(_amount);
        Console.WriteLine($"Deposited {_amount} to {_account.Owner}. New balance: {_account.Balance}");
    }

    public void Undo()
    {
        _account.Withdraw(_amount);
        Console.WriteLine($"Undo: Reversed deposit of {_amount}. Balance is now: {_account.Balance}");
    }
}

public class WithdrawCommand : ITransactionCommand
{
    private readonly BankAccount _account;
    private readonly decimal _amount;
    private bool _wasSuccessful;

    public WithdrawCommand(BankAccount account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        _wasSuccessful = _account.Withdraw(_amount);
        if (_wasSuccessful)
        {
            Console.WriteLine($"Withdrew {_amount} from {_account.Owner}. New balance: {_account.Balance}");
        }
        else
        {
            Console.WriteLine($" Withdrawal failed for {_account.Owner}. Insufficient funds.");
        }
    }

    public void Undo()
    {
        if (_wasSuccessful)
        {
            _account.Deposit(_amount);
            Console.WriteLine($"Undo: Reversed withdrawal of {_amount}. Balance is now: {_account.Balance}");
        }
        else
        {
            Console.WriteLine("Undo: Withdrawal of 0 skipped.");
        }
    }
}


public class TransactionInvoker
{
    private readonly Stack<ITransactionCommand> _history = new Stack<ITransactionCommand>();

    public void ExecuteCommand(ITransactionCommand cmd)
    {
        cmd.Execute();
        _history.Push(cmd);
    }

    public void UndoLast()
    {
        if (_history.Count > 0)
        {
            var cmd = _history.Pop();
            cmd.Undo();
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        var account = new BankAccount("John", 1000);

        var deposit = new DepositCommand(account, 200);
        var withdraw = new WithdrawCommand(account, 150);
        var invalidWithdraw = new WithdrawCommand(account, 5000);

        var invoker = new TransactionInvoker();
        invoker.ExecuteCommand(deposit);
        invoker.ExecuteCommand(withdraw);
        invoker.ExecuteCommand(invalidWithdraw);

        invoker.UndoLast(); 
        invoker.UndoLast(); 

        Console.WriteLine("Final balance: " + account.Balance);
    }
}

using RESTfullBankAPI.Models;
using RESTfullBankAPI.Models.Records;

namespace RESTfullBankAPI.Services;

public class AccountsService(AccountContext context)
{
    /* Db Notes:
     * _context.Accounts.Add(item) adds and item
     * await _context.SaveChangesAsync() saves the changes
     * await _context.Accounts.FindAsync(id) finds the Account with an id
     */
    private readonly AccountContext _context = context;

    public Account GetAccount(Guid id)
    {
        var queriedAccount = _context.Accounts.Find(id);

        if (queriedAccount == null)
        {
            throw new ArgumentException("No user found with given id", nameof(id));
        }
        
        return queriedAccount;
    }

    public Account CreateAccount(CreationRequest request)
    {
        //TODO: finish when wifi fixes
        return null;
    }

    public decimal Deposit(Guid id, ChangeBalanceRequest request)
    {
        if (request.Amount <= 0)
        {
            //TODO: look into what the nameof operator does and the purpose that it serves in throwing an argument
            throw new ArgumentException("Requested amount must be positive", nameof(id));
        }

        var queriedAccount = GetAccount(id);
        queriedAccount.Balance += request.Amount;
        _context.SaveChanges();

        return queriedAccount.Balance;
    }

    public decimal Withdraw(Guid id, ChangeBalanceRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new ArgumentException("Requested amount must be positive", nameof(id));
        }

        var queriedAccount = GetAccount(id);

        if (request.Amount > queriedAccount.Balance)
        {
            throw new ArgumentException("Requested amount must be less than or equal to current balance", nameof(id));
        }

        queriedAccount.Balance -= request.Amount;
        _context.SaveChanges();
        
        return queriedAccount.Balance;
    }

    public decimal Transfer(TransferRequest request)
    {
        //TODO: Complete the logic in method
        var sendingAccount = GetAccount(request.SenderId);
        var recievingAccount = GetAccount(request.ReceiverId);

        return 0;
    }
}
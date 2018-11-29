using AndreasBank.Exceptions;
using AndreasBank.Repositories;
using AndreasBank.Services;
using System;
using System.Linq;
using Xunit;

namespace AndreasBankUnitTest
{
    public class BankRepositoryUnitTest
    {
        [InlineData(1, 2500)]
        [InlineData(2, 3500)]
        [InlineData(3, 4500)]
        [InlineData(3, 10050)]
        [Theory]
        public void WithdrawFromAccount_CastException_WhenInsufficientFunds(int accountId, decimal sumToWithdraw)
        {
            //Arrange
            var bankRepository = new BankRepository();
            var bankService = new BankService(bankRepository);
            Exception actualException = new Exception();

            //Act
            Action withdraw = () => bankService.Withdraw(accountId, sumToWithdraw);

            //Assert
            Assert.Throws<InsufficientFundsException>(withdraw);
        }

        [InlineData(1, 1500, 500)]
        [InlineData(2, 2500, 500)]
        [InlineData(3, 3500, 500)]
        [InlineData(4, 9500, 500)]
        [Theory]
        public void AccountShowsRightActualAmount_WhenWithdraw(int accountId, decimal sumToWithdraw, decimal expectedActualSum)
        {
            //Arrange
            var bankRepository = new BankRepository();
            var bankService = new BankService(bankRepository);

            //Act
            bankService.Withdraw(accountId, sumToWithdraw);
            var account = bankRepository.GetAccountById(accountId);

            //Assert
            Assert.Equal(expectedActualSum, account.ActualSum);
        }

        [InlineData(1, 2500, 500)]
        [InlineData(2, 3500, 500)]
        [InlineData(3, 4500, 500)]
        [InlineData(4, 10500, 500)]
        [Theory]
        public void AccountShowsRightActualAmount_WhenDeposit(int accountId, decimal expectedActualSum, decimal sumToDeposit)
        {
            //Arrange
            var bankRepository = new BankRepository();
            var bankService = new BankService(bankRepository);

            //Act
            bankService.Deposit(accountId, sumToDeposit);
            var account = bankRepository.GetAccountById(accountId);

            //Assert
            Assert.Equal(expectedActualSum, account.ActualSum);
        }

        [Fact]
        public void Deposit_NonPositiveValue_CastsException()
        {
            //Arrange
            var bankRepository = new BankRepository();
            var bankService = new BankService(bankRepository);
            Exception actualException = new Exception();

            //Act
            Action withdraw = () => bankService.Deposit(1, -500);

            //Assert
            Assert.Throws<NonPositiveValueException>(withdraw);
        }

        [Fact]
        public void Withdraw_NonPositiveValue_CastsException()
        {
            //Arrange
            var bankRepository = new BankRepository();
            var bankService = new BankService(bankRepository);
            Exception actualException = new Exception();

            //Act
            Action withdraw = () => bankService.Withdraw(1, -500);

            //Assert
            Assert.Throws<NonPositiveValueException>(withdraw);
        }

        [Fact]
        public void Account_Shows_Correct_Balance_After_Transfer()
        {
            //Arrange
            var bankRepository = new BankRepository();
            var bankService = new BankService(bankRepository);

            var fromAccount = bankRepository.Accounts.First();
            var toAccount = bankRepository.Accounts.Last();
            decimal balanceToTransfer = fromAccount.Balance - 1;
            decimal toAccountBalanceBefore = toAccount.Balance;

            //Act
            bankService.Transfer(fromAccount.Id, toAccount.Id, balanceToTransfer);

            //Assert
            Assert.Equal(1, fromAccount.Balance);
            Assert.Equal(toAccountBalanceBefore + balanceToTransfer, toAccount.Balance);
        }

        [Fact]
        public void Should_Not_Allow_Transfer_When_Not_Not_Enough_Money()
        {
            //Arrange
            var bankRepository = new BankRepository();
            var bankService = new BankService(bankRepository);

            var fromAccount = bankRepository.Accounts.First();
            var toAccount = bankRepository.Accounts.Last();
            decimal balanceToTransfer = fromAccount.Balance + 1;
            decimal fromAccountBalanceBefore = fromAccount.Balance;
            decimal toAccountBalanceBefore = toAccount.Balance;

            //Act
            var result = bankService.Transfer(fromAccount.Id, toAccount.Id, balanceToTransfer);

            //Assert
            Assert.False(result);
            Assert.Equal(fromAccountBalanceBefore, fromAccount.Balance);
            Assert.Equal(toAccountBalanceBefore, toAccount.Balance);
        }
    }
}

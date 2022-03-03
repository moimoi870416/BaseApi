namespace Base.Api.Enums
{
    public enum EnumTransferMethod : int
    {
        DepositBankTransfer = 1,
        DepositInternetBanking = 2,
        WithdrawalBankTransfer = 4,
        WithdrawalInternetBanking = 8
    }
    public enum TransactionStatus : int
    {
        Waiting = 1,
        Verified,
        Approved,
        Rejected,
        Cancelled,
        Completed,
        Manual,
        All,


		//Payment
		PaymentStarted = 1000, //after player click deposit, and redirect to deposit widget.
		PaymentTransferSuccess = 1001, //payment gate way already finish all the process.
		PaymentProcessing = 1002, //player clicking on payment, only will get when check status api
		PaymentPending = 1003, //pending bank side, only will get when check status api
 		PaymentRejected = 1004, //payment gateway reject the transaction
        PaymentPaid = 1005, //payment gateway withdrawal usage
        PaymentCanceled = 1006, //payment gateway cancel the transaction
        PaymentSuccessDepositToPlayer = 1007, //internal status, after successfully deposit to player(Add_SingerTransfer) will be this status.
        PaymentError = 1008, //payment gateway return error
        PaymentRejectedByCompany = 1009,  //will update to this status by scheduler.
        PaymentInternalTransactionError = 1010, //internal or Hermes error.
        PaymentSuccessHoldOnCreditFromPlayer = 1011, //internal status, after successfully on hold credit from player(Add_SingerTransfer) will be this status.
        PaymentSuccessRollBackCreditToPlayer = 1012, //internal status, after successfully roll back credit to player(Add_SingerTransfer) will be this status.
        PaymentAdminWaiting = 1013, //after request withdrawal, need admin verified in BO 9.2 then send request to payment
        PaymentAdminVerified = 1014, //after request withdrawal, need admin approve in BO 9.2 then send request to payment
        PaymentAdminApproved = 1015, //after request withdrawal, need payment provider approve
        PaymentAdminRejected = 1016, //after request withdrawal, need payment provider approve
    }

    public enum PromotionRequestStatus : int
    {
        Waiting = 1,
        Approved,
        Rejected,
        Completed,
        Cancelled,
        All
    }

    public enum ReferralRedeemRequestStatus : int
    {
	    Waiting = 1,
	    Approved,
	    Rejected,
    }

    public enum PaymentProviderTransactionStatus : int
    {
        TransactionStart = 0,
        Accepted = 1,//deposit
        Processing = 2,
        Pending = 3,
        Rejected = 4,
        Paid = 5,//withdrawal
        Canceled = 6,
        Authorized = 7,
        Expired = 8,
        Error = 500
    }

    public static class DepositStatusExtensions
    {
        public static TransactionStatus ConvertToTransactionStatus(this PaymentProviderTransactionStatus status)
        {
            switch (status)
            {
                case PaymentProviderTransactionStatus.TransactionStart:
                    return TransactionStatus.PaymentStarted;

                case PaymentProviderTransactionStatus.Accepted:
                    return TransactionStatus.PaymentTransferSuccess;

                case PaymentProviderTransactionStatus.Processing:
                    return TransactionStatus.PaymentProcessing;

                case PaymentProviderTransactionStatus.Pending:
                    return TransactionStatus.PaymentPending;

                case PaymentProviderTransactionStatus.Rejected:
                    return TransactionStatus.PaymentRejected;

                case PaymentProviderTransactionStatus.Paid:
                    return TransactionStatus.PaymentPaid;

                case PaymentProviderTransactionStatus.Error:
                    return TransactionStatus.PaymentError;

                case PaymentProviderTransactionStatus.Canceled:
                    return TransactionStatus.PaymentCanceled;

                case PaymentProviderTransactionStatus.Authorized:
                    return TransactionStatus.PaymentProcessing;
                case PaymentProviderTransactionStatus.Expired:
                    return TransactionStatus.PaymentRejectedByCompany;


                default:
                    return TransactionStatus.PaymentError;
            }
        }
    }
}
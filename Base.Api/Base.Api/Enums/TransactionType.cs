namespace Base.Api.Enums
{
    public enum TransactionType
    {
        Unknown = -1,
        FullTransfer = 0,
        Deposit = 100,
        Withdrawal = 200,
        PromotionBonus = 300,
        ReferralRedeem = 400,
        InjectBonus = 500,
        PaymentGatewayDeposit = 600,
        PaymentGatewayWithdrawal = 700,
        P2PDeposit = 800,
        P2PWithdrawal = 900,
        StockDeposit = 1000,
        StockWithdrawal = 1100,
        StockRevenueOut = 1200,
        StockRevenueIn = 1300,
        PromotionDailyCommission = 1400,
        ManualDeposit = 1500,
        ManualWithdrawal = 1600,
        ManualBonus = 1700
    }
}
using System.ComponentModel;

namespace Base.Api.Enums
{
    public enum ApiReturnError
    {
        [Description("Internal Error")]
        DbError = -1,
        [Description("Success")]
        Success = 0,
        [Description("No Change")]
        NoChange = 1,

        //Register
        [Description("Invalid Request Parameter")]
        InvalidParameter = 101,
        [Description("Invalid Password Format")]
        InvalidPasswordFormat = 102,
        [Description("Invalid Currency")]
        InvalidCurrency = 103,
        [Description("Invalid Username Format")]
        InvalidUsernameFormat = 104,
        [Description("Invalid Web Id")]
        InvalidWebId = 105,
        [Description("Invalid Account Type")]
        InvalidAccountType = 106,
        [Description("Invalid Product Type")]
        InvalidProductType = 107,
        [Description("Invalid Operator Id")]
        InvalidOperatorId = 108,
        [Description("Invalid Contact Format")]
        InvalidContactFormat = 109,
        [Description("Duplicate Instance Message Type")]
        DuplicateInstanceMessageType = 110,
        [Description("Invalid Instance Message Type")]
        InvalidInstanceMessageType = 111,
        [Description("Invalid Cash Agent RequestId")]
        InvalidCashAgentRequestId = 112,
        [Description("Duplicate Email")]
        DuplicateEmail = 113,
        [Description("Duplicate FullName")]
        DuplicateFullName = 114,
        [Description("Duplicate Line")]
        DuplicateLine = 115,
        [Description("Duplicate Mobile")]
        DuplicateMobile = 116,
        [Description("Duplicate Phone")]
        DuplicatePhone = 117,
        [Description("Invalid Stock Holding")]
        InvalidStockHolding = 118,
        [Description("Invalid Email Format")]
        InvalidEmailFormat = 119,
        [Description("Invalid Percentage")]
        InvalidPercentage = 120,
        [Description("Reference Number Is Required")]
        ReferenceNumberIsRequired = 121,
        [Description("Deposit Time Is Required")]
        DepositTimeIsRequired = 122,
        [Description("Deposit Slip Is Required")]
        DepositSlipIsRequired = 123,

        //Login
        [Description("Wrong Username Or Password")]
        WrongUserNameOrPassword = 200,
        [Description("Failed to request SSO")]
        RequestSSOFailed = 201,
        [Description("Failed to consume SSO")]
        ConsumeSSOFailed = 202,
        [Description("Multiple login")]
        MultipleLogin = 203,
        [Description("Session time out")]
        SessionTimeOut =  204,
        [Description("Invalid online id")]
        InvalidOnlineId = 205,
        [Description("Invalid Login IP")]
        InvalidIP = 206,
        [Description("Login fail too many times, account suspend")]
        LoginFailUserSuspend = 207,
        [Description("Login token invalid")]
        LoginTokenInvalid = 208,


        //Transaction
        [Description("Bank Id Error")]
        BankIdError = 300,
        [Description("Exceed deposit max")]
        ExceedDepositMax = 301,
        [Description("Exceed deposit min")]
        ExceedDepositMin = 302,
        [Description("Exceed withdraw max")]
        ExceedWithdrawalMax = 303,
        [Description("Exceed withdraw min")]
        ExceedWithdrawalMin = 304,

        [Description("Invalid transaction type")]
        InvalidTransactionType = 305,
        [Description("Invalid transaction status")]
        InvalidTransactionStatus = 306,
        [Description("Invalid company bank")]
        InvalidCompanyBank = 307,
        [Description("Invalid transaction operation")]
        InvalidTransactionOperation = 308,
        [Description("Only positive amount allowed")]
        OnlyAllowPositiveAmount = 309,
        [Description("Invalid transaction id format")]
        InvalidTransactionIdFormat = 310,
        [Description("Invalid transaction action")]
        InvalidTransactionAction = 311,
        [Description("No transaction found")]
        TransactionNotFound = 312,
        [Description("There is a waiting transaction exists")]
        WaitingTransactionExists = 313,
        [Description("Invalid priority")]
        InvalidPriority = 314,
        [Description("Invalid bank group")]
        InvalidBankGroup = 315,
        [Description("Verified amount bigger than requested amount")]
        VerifiedAmountBiggerThanRequestedAmount = 316,
        [Description("Invalid transfer amount")]
        InvalidTransferAmount = 317,
        [Description("Negative outstanding")]
        NegativeOutstanding = 318,
        [Description("Transfer amount exceed Child Balance")]
        TransferAmountExceedChildBalance = 319,
        [Description("Tranfer amount exceed Parent Balance")]
        TransferAmountExceedParentBalance = 320,
        [Description("You may only request for withdrawal once within 24 hours")]
        ExceedDailyWithdrawLimit = 321,
        [Description("Payment Deposit request should only send once per minutes")]
        SendPaymentDepositRequestInTooShortTime = 322,
        [Description("The Bank Account Number Already Registered")]
        BankRegistered = 323,
        [Description("Withdrawl Limit Is Not Zero")]
        WithdrawlLimitIsNotZero = 324,
        [Description("Decimal not allow")]
        DecimalNotAllow = 325,
        [Description("Invalid Payment Password")]
        InvalidPaymentPassword = 326,

        //Promotion
        [Description("Waiting / Incomplete Promotion Exists")]
        WaitingOrIncompletePromotionExists = 330,
        [Description("Invalid Promotion request status")]
        InvalidPromotionRequestStatus = 331,
        [Description("Duplicate PromotionCode")]
        DuplicatePromotionCode = 332,
        [Description("Apply Promotion Over Limit")]
        ApplyPromotionOverLimit = 333,
        [Description("Deposit Lower Than Promotion MinDeposit")]
        DepositLowerThanPromotionMinDeposit = 334,
        [Description("No Available Deposit")]
        NoAvailableDeposit = 335,
        [Description("Promotion Type Is Not Deposit")]
        PromotionTypeIsNotDeposit = 336,
        [Description("Not Qualified For This Promotion")]
        NotQualifiedForPromotion = 337 ,
        [Description("Register Promotion Auto Apply Error")]
        RegisterPromotionAutoApplyError = 338,
        [Description("Register Promotion Get PromoList Error")]
        RegisterPromotionGetPromoListError = 339,
        [Description("Register Promotion Auto Approve Error")]
        RegisterPromotionAutoApproveError = 340,
        [Description("Detect Promotion Wallet has unsettle bets")]
        PromotionWalletHasUnSettleBets = 341,

        //Theme
        [Description("Invalid Theme Name")]
        InvalidThemeName = 400,
        [Description("Invalid Html Id")]
        InvalidHtmlId = 401,
        [Description("Invalid Theme Id")]
        InvalidThemeId = 402,

        //Referral
        [Description("Invalid Referral Option Id")]
        InvalidReferralOptionId = 500,
        [Description("Invalid Referral Type Id")]
        InvalidReferralTypeId = 501,
        [Description("Invalid Referral Start Time")]
        InvalidReferralStartTime = 502,
        [Description("Invalid Referral Redeem Amount")]
        InvalidReferralRedeemAmount = 503,
        [Description("Invalid Referral Request Id")]
        InvalidRedeemRequestId = 504,
        [Description("Invalid Referral Redeem Status")]
        InvalidProcessRedeemStatus = 505,
        [Description("Duplicate Referral Layer")]
        DuplicateReferralLayer = 506,
        [Description("Duplicate Referral RebateTypeId")]
        DuplicateReferralRebateTypeId = 507,
        [Description("Invalid Referral User")]
        InvalidReferralUser = 508,

        //CommonError
        [Description("Invalid Customer Id")]
        InvalidCustomerId = 800,
        [Description("Invalid Parent Id")]
        InvalidParentId = 801,
        [Description("Update Error")]
        UpdateError = 802,
        [Description("Invalid Status")]
        InvalidStatus = 803,
		[Description("Invalid DateTime")]
		InvalidDateTime = 804,
        [Description("No Correspond LiveChatDomain")]
        NoCorrespondLiveChatDomain = 805,
        [Description("No Data")]
        NoData = 806,
        [Description("Not a Company Account")]
        NotACompanyAccount = 807,
        [Description("User not exists")]
        UserNotExists = 808,
        [Description("NotCashPlayer")]
        NotCashPlayer = 809,
        [Description("Not A Valid Target")]
        NotAValidTarget = 810,
        [Description("Invalid Start Dateime")]
        InvalidStartDatetime = 811,
        [Description("Invalid DateTime Range")]
        InvalidDatetimeRange = 812,
        [Description("DateTime Range Over 7 Days")]
        DateTimeRangeOver7Days = 813,
        [Description("DateTime Range Over 2 months")]
        DateTimeRangeOver2Months = 814,

        [Description("Model State Error")]
        ModelStateError = 997,
        [Description("Not Authorized")]
        NotAuthorized = 998,
        [Description("General Error")]
        GeneralError = 999,

        //User and Account Status
        [Description("User Not Found")]
        UserNotFound = 1001,
        [Description("Account Is Closed")]
        AccountIsClosed = 1002,
        [Description("Account Is Suspended")]
        AccountIsSuspended = 1003,
        [Description("Account Is Deleted")]
        AccountIsDeleted = 1004,
        [Description("Duplicate Username")]
        DuplicateUsername = 1005,
        [Description("Insufficient Balance")]
        InsufficientBalance = 1006,
        [Description("Duplicate Value on unique required column")]
        DuplicateSettingValue = 1007,
        [Description("Not able to update this user's currency")]
        NotAbleUpdateThisUsersCurrency = 1008,
        [Description("Agent Not Found")]
        AgentNotFound = 1009,
        [Description("Target Account Not Available")]
        TargetAccountNotAvailable = 1010,
        [Description("Company Is Caped")]
        CompanyIsCaped = 1011,
        [Description("Account Is Not Able To Do Stock Transfer")]
        AccountIsNotAbleToDoStockTransfer = 1012,

        //Admin
        [Description("Duplicate Brand Name")]
        DuplicateBrandName = 1996,
        [Description("Exceed Max Ip Count")]
        ExceedMaxIpCount = 1997,
        [Description("Create Downline Fail")]
        CreateDownlineFail = 1998,
        [Description("Invalid Username Or Password")]
        InvalidUsernameOrPassword = 1999,
        [Description("No Downline Found")]
        NoDownlineFound = 2000,
        [Description("Admin Not Found")]
        AdminNotFound = 2001,
        [Description("Username Taken")]
        UsernameTaken = 2002,
        [Description("Please choose a stronger Username. Try a mix of letters, numbers, and '_'")]
        UsernameNotStrongEnough = 20021,
        [Description("Cannot Create Cross Level Member")]
        CannotCreateCrossLevelMember = 2003,
        [Description("Invalid Lc Max Limit")]
        InvalidLcMaxLimit = 2004,
        [Description("Invalid Discount")]
        InvalidDiscount = 2005,
        [Description("Invalid Table Limit")]
        InvalidTableLimit = 2006,
        [Description("Invalid Commission")]
        InvalidCommission = 2007,
        [Description("Invalid Min PT")]
        InvalidMinPT = 2008,
        [Description("Invalid Group A Discount")]
        InvalidGroupADiscount = 2009,
        [Description("Invalid Group B Discount")]
        InvalidGroupBDiscount = 2010,
        [Description("Invalid Group C Discount")]
        InvalidGroupCDiscount = 2011,
        [Description("Invalid Group Other Discount")]
        InvalidGroupOtherDiscount = 2012,
        [Description("Invalid Group 1X2 Discount")]
        InvalidGroup1x2Discount = 2013,
        

        //Resource
        [Description("Invalid Resource Group Id")]
        InvalidResourceGroupId = 2014,
        [Description("Invalid Resource Id")]
        InvalidResourceId = 2015,

        //PT
        [Description("Invalid Force PT")]
        InvalidForcePT = 2016,
        [Description("Over Max PT")]
        OverMaxPT = 2017,
        [Description("Invalid User Group")]
        InvalidUserGroup = 2018 ,
        [Description("Keyword can't be Empty")]
        KeyWordCantBeEmpty= 2019,
        [Description("Invalid PT")]
        InvalidPT = 2020,

        //PT By Gp
        [Description("MA Havent set Agent PT for This Game Provider")]
        AgentPresetPTHaventSet = 2040,

        //MaxWinLoseSetting
        [Description("Invalid GM Player Max Win")]
        InvalidGMPlayerMaxWin = 2050,
        [Description("Invalid GM Player Max Lose")]
        InvalidGMPlayerMaxLose = 2051,
        [Description("Invalid GM Player Daily Reset")]
        InvalidGMPlayerDailyReset = 2052,
        [Description("Invalid LC Player Max Win")]
        InvalidLCPlayerMaxWin = 2053,
        [Description("Invalid LC Player Max Lose")]
        InvalidLCPlayerMaxLose = 2054,
        [Description("Invalid LC Player Daily Reset")]
        InvalidLCPlayerDailyReset = 2055,

        [Description("Invalid Max Win")]
        OverPlayerMaxWin = 2056,
        [Description("Invalid Max Lose")]
        OverPlayerMaxLose = 2057,

        //Bet Setting
        [Description("Invalid Min Bet")]
        InvalidMinBet = 2500,
        [Description("Invalid Max Bet")]
        InvalidMaxBet = 2501,
        [Description("Invalid Max Per Match")]
        InvalidMaxPerMatch = 2502,
        [Description("Invalid Max Credit")]
        InvalidMaxCredit = 2503,
        [Description("Invalid Live Casino Max Limit")]
        InvalidLiveCasinoMaxLimit = 2504,
        [Description("Invalid Credit")]
        InvalidCredit = 2505,
        [Description("Min Bet Lower Than Zero")]
        MinBetLowerThanZero = 2506,
        [Description("Max Bet Lower Than Zero")]
        MaxBetLowerThanZero = 2507,
        [Description("Min Bet Greater Than Max Bet")]
        MinBetGreaterThanMaxBet = 2508,
        [Description("Max Bet Greater Than Max Per Match")]
        MaxBetGreaterThanMaxPerMatch = 2509,
        [Description("Max Per Match Equal To Zero")]
        MaxPerMatchEqualtoZero = 2510,
        [Description("Min Bet Greater Than Max Per Match")]
        MinBetGreaterThanMaxPerMatch = 2511,
        [Description("Invalid Group Type")]
        InvalidGroupType = 2512,
        [Description("Invalid Max Bet Ratio")]
        InvalidMaxBetRatio = 2513,

        //Game/GameProvider
        [Description("Duplicate Game Provider")]
        DuplicateGameProvider = 3000,
        [Description("Duplicate Game")]
        DuplicateGame = 3001,
        [Description("Invalid Wallet Type")]
        InvalidWalletType = 3002,
        [Description("Invalid Game Provider")]
        InvalidGameProvider = 3003,

        //Domain
        [Description("Duplicate Domain Exists")]
        DuplicateDomain = 4000,

        //PlaceOrder/Settle
        [Description("Bet Already Settled")]
        BetAlreadySettled = 5000,
        [Description("Bet Already Canceled")]
        BetAlreadyCanceled = 5001,
        [Description("Cannot Rollback Running Bet")]
        CannotRollBackRunningBet = 5002,
        [Description("Bet With Same RefNo Exists")]
        BetWithSameRefNoExists = 5003,
        [Description("Bet Not Found")]
        BetNotFound = 5004,
        [Description("Stake is Lower than Min Bet")]
        LowerThenMinBet = 5005,
        [Description("Stake is Bigger than Max Bet")]
        BiggerThenMaxBet = 5006,
        [Description("Bet winlost amount is not valid for the Status")]
        StatusAmountNotValid = 5007,
        [Description("Bet Already Returned Stake")]
        BetAlreadyReturnedStake = 5008,
        [Description("RefNo No Data")]
        RefNoNotData = 5009,

        //Promotion
        [Description("Running Promotion Exists")]
        RunningPromotionExists = 6000,

        //LoginName
        [Description("Login Name Updated Before")]
        LoginNameUpdated = 7000,
        [Description("Login Name Taken.")]
        DuplicateLoginName = 7001,


		//Payment
		[Description("Payment Amount <= 0")]
		PaymentInvalidAmount = 8000,
        [Description("Invalid Payment Provider Id")]
        InvalidPaymentProviderId = 8001,
        [Description("Payment Gateway Functions Are Disabled")]
        PaymentGatewayIsDisabled = 8002,
        [Description("Payment Gateway Manual Callback Error")]
        PaymentGatewayManualCallbackError = 8003,

        //Affiliate
        [Description("You Have Apply In Progress Or Rejected Within A Period Of Time")]
        AlreadyApply = 9003,
        
        //Risk Control
        [Description("This player has been tag as a suspicious player")]
        AlreadyTagBefore = 10001,
        [Description("This player has not been tag as a suspicious player before")]
        NoTagBefore = 10002,
        [Description("You do not have permission,please contact our support")]
        NoPermission = 10003,

        //LanguageSettings
        [Description("At least one modification information is required")]
        NoSettingData = 11001,
        [Description("At least need one language enabled")]
        NeedLeastOneEnabled = 11002
    }
}

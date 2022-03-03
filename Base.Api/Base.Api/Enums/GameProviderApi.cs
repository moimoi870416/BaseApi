namespace Base.Api.Enums
{
    public enum GameProviderApi
    {
        Login = 1,
        GetBalance = 2,
        Transfer = 3,
        UpdateMemberUserGroup = 4,
        UpdateMemberBetSettings =  5,
        UpdateCreditMemberBetSettings = 6 ,
        GetLast50Page = 7,
        GetForecastPage = 8,
        GetLeague = 9,
        GetLeagueBetSetting = 10,
        SetLeagueBetSetting = 11,
        GetLeagueGroupBetSetting = 12,
        SetLeagueGroupBetSetting = 13,
        GetBetPayload = 14,
        SetPlayerSuspiciousForSports = 15,
        DelaySettleByBets = 16,
        RemoveDelaySettleByBets = 17,
        VoidSingleBetByTransid = 18,
        ReloadCustomerSeamlessWalletInfo = 19,
        CheckAbnormalPlayerSport = 20,
        SetPlayerUnsuspiciousForSports = 21,
        GetSportsMatchResultPage = 22,
        UpdateCashBetSetting = 23,
        UpdateCashBetSettingByType = 24,
        UpdateCashPresetBetSettingByType = 25
    }
}
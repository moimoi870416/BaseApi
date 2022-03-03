namespace Base.Api.Enums
{
    public enum PromotionStatus
    {
        Active = 0,
        Disabled = 1,
        Deleted = 2,
        ActiveAndOpenForApply = 3,
    }

    public enum PromotionGameProvider
    {
        ALL = -1,
        WM = 0,
        SBO = 1,
        CQNine = 2,
        PragmaticPlay = 3,
        DreamGaming = 4,
        BigGaming = 5,
        FlowGaming = 6,
        SexyBaccarat = 7,
        Celton = 8,
        GamingWorld = 9,
        JokerGaming = 10,
        RTG = 11,
        RealTimeGaming = 11,
        IONLC = 12,
        WorldMatch = 13,
        CreativeGaming = 14,
        Saba = 15,
    }

    public enum PromotionGameCategory
    {
        ALL = -1,
        SPORTS = 1,
        LIVE_CASINO = 2,
        GAMES = 3,
        VIRTUAL_SPORTS = 4,
        SEAMLESS_GAMEPROVIDER = 5
    }

    public enum PromotionGameType
    {
        All = -1,
        SPORTS = 1,
        BACCARAT = 2,
        ROULETTE = 3,
        SIC_BO = 4,

        //BLACKJACK= 5,
        DRAGON_TIGER = 6,

        RNG_GAMES = 7,
        SLOTS = 8,

        //FISHING = 9,
        //KENO = 10,
        //LOTTERY = 11,
        VIRTUAL_SPORTS = 12,

        SEAMLESS_GAME = 13
    }
}
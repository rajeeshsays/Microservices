namespace TransportService.Model
{
    public enum EnumLoanCategory
    {
        Select = 0,
        FishnetLoan = 1,
        FishTradingLoan = 2,
        EducationLoan = 3,
        BoatLoan = 4,
        ShareLoan = 5,
        SpecialLoan = 6
    }

    public enum EnumMembershipCategory
    {

        Select = 0,
        Mathsayavari = 1,
        Karavari = 2,
        Others = 3

    }

    public enum EnumLoanStatus
    {
        Created = 0,
        Opened = 1,
        Closed = 2
    }
    public enum EnumLoanInterest
    {
        Select = 0,
        Interest_5 = 5,
        Interest_9 = 9,
        Interest_10 = 10,
        Interest_11 = 11,
        Interest_12 = 12,
        Interest_13 = 13,
        Interest_14 = 14,
        Interest_15 = 15,
        Interest_16 = 16,
        Interest_17 = 17,
        Interest_18 = 18,
    }

    public enum EnumSharePenaltyInterest
    {
        Select = 0,
        Interest_1 = 1,
        Interest_2 = 2,
        Interest_3 = 2,
        Interest_4 = 4,
        Interest_5 = 5,
        Interest_6 = 6,
        Interest_7 = 7,
        Interest_8 = 8,
        Interest_9 = 9,
        Interest_10 = 10,
        Interest_11 = 11,
        Interest_12 = 12,
        Interest_13 = 13,
        Interest_14 = 14,
        Interest_15 = 15,
        Interest_16 = 16,
        Interest_17 = 17,
        Interest_18 = 18

    }


    public enum EnumShareUnit
    {
        Select = 0,
        ShareUnit_100 = 100,
        ShareUnit_200 = 200,
        ShareUnit_300 = 300,
        ShareUnit_400 = 400,
        ShareUnit_500 = 500,
        ShareUnit_600 = 600,
        ShareUnit_700 = 700,
        ShareUnit_800 = 800,
        ShareUnit_900 = 900,
        ShareUnit_1000 = 1000,
    }

    public enum EnumFinancialYear
    {
        Select = 0,
        FinYear_2017_2018 = 1,
        FinYear_2018_2019 = 2,
        FinYear_2019_2020 = 3,
        FinYear_2020_2021 = 4,
        FinYear_2021_2022 = 5
    }
    public enum EnumFinancialYearStatus
    {

        Created = 0,
        Opened = 1,
        Closed = 2

    }

    public enum EnumAccountMain
    {
        SundryCreditors = 1,
        SundryDebtors = 2,
        DirectIncome = 3,
        DirectExpense = 4,
        IndirectIncome = 5,
        IndirectExpense = 6,
        Asset = 7,
        ShortTermLiability = 8,
        LongTermLiability  = 9,
        StakeHoldersEquity = 10
    }

    public enum EnumAccountGroupOld
    {
        Select = 0,
        AgentComm = 1,
        BankInterestRecieved = 2,
        LoanInterestRecieved = 3,
        LoanInterestPaid = 4,
        RentRecieved = 5,
        RentPaid = 6,
        AgentBusinessPrinciple = 7,
        SmallScaleRecieved = 8,
        LoanLendout = 9,
        LoanBorrowed = 10,
        Salary = 11,
        FishnetExpense = 12,
        OutsideFishExpense = 13,
        Agent = 14,
        Electricity = 15,
        Cash = 16,
        Phone = 17,
        Bank = 18,
        PenaltyInterestRecieved = 19,
        PenaltyInterestPaid = 20,
        Utility = 21,
        MislaneousExpense = 22,
        MembershipFees = 23,
        KSFE = 24,
        DHARMAVU = 25,
        Wage = 26,
        OutsideFishIncome = 27,
        FishnetIncome = 28,
        Member = 30,
        SundryDebtors = 31,
        SundryCreditors = 32
}

    public enum EnumAccountGrp
    {
    Salary = 1,
	Rent_Expense = 2,
	Electricity =3,
	Phone = 4, 
	Internet = 5, 
	Rent_Income = 6, 
	Wage = 7, 
	Frieght = 8

    }



    public enum EnumSettings
    {
        SSF_ReferenceNo_Initial = 1,
        Loan_ReferenceNumber_Voucher_Initial = 2,
        FishnetDaily_ReferenceNumber_Initial = 3,
        MembershipFees = 4,
        MembershipFees_GrancePeriod = 5
    }

    public enum EnumAccounts
    {
        Cash_In_Transit                        = -29,
       	Bank_In_Transit                        = -28,
       	Credit_Card                            = -27,
       	Credit_Card_Expense                    = -26,
       	RENT_RECIEVED                          = -25,
       	INTERNET                               = -24,
       	CARRIAGE_OUTWARD                       = -23,
       	CARRIAGE_INWARD                        = -22,
       	TELEPHONEBILL                          = -21,
       	FUEL                                   = -20,
       	SNACKS_AND_BEVERAGES                   = -19,
       	STATIONARY_PRINTING                    = -18,
       	FRIEGHT                                = -17,
       	PUBLIC_DONATION                        = -16,
       	WATER                                  = -15,
       	ELECTRICITY                            = -14,
       	SALE_TAX                               = -13,
       	BUILDING_AND_LAND_TAX                  = -12,
        PREPAID_INSURANCE                      = -11,
        PETTY_CASH                             = -10,	
        RENT                                   = -9	,
        FURNITURE                              = -8	,
        WAGE                                   = -7	,
        SALARY                                 = -6	,
        BANK_OVERDRAFT                         = -5	,
        BANK                                   = -4	,
        CASH                                   = -3	,
        SALE                                   = -2	,
        PURCHASE                               = -1,
        CARD                                   = -30,
        UPI                                    = -31,
        CARD_PAYER_COMMISSION                  = -32,
        CHEQUE                                 = -33,
        UPI_PAYER_COMMISSION                   = -34,
        CREDIT                                 = -35,
        TRASPORTENTRY_RENT                     = 26,
        TRCOMPANY                              = 27

    }

    public enum EnumIncomeAndExpense
    {
        FishnetIncome = 1,
        OutsideFishnetIncome = 2,
        OutsideFishPrinciplePayed = 3,
        OutsideFishPrincipleReturned = 4,
        LoanInterestRecieved = 5,
        BankInterestPayed = 6,
        BankInterestRecieved = 7,
        LoanPrincipleReturned = 8,
        LoanPrinciplePayed = 9,
        RentRecieved = 10,
        RentPayed = 11,
        MembershipFeesReceived = 12,
        ElectricityBillPayed = 13,
        PhoneBillPayed = 14,
        StationaryPurchased = 15,
        MislaneousExpense = 16,
        SalaryPayed = 17,
        WagePayed = 18,
        AgentCommissionPayed = 19,
        DonationPayed = 20,
        AidAssistancePayed = 21


    }

    public static class EnumAssetAndliability
    {

        public static class Asset
        {


            public enum IntangibleAsset
            {

                GoodWill = 1,

                Tradenames = 2,

                TotIntangAsset = 3
            }


            public enum CurrentAsset
            {
                Cash = 4,
                PettyCash = 5,
                TemperoryInvestment = 6,
                AccountReceivable = 7,
                Inventroy = 8,
                Supplies = 9,
                PrepaidInsurance = 10,
                TotCurrAsset = 11

            }

            public enum FixedAsset
            {

                Land = 12,
                LandImprovements = 13,
                Buildings = 14,
                Equipements = 15,
                LessAccumDepreciation = 16,
                NetProperyPlant = 17

            }
            public enum TemperoryInvestments
            {
                TempInvestment = 18
            }


        }
        public static class Libability
        {
            public enum CurrentLiability
            {

                NotesPayable = 19,
                AccountPayable = 20,
                WagesPayable = 21,
                InterestPayable = 22,
                TaxesPayable = 23,
                WarrantyLiability = 24,
                UnearnedRevenues = 25
            }
            public enum LongTermLiability
            {
                NotesPayable = 26,
                BondsPayable = 27,
                TotLongTermLiables = 28

            }

            public enum StakeHoldersEquity
            {

                CommonStock = 29,
                RetainedEarnings = 30,
                AccmOthrComprhIncome = 31,
                LessTreasurystock = 32

            }

        }

    }
    public enum EnumTransactionMode
    {
        Select = 0,
        Payment = 1,
        Receipt = 2
    }
    public enum EnumPaymode
    {
        CASH = 1,
        CREDIT=2,
        CARD = 3,
        UPI = 4,
        CHEQUE = 5
    }
    public enum EnumAgentCommissonGenerated
    {
        Cancelled = 0,
        Generated = 1,
    }

    public enum EnumCommissionInterest
    {
        AgentCommissionInterest_1 = 1,
        AgentCommissionInterest_2 = 2,
        AgentCommissionInterest_3 = 3,
        AgentCommissionInterest_4 = 4,
        AgentCommissionInterest_5 = 5,
        AgentCommissionInterest_6 = 6,
        AgentCommissionInterest_7 = 7,
        AgentCommissionInterest_8 = 8,
        AgentCommissionInterest_9 = 9
    }

    public enum EnumShareDuration
    {
        Select = 0,
        Share_Duration = 50
    }

    public enum EnumOFProfPercent
    {
        Select = 0,
        Pro_5 = 5,
        Pro_12 = 12

    }
    public enum EnumCommCategory
    {
        Select = 0,
        FishnetDaily = 1,
        OutsideFishMem = 2,
        OutsideFish = 3
    }

    public enum EnumAccGroupsubs
    {
        AgentComm = 1,
        AgentBussinessPrinciple = 6,
        RentRecieved = 4
    }
    public enum EnumLoanType
    {
        MortguageLoan = 1,
        PersonalLoan  = 2,
        TradingLoan    = 3
    }
    public enum EnumLoanRepayType
    {
        Diminishing = 1,
        Flat = 2
    }

    public enum EnumJournalEntryType
    {
        CreditCardSales = 1,
        DebitCardSales  = 2,
        CashSales =3,
        ChequeSales =4


    }
}
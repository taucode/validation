using System;
using System.Collections.Generic;
using System.Linq;
using TauCode.Extensions;

namespace TauCode.Validation.Tests.Core
{
    public static class DataConstants
    {
        public static class DateAndTime
        {
            public static readonly DateTimeOffset MinDate = "1900-01-01Z".ToUtcDateOffset();
            public static readonly DateTimeOffset MaxDate = "2999-12-31Z".ToUtcDateOffset();
        }

        public static class SystemWatcher
        {
            public const int MaxSystemWatcherCodeLength = 50;
            public const long DefaultSystemWatcherId = 1L;
            public const string DefaultSystemWatcherGuidString = "00000000-0000-0000-0000-000000000001";
            public static readonly Guid DefaultSystemWatcherGuid = new Guid(DefaultSystemWatcherGuidString);
            public const string DefaultSystemWatcherCode = "default";
        }

        public class PredefinedCurrency
        {
            public PredefinedCurrency(long id, string code)
            {
                this.Id = id;
                this.Code = code;
            }

            public long Id { get; }
            public string Code { get; set; }
        }

        public static class Currency
        {
            public const int MaxCurrencyNameLength = 50;

            public static IReadOnlyDictionary<long, PredefinedCurrency> PredefinedCurrenciesById { get; }
            public static IReadOnlyDictionary<string, PredefinedCurrency> PredefinedCurrenciesByCode { get; }

            static Currency()
            {
                var predefinedCurrencies = GeneratePredefinedCurrencies();
                PredefinedCurrenciesById = predefinedCurrencies.ToDictionary(x => x.Id, x => x);
                PredefinedCurrenciesByCode = predefinedCurrencies.ToDictionary(x => x.Code, x => x);
            }

            public static bool IsPredefinedCurrencyId(long id) => PredefinedCurrenciesById.ContainsKey(id);
            public static bool IsPredefinedCurrencyCode(string code) => PredefinedCurrenciesByCode.ContainsKey(code);

            public const long SystemBasicCurrencyId = UsdId;
            public const string SystemBasicCurrencyCode = UsdCode;

            #region Currency Ids and Codes

            public const long UsdId = 9001L;
            public const string UsdGuidString = "ce49c7a1-66f8-494e-b5cd-9b9b925637ee";
            public static readonly Guid UsdGuid = new Guid(UsdGuidString);
            public const string UsdCode = "USD";

            public const long EurId = 9002L;
            public const string EurGuidString = "fe781e20-5fd3-4ec6-9350-dd1859649e14";
            public static readonly Guid EurGuid = new Guid(EurGuidString);
            public const string EurCode = "EUR";

            public const long UahId = 9003L;
            public const string UahGuidString = "c419319b-dfa4-48dc-b42a-bb7d2c58ce5a";
            public static readonly Guid UahGuid = new Guid(UahGuidString);
            public const string UahCode = "UAH";

            public const long GbpId = 9004L;
            public const string GbpGuidString = "d9429552-9bfc-4a74-aa77-b8cde671ee53";
            public static readonly Guid GbpGuid = new Guid(GbpGuidString);
            public const string GbpCode = "GBP";

            public const long PlnId = 9005L;
            public const string PlnGuidString = "e82dd292-5bea-4e8b-8359-d373ae4cea2a";
            public static readonly Guid PlnGuid = new Guid(PlnGuidString);
            public const string PlnCode = "PLN";

            public const long CzkId = 9006L;
            public const string CzkGuidString = "58871d0e-d879-4f75-9aa5-bbebb01a43ff";
            public static readonly Guid CzkGuid = new Guid(CzkGuidString);
            public const string CzkCode = "CZK";

            public const long HufId = 9007L;
            public const string HufGuidString = "5e58fada-7a19-433d-b44e-761d0f56efc0";
            public static readonly Guid HufGuid = new Guid(HufGuidString);
            public const string HufCode = "HUF";

            public const long DkkId = 9008L;
            public const string DkkGuidString = "cc4ece2c-f7ba-408b-a263-5c7f522b4762";
            public static readonly Guid DkkGuid = new Guid(DkkGuidString);
            public const string DkkCode = "DKK";

            public const long NokId = 9009L;
            public const string NokGuidString = "b8a080f6-2cf3-48fa-b276-2b990f3456ca";
            public static readonly Guid NokGuid = new Guid(NokGuidString);
            public const string NokCode = "NOK";

            public const long SekId = 9010L;
            public const string SekGuidString = "6a2367e1-0238-4c25-8447-1637c6313e2d";
            public static readonly Guid SekGuid = new Guid(SekGuidString);
            public const string SekCode = "SEK";

            public const long MdlId = 9011L;
            public const string MdlGuidString = "adf9da21-7d73-435c-9601-778c49b8af5c";
            public static readonly Guid MdlGuid = new Guid(MdlGuidString);
            public const string MdlCode = "MDL";

            public const long GelId = 9012L;
            public const string GelGuidString = "7745b1f9-9e8e-4c73-ae35-dcba5378db8a";
            public static readonly Guid GelGuid = new Guid(GelGuidString);
            public const string GelCode = "GEL";

            public const long ChfId = 9013L;
            public const string ChfGuidString = "6573890e-8826-44ce-80bc-aa01656a7d2d";
            public static readonly Guid ChfGuid = new Guid(ChfGuidString);
            public const string ChfCode = "CHF";

            public const long HrkId = 9014L;
            public const string HrkGuidString = "25ba5f33-65cc-4726-b997-96b7088e9f9a";
            public static readonly Guid HrkGuid = new Guid(HrkGuidString);
            public const string HrkCode = "HRK";

            public const long BgnId = 9015L;
            public const string BgnGuidString = "26b7a5a6-0440-4f44-a901-cb2a883a42db";
            public static readonly Guid BgnGuid = new Guid(BgnGuidString);
            public const string BgnCode = "BGN";

            public const long BtcId = 9016L;
            public const string BtcGuidString = "cfa07608-ae52-4409-8e24-288287c2bb34";
            public static readonly Guid BtcGuid = new Guid(BtcGuidString);
            public const string BtcCode = "BTC";

            public const long CadId = 9017L;
            public const string CadGuidString = "b3669345-61b6-4400-8c25-2a254ffbf5db";
            public static readonly Guid CadGuid = new Guid(CadGuidString);
            public const string CadCode = "CAD";

            public const long AudId = 9018L;
            public const string AudGuidString = "47872004-6de2-4efb-af2f-5c6738d90193";
            public static readonly Guid AudGuid = new Guid(AudGuidString);
            public const string AudCode = "AUD";

            public const long IlsId = 9019L;
            public const string IlsGuidString = "3a29f288-7bfa-4a2f-9c38-29c61fe15a29";
            public static readonly Guid IlsGuid = new Guid(IlsGuidString);
            public const string IlsCode = "ILS";

            public const long AedId = 9020L;
            public const string AedGuidString = "cd26c24a-a58d-40ac-bd96-1eb130c68b99";
            public static readonly Guid AedGuid = new Guid(AedGuidString);
            public const string AedCode = "AED";

            public const long AfnId = 9021L;
            public const string AfnGuidString = "f68dbcf3-9a1e-4123-bfa2-6effa58d2f1d";
            public static readonly Guid AfnGuid = new Guid(AfnGuidString);
            public const string AfnCode = "AFN";

            public const long AllId = 9022L;
            public const string AllGuidString = "63dbb6b2-352f-4d12-8246-ea6c8e278640";
            public static readonly Guid AllGuid = new Guid(AllGuidString);
            public const string AllCode = "ALL";

            public const long AmdId = 9023L;
            public const string AmdGuidString = "8dc440d0-1ffe-4730-bfb9-19ef707f0733";
            public static readonly Guid AmdGuid = new Guid(AmdGuidString);
            public const string AmdCode = "AMD";

            public const long AngId = 9024L;
            public const string AngGuidString = "5a3e4418-3bfe-4ef5-8d12-34477e2f7670";
            public static readonly Guid AngGuid = new Guid(AngGuidString);
            public const string AngCode = "ANG";

            public const long AoaId = 9025L;
            public const string AoaGuidString = "8acd8f3d-28ce-4682-ab5f-1d5c35b76050";
            public static readonly Guid AoaGuid = new Guid(AoaGuidString);
            public const string AoaCode = "AOA";

            public const long ArsId = 9026L;
            public const string ArsGuidString = "7274f0b6-4028-4663-a5b1-3b2082750011";
            public static readonly Guid ArsGuid = new Guid(ArsGuidString);
            public const string ArsCode = "ARS";

            public const long AwgId = 9027L;
            public const string AwgGuidString = "5199f072-bb82-4125-b9a2-08077fe53cc3";
            public static readonly Guid AwgGuid = new Guid(AwgGuidString);
            public const string AwgCode = "AWG";

            public const long AznId = 9028L;
            public const string AznGuidString = "6e0e2a91-239e-42fe-8c83-d2509aa5dd37";
            public static readonly Guid AznGuid = new Guid(AznGuidString);
            public const string AznCode = "AZN";

            public const long BamId = 9029L;
            public const string BamGuidString = "1806289c-5b5e-4ddf-b490-0daa73f79c6a";
            public static readonly Guid BamGuid = new Guid(BamGuidString);
            public const string BamCode = "BAM";

            public const long BbdId = 9030L;
            public const string BbdGuidString = "0764b605-8c67-425b-a59c-a70a3d14ba49";
            public static readonly Guid BbdGuid = new Guid(BbdGuidString);
            public const string BbdCode = "BBD";

            public const long BdtId = 9031L;
            public const string BdtGuidString = "38919c74-b391-4274-92f2-fe7d03ae1051";
            public static readonly Guid BdtGuid = new Guid(BdtGuidString);
            public const string BdtCode = "BDT";

            public const long BhdId = 9032L;
            public const string BhdGuidString = "842d94c2-d17b-40ca-8370-43803987a9c3";
            public static readonly Guid BhdGuid = new Guid(BhdGuidString);
            public const string BhdCode = "BHD";

            public const long BifId = 9033L;
            public const string BifGuidString = "643668a2-9c27-49ed-85d2-02bf70901efd";
            public static readonly Guid BifGuid = new Guid(BifGuidString);
            public const string BifCode = "BIF";

            public const long BmdId = 9034L;
            public const string BmdGuidString = "41307736-2d57-4de4-afc4-f536de13d4e1";
            public static readonly Guid BmdGuid = new Guid(BmdGuidString);
            public const string BmdCode = "BMD";

            public const long BndId = 9035L;
            public const string BndGuidString = "de386f6e-85d9-48c4-992f-66f90e91a2d2";
            public static readonly Guid BndGuid = new Guid(BndGuidString);
            public const string BndCode = "BND";

            public const long BobId = 9036L;
            public const string BobGuidString = "8fee4805-103f-4ac3-bee4-a9b88a386546";
            public static readonly Guid BobGuid = new Guid(BobGuidString);
            public const string BobCode = "BOB";

            public const long BrlId = 9037L;
            public const string BrlGuidString = "e04ef03c-640f-43cd-9551-e5d6d771630e";
            public static readonly Guid BrlGuid = new Guid(BrlGuidString);
            public const string BrlCode = "BRL";

            public const long BsdId = 9038L;
            public const string BsdGuidString = "a0237b90-05d7-418e-ba65-106db3dc1362";
            public static readonly Guid BsdGuid = new Guid(BsdGuidString);
            public const string BsdCode = "BSD";

            public const long BtnId = 9039L;
            public const string BtnGuidString = "65ec871c-5c2f-4c03-ba42-a3a8e5fd8a43";
            public static readonly Guid BtnGuid = new Guid(BtnGuidString);
            public const string BtnCode = "BTN";

            public const long BwpId = 9040L;
            public const string BwpGuidString = "62e729a1-1e16-442b-8e9b-b4bc0d4ba9d6";
            public static readonly Guid BwpGuid = new Guid(BwpGuidString);
            public const string BwpCode = "BWP";

            public const long BynId = 9041L;
            public const string BynGuidString = "23c9ac67-3686-42af-b619-2e7181cf9c5f";
            public static readonly Guid BynGuid = new Guid(BynGuidString);
            public const string BynCode = "BYN";

            public const long BzdId = 9042L;
            public const string BzdGuidString = "437faaa3-1fe3-4ac8-a61c-220b5bee11bc";
            public static readonly Guid BzdGuid = new Guid(BzdGuidString);
            public const string BzdCode = "BZD";

            public const long CdfId = 9043L;
            public const string CdfGuidString = "f27490b8-1052-413c-bfa5-03a563a72738";
            public static readonly Guid CdfGuid = new Guid(CdfGuidString);
            public const string CdfCode = "CDF";

            public const long ClfId = 9044L;
            public const string ClfGuidString = "3dfd219f-5054-4cc9-9b83-b10dffddd3fe";
            public static readonly Guid ClfGuid = new Guid(ClfGuidString);
            public const string ClfCode = "CLF";

            public const long ClpId = 9045L;
            public const string ClpGuidString = "4952498c-5406-4f19-be59-c7be7aed87cc";
            public static readonly Guid ClpGuid = new Guid(ClpGuidString);
            public const string ClpCode = "CLP";

            public const long CnyId = 9046L;
            public const string CnyGuidString = "de56c9ec-b8d7-4f97-9201-fdcd5f8543b2";
            public static readonly Guid CnyGuid = new Guid(CnyGuidString);
            public const string CnyCode = "CNY";

            public const long CopId = 9047L;
            public const string CopGuidString = "d682fe03-83c6-4594-a151-59aa07550c18";
            public static readonly Guid CopGuid = new Guid(CopGuidString);
            public const string CopCode = "COP";

            public const long CrcId = 9048L;
            public const string CrcGuidString = "8f2a92f9-9f52-4d2e-9065-e9e7e93920a0";
            public static readonly Guid CrcGuid = new Guid(CrcGuidString);
            public const string CrcCode = "CRC";

            public const long CucId = 9049L;
            public const string CucGuidString = "4776da43-b890-4dc9-a0a0-8004d9fdee77";
            public static readonly Guid CucGuid = new Guid(CucGuidString);
            public const string CucCode = "CUC";

            public const long CupId = 9050L;
            public const string CupGuidString = "f7fcf685-ee71-44de-8d74-313337f32f3d";
            public static readonly Guid CupGuid = new Guid(CupGuidString);
            public const string CupCode = "CUP";

            public const long CveId = 9051L;
            public const string CveGuidString = "fbe3f5d9-b8b0-4ffd-b628-5d81c9927d40";
            public static readonly Guid CveGuid = new Guid(CveGuidString);
            public const string CveCode = "CVE";

            public const long DjfId = 9052L;
            public const string DjfGuidString = "fcff9c2e-0763-4835-a969-de541393ae70";
            public static readonly Guid DjfGuid = new Guid(DjfGuidString);
            public const string DjfCode = "DJF";

            public const long DopId = 9053L;
            public const string DopGuidString = "92249a19-6f01-40f4-b0fb-a9274466fc25";
            public static readonly Guid DopGuid = new Guid(DopGuidString);
            public const string DopCode = "DOP";

            public const long DzdId = 9054L;
            public const string DzdGuidString = "c6fc94c1-3aeb-4024-ada3-f2d76cd6925d";
            public static readonly Guid DzdGuid = new Guid(DzdGuidString);
            public const string DzdCode = "DZD";

            public const long EgpId = 9055L;
            public const string EgpGuidString = "76b95258-754f-4396-920d-de26d25ef24c";
            public static readonly Guid EgpGuid = new Guid(EgpGuidString);
            public const string EgpCode = "EGP";

            public const long ErnId = 9056L;
            public const string ErnGuidString = "a3da47ac-e67a-4cae-ac0d-1c5bc1677e29";
            public static readonly Guid ErnGuid = new Guid(ErnGuidString);
            public const string ErnCode = "ERN";

            public const long EtbId = 9057L;
            public const string EtbGuidString = "dfcf8e05-36af-4fa7-b590-24c051c0505f";
            public static readonly Guid EtbGuid = new Guid(EtbGuidString);
            public const string EtbCode = "ETB";

            public const long FjdId = 9058L;
            public const string FjdGuidString = "248bca82-1804-4c51-babf-cc0fe2f9028d";
            public static readonly Guid FjdGuid = new Guid(FjdGuidString);
            public const string FjdCode = "FJD";

            public const long FkpId = 9059L;
            public const string FkpGuidString = "79f79f55-931b-4566-ab1b-703b65afd30f";
            public static readonly Guid FkpGuid = new Guid(FkpGuidString);
            public const string FkpCode = "FKP";

            public const long GgpId = 9060L;
            public const string GgpGuidString = "ee72a220-3a03-409e-a1e3-4dd986a0282d";
            public static readonly Guid GgpGuid = new Guid(GgpGuidString);
            public const string GgpCode = "GGP";

            public const long GhsId = 9061L;
            public const string GhsGuidString = "f4250425-e99f-45b7-a290-3f9ad0c24105";
            public static readonly Guid GhsGuid = new Guid(GhsGuidString);
            public const string GhsCode = "GHS";

            public const long GipId = 9062L;
            public const string GipGuidString = "b96c5f98-fcee-40fa-b9e7-ea2ccc049706";
            public static readonly Guid GipGuid = new Guid(GipGuidString);
            public const string GipCode = "GIP";

            public const long GmdId = 9063L;
            public const string GmdGuidString = "6a1aa697-4fa3-4144-9a8c-6a1ab24590df";
            public static readonly Guid GmdGuid = new Guid(GmdGuidString);
            public const string GmdCode = "GMD";

            public const long GnfId = 9064L;
            public const string GnfGuidString = "df705ba9-2ee0-4c55-a4cb-653d8e49e7fd";
            public static readonly Guid GnfGuid = new Guid(GnfGuidString);
            public const string GnfCode = "GNF";

            public const long GtqId = 9065L;
            public const string GtqGuidString = "a4305b0f-da19-477f-8ae3-d44e771d932b";
            public static readonly Guid GtqGuid = new Guid(GtqGuidString);
            public const string GtqCode = "GTQ";

            public const long GydId = 9066L;
            public const string GydGuidString = "e3829579-a5ab-43c6-985b-cbb17034b1b8";
            public static readonly Guid GydGuid = new Guid(GydGuidString);
            public const string GydCode = "GYD";

            public const long HkdId = 9067L;
            public const string HkdGuidString = "67ab99d0-1097-43b8-a5e4-fbed29062f14";
            public static readonly Guid HkdGuid = new Guid(HkdGuidString);
            public const string HkdCode = "HKD";

            public const long HnlId = 9068L;
            public const string HnlGuidString = "7df53103-ac2b-481e-8ccb-b8421e035db3";
            public static readonly Guid HnlGuid = new Guid(HnlGuidString);
            public const string HnlCode = "HNL";

            public const long HtgId = 9069L;
            public const string HtgGuidString = "aa15ea7d-297b-4ae6-b93b-4c21bc34efb7";
            public static readonly Guid HtgGuid = new Guid(HtgGuidString);
            public const string HtgCode = "HTG";

            public const long IdrId = 9070L;
            public const string IdrGuidString = "082555e8-b6d0-4fde-a11d-9c378a11fea7";
            public static readonly Guid IdrGuid = new Guid(IdrGuidString);
            public const string IdrCode = "IDR";

            public const long ImpId = 9071L;
            public const string ImpGuidString = "b9f92b43-1834-4eb8-8268-63124e776b6b";
            public static readonly Guid ImpGuid = new Guid(ImpGuidString);
            public const string ImpCode = "IMP";

            public const long InrId = 9072L;
            public const string InrGuidString = "6a3ac916-60ee-4247-b82b-513cd1ce3479";
            public static readonly Guid InrGuid = new Guid(InrGuidString);
            public const string InrCode = "INR";

            public const long IqdId = 9073L;
            public const string IqdGuidString = "2c553f9e-a2db-4888-a0e2-e0e041bb8bd6";
            public static readonly Guid IqdGuid = new Guid(IqdGuidString);
            public const string IqdCode = "IQD";

            public const long IrrId = 9074L;
            public const string IrrGuidString = "8d450f88-c113-4d99-8f28-58e0801df036";
            public static readonly Guid IrrGuid = new Guid(IrrGuidString);
            public const string IrrCode = "IRR";

            public const long IskId = 9075L;
            public const string IskGuidString = "27715473-7222-4845-b756-15de99d39ee0";
            public static readonly Guid IskGuid = new Guid(IskGuidString);
            public const string IskCode = "ISK";

            public const long JepId = 9076L;
            public const string JepGuidString = "b8503525-69b7-4937-8947-10841857d0dd";
            public static readonly Guid JepGuid = new Guid(JepGuidString);
            public const string JepCode = "JEP";

            public const long JmdId = 9077L;
            public const string JmdGuidString = "18a29030-5117-4cf3-ba03-56c26f41f084";
            public static readonly Guid JmdGuid = new Guid(JmdGuidString);
            public const string JmdCode = "JMD";

            public const long JodId = 9078L;
            public const string JodGuidString = "5983414b-5bb0-4815-abf6-74c843166da0";
            public static readonly Guid JodGuid = new Guid(JodGuidString);
            public const string JodCode = "JOD";

            public const long JpyId = 9079L;
            public const string JpyGuidString = "3467d4f0-b292-48be-b1d5-1ecb6fd157ca";
            public static readonly Guid JpyGuid = new Guid(JpyGuidString);
            public const string JpyCode = "JPY";

            public const long KesId = 9080L;
            public const string KesGuidString = "82dec336-c291-40a7-85a1-afafc028ec92";
            public static readonly Guid KesGuid = new Guid(KesGuidString);
            public const string KesCode = "KES";

            public const long KgsId = 9081L;
            public const string KgsGuidString = "456f8ecb-29bb-4dbc-abcb-62a64a94c49d";
            public static readonly Guid KgsGuid = new Guid(KgsGuidString);
            public const string KgsCode = "KGS";

            public const long KhrId = 9082L;
            public const string KhrGuidString = "4e0a1974-3927-4884-af9e-a4f018a17304";
            public static readonly Guid KhrGuid = new Guid(KhrGuidString);
            public const string KhrCode = "KHR";

            public const long KmfId = 9083L;
            public const string KmfGuidString = "acbc8eb6-790d-4c6e-8f8e-179282ecca08";
            public static readonly Guid KmfGuid = new Guid(KmfGuidString);
            public const string KmfCode = "KMF";

            public const long KpwId = 9084L;
            public const string KpwGuidString = "47b46c6d-b981-4994-8867-d92331897273";
            public static readonly Guid KpwGuid = new Guid(KpwGuidString);
            public const string KpwCode = "KPW";

            public const long KrwId = 9085L;
            public const string KrwGuidString = "c404b4e1-fc2b-4cc4-881f-9ac1d4d97625";
            public static readonly Guid KrwGuid = new Guid(KrwGuidString);
            public const string KrwCode = "KRW";

            public const long KwdId = 9086L;
            public const string KwdGuidString = "3a42424c-e52b-4622-b404-4850c203c4af";
            public static readonly Guid KwdGuid = new Guid(KwdGuidString);
            public const string KwdCode = "KWD";

            public const long KydId = 9087L;
            public const string KydGuidString = "2f47bded-e36d-4d90-a0d4-a3b26b92a3a3";
            public static readonly Guid KydGuid = new Guid(KydGuidString);
            public const string KydCode = "KYD";

            public const long KztId = 9088L;
            public const string KztGuidString = "f979aaf6-2bf0-4553-b1bb-567323d88722";
            public static readonly Guid KztGuid = new Guid(KztGuidString);
            public const string KztCode = "KZT";

            public const long LakId = 9089L;
            public const string LakGuidString = "65ccc113-2a5c-4ee9-adca-9f92fb0a4fc5";
            public static readonly Guid LakGuid = new Guid(LakGuidString);
            public const string LakCode = "LAK";

            public const long LbpId = 9090L;
            public const string LbpGuidString = "1533db2f-1c3b-4357-a2a4-4f98f63bbfc2";
            public static readonly Guid LbpGuid = new Guid(LbpGuidString);
            public const string LbpCode = "LBP";

            public const long LkrId = 9091L;
            public const string LkrGuidString = "568380a4-8fed-48af-b7d8-c09ee06c123d";
            public static readonly Guid LkrGuid = new Guid(LkrGuidString);
            public const string LkrCode = "LKR";

            public const long LrdId = 9092L;
            public const string LrdGuidString = "ae32e046-7f28-40f9-86b1-516e529aa726";
            public static readonly Guid LrdGuid = new Guid(LrdGuidString);
            public const string LrdCode = "LRD";

            public const long LslId = 9093L;
            public const string LslGuidString = "68afa7e6-fd4b-469c-b7db-3e5f2bb4ce4b";
            public static readonly Guid LslGuid = new Guid(LslGuidString);
            public const string LslCode = "LSL";

            public const long LydId = 9094L;
            public const string LydGuidString = "e5432ba3-222b-4289-bc2c-a560ea8e7fc5";
            public static readonly Guid LydGuid = new Guid(LydGuidString);
            public const string LydCode = "LYD";

            public const long MadId = 9095L;
            public const string MadGuidString = "ad983bf2-1b13-419a-8a28-27e2f486b3ee";
            public static readonly Guid MadGuid = new Guid(MadGuidString);
            public const string MadCode = "MAD";

            public const long MgaId = 9096L;
            public const string MgaGuidString = "766dbe28-b971-45d7-93be-f9d4d23bf53a";
            public static readonly Guid MgaGuid = new Guid(MgaGuidString);
            public const string MgaCode = "MGA";

            public const long MkdId = 9097L;
            public const string MkdGuidString = "31d7f606-d866-445c-8ef6-d496755a7264";
            public static readonly Guid MkdGuid = new Guid(MkdGuidString);
            public const string MkdCode = "MKD";

            public const long MmkId = 9098L;
            public const string MmkGuidString = "9a9dc951-0419-4463-a2fd-2a43f03b30c0";
            public static readonly Guid MmkGuid = new Guid(MmkGuidString);
            public const string MmkCode = "MMK";

            public const long MntId = 9099L;
            public const string MntGuidString = "18d8f431-0f25-4745-80e5-2f4da2f7fe84";
            public static readonly Guid MntGuid = new Guid(MntGuidString);
            public const string MntCode = "MNT";

            public const long MopId = 9100L;
            public const string MopGuidString = "b6286a57-6def-4934-9add-e558ae1f1746";
            public static readonly Guid MopGuid = new Guid(MopGuidString);
            public const string MopCode = "MOP";

            public const long MroId = 9101L;
            public const string MroGuidString = "ad8767db-520f-4ce6-a6cf-06c9c0093910";
            public static readonly Guid MroGuid = new Guid(MroGuidString);
            public const string MroCode = "MRO";

            public const long MurId = 9102L;
            public const string MurGuidString = "1778d69f-c611-48e7-a15c-f93628f9c250";
            public static readonly Guid MurGuid = new Guid(MurGuidString);
            public const string MurCode = "MUR";

            public const long MvrId = 9103L;
            public const string MvrGuidString = "413f442b-0dab-4bf7-a55e-83686478c535";
            public static readonly Guid MvrGuid = new Guid(MvrGuidString);
            public const string MvrCode = "MVR";

            public const long MwkId = 9104L;
            public const string MwkGuidString = "a1d32204-149c-439c-a57a-eeaaf3fd3eee";
            public static readonly Guid MwkGuid = new Guid(MwkGuidString);
            public const string MwkCode = "MWK";

            public const long MxnId = 9105L;
            public const string MxnGuidString = "673692e9-7576-48df-a184-5c1d96467dbd";
            public static readonly Guid MxnGuid = new Guid(MxnGuidString);
            public const string MxnCode = "MXN";

            public const long MyrId = 9106L;
            public const string MyrGuidString = "bf9538ce-5045-421a-b499-1f52331c6513";
            public static readonly Guid MyrGuid = new Guid(MyrGuidString);
            public const string MyrCode = "MYR";

            public const long MznId = 9107L;
            public const string MznGuidString = "fe59e1a4-0e96-4d6c-b4d8-a4f3edfa15bd";
            public static readonly Guid MznGuid = new Guid(MznGuidString);
            public const string MznCode = "MZN";

            public const long NadId = 9108L;
            public const string NadGuidString = "3e12b1dc-b5a6-4bbc-8fba-818df74b941c";
            public static readonly Guid NadGuid = new Guid(NadGuidString);
            public const string NadCode = "NAD";

            public const long NgnId = 9109L;
            public const string NgnGuidString = "0ca9a4af-01fc-4cee-8f88-bf3f56bd363a";
            public static readonly Guid NgnGuid = new Guid(NgnGuidString);
            public const string NgnCode = "NGN";

            public const long NioId = 9110L;
            public const string NioGuidString = "d5ebc8e3-5ee1-44e4-9d02-5fb8bc314175";
            public static readonly Guid NioGuid = new Guid(NioGuidString);
            public const string NioCode = "NIO";

            public const long NprId = 9111L;
            public const string NprGuidString = "dcece14a-89af-4f22-9cad-599dedd21aad";
            public static readonly Guid NprGuid = new Guid(NprGuidString);
            public const string NprCode = "NPR";

            public const long NzdId = 9112L;
            public const string NzdGuidString = "1bb72e35-8038-4886-a1f4-9d36e8962295";
            public static readonly Guid NzdGuid = new Guid(NzdGuidString);
            public const string NzdCode = "NZD";

            public const long OmrId = 9113L;
            public const string OmrGuidString = "8aa2aa6a-d320-4bd1-a58c-8d9e066fc837";
            public static readonly Guid OmrGuid = new Guid(OmrGuidString);
            public const string OmrCode = "OMR";

            public const long PabId = 9114L;
            public const string PabGuidString = "e360c090-b8a6-4de1-baa0-d1de5c03e418";
            public static readonly Guid PabGuid = new Guid(PabGuidString);
            public const string PabCode = "PAB";

            public const long PenId = 9115L;
            public const string PenGuidString = "8fb1ad14-883e-4374-a919-16cb5e854cb1";
            public static readonly Guid PenGuid = new Guid(PenGuidString);
            public const string PenCode = "PEN";

            public const long PgkId = 9116L;
            public const string PgkGuidString = "21a6ddae-1a7a-48d6-9e4e-5b064871b2b6";
            public static readonly Guid PgkGuid = new Guid(PgkGuidString);
            public const string PgkCode = "PGK";

            public const long PhpId = 9117L;
            public const string PhpGuidString = "91846023-0332-44aa-882d-a352ac3abe3f";
            public static readonly Guid PhpGuid = new Guid(PhpGuidString);
            public const string PhpCode = "PHP";

            public const long PkrId = 9118L;
            public const string PkrGuidString = "e854f050-22f0-45a9-aaa9-3736f5267814";
            public static readonly Guid PkrGuid = new Guid(PkrGuidString);
            public const string PkrCode = "PKR";

            public const long PygId = 9119L;
            public const string PygGuidString = "55640596-f282-4289-b009-1d8ea462e5b8";
            public static readonly Guid PygGuid = new Guid(PygGuidString);
            public const string PygCode = "PYG";

            public const long QarId = 9120L;
            public const string QarGuidString = "102f6d4f-6ddd-433e-ad31-e6b514b61755";
            public static readonly Guid QarGuid = new Guid(QarGuidString);
            public const string QarCode = "QAR";

            public const long RonId = 9121L;
            public const string RonGuidString = "2fa2e82c-08ff-41ee-b232-4f3b22bfd3a8";
            public static readonly Guid RonGuid = new Guid(RonGuidString);
            public const string RonCode = "RON";

            public const long RsdId = 9122L;
            public const string RsdGuidString = "f384f3d5-4914-434c-8977-4daeeee93c34";
            public static readonly Guid RsdGuid = new Guid(RsdGuidString);
            public const string RsdCode = "RSD";

            public const long RubId = 9123L;
            public const string RubGuidString = "1c345052-7c12-46a4-ac49-76b33a079cca";
            public static readonly Guid RubGuid = new Guid(RubGuidString);
            public const string RubCode = "RUB";

            public const long RwfId = 9124L;
            public const string RwfGuidString = "c0d2f227-7a76-437a-85cb-baa8986011fa";
            public static readonly Guid RwfGuid = new Guid(RwfGuidString);
            public const string RwfCode = "RWF";

            public const long SarId = 9125L;
            public const string SarGuidString = "c367a634-3c6f-4645-ab6a-8d52c3949f6a";
            public static readonly Guid SarGuid = new Guid(SarGuidString);
            public const string SarCode = "SAR";

            public const long SbdId = 9126L;
            public const string SbdGuidString = "eeee5643-c492-49ff-9dae-ba10a9758424";
            public static readonly Guid SbdGuid = new Guid(SbdGuidString);
            public const string SbdCode = "SBD";

            public const long ScrId = 9127L;
            public const string ScrGuidString = "45ceee1a-d8b3-430d-84b2-fb0434a5a2ce";
            public static readonly Guid ScrGuid = new Guid(ScrGuidString);
            public const string ScrCode = "SCR";

            public const long SdgId = 9128L;
            public const string SdgGuidString = "6e58ca58-37f6-471b-ab62-8a52d99fd551";
            public static readonly Guid SdgGuid = new Guid(SdgGuidString);
            public const string SdgCode = "SDG";

            public const long SgdId = 9129L;
            public const string SgdGuidString = "a6a768cc-dd36-4be4-832e-ea693e17d86c";
            public static readonly Guid SgdGuid = new Guid(SgdGuidString);
            public const string SgdCode = "SGD";

            public const long ShpId = 9130L;
            public const string ShpGuidString = "9722c7f2-cd5c-47b1-8659-58fcc2634ddb";
            public static readonly Guid ShpGuid = new Guid(ShpGuidString);
            public const string ShpCode = "SHP";

            public const long SllId = 9131L;
            public const string SllGuidString = "c599a662-258d-41b1-8f8e-e97ee3844958";
            public static readonly Guid SllGuid = new Guid(SllGuidString);
            public const string SllCode = "SLL";

            public const long SosId = 9132L;
            public const string SosGuidString = "7a1f5672-e687-470b-9d32-127d16f92eb3";
            public static readonly Guid SosGuid = new Guid(SosGuidString);
            public const string SosCode = "SOS";

            public const long SrdId = 9133L;
            public const string SrdGuidString = "c95fcebe-dbdf-4c46-8908-f5023ccaa0e6";
            public static readonly Guid SrdGuid = new Guid(SrdGuidString);
            public const string SrdCode = "SRD";

            public const long StdId = 9134L;
            public const string StdGuidString = "2e5e359e-f383-4ea9-9b79-81a6f45d5152";
            public static readonly Guid StdGuid = new Guid(StdGuidString);
            public const string StdCode = "STD";

            public const long SvcId = 9135L;
            public const string SvcGuidString = "e0d7efd9-de2f-421c-8ac8-6352109a7619";
            public static readonly Guid SvcGuid = new Guid(SvcGuidString);
            public const string SvcCode = "SVC";

            public const long SypId = 9136L;
            public const string SypGuidString = "9868a4d8-4e2c-4ec5-9f67-f7b30ab9f525";
            public static readonly Guid SypGuid = new Guid(SypGuidString);
            public const string SypCode = "SYP";

            public const long SzlId = 9137L;
            public const string SzlGuidString = "03049a64-ddbb-41e2-a7a8-b7fc4ab4ee75";
            public static readonly Guid SzlGuid = new Guid(SzlGuidString);
            public const string SzlCode = "SZL";

            public const long ThbId = 9138L;
            public const string ThbGuidString = "521187b7-8591-4cdd-aea3-ad9e41e74be4";
            public static readonly Guid ThbGuid = new Guid(ThbGuidString);
            public const string ThbCode = "THB";

            public const long TjsId = 9139L;
            public const string TjsGuidString = "4c221c8d-1fcd-4d39-acaa-86df0755d6ad";
            public static readonly Guid TjsGuid = new Guid(TjsGuidString);
            public const string TjsCode = "TJS";

            public const long TmtId = 9140L;
            public const string TmtGuidString = "8429c713-4a2a-4f07-ab98-3e7f0fb9fc0d";
            public static readonly Guid TmtGuid = new Guid(TmtGuidString);
            public const string TmtCode = "TMT";

            public const long TndId = 9141L;
            public const string TndGuidString = "0761d179-80ae-4bf3-9196-e82724cdc85a";
            public static readonly Guid TndGuid = new Guid(TndGuidString);
            public const string TndCode = "TND";

            public const long TopId = 9142L;
            public const string TopGuidString = "b122fb0d-9815-47f0-bb44-d9cdfe7f5d72";
            public static readonly Guid TopGuid = new Guid(TopGuidString);
            public const string TopCode = "TOP";

            public const long TryId = 9143L;
            public const string TryGuidString = "26f69c3d-0d6e-4f2e-ac1d-305e9c1492d5";
            public static readonly Guid TryGuid = new Guid(TryGuidString);
            public const string TryCode = "TRY";

            public const long TtdId = 9144L;
            public const string TtdGuidString = "2ad90dcd-afbd-4157-b488-15e6ac9db527";
            public static readonly Guid TtdGuid = new Guid(TtdGuidString);
            public const string TtdCode = "TTD";

            public const long TwdId = 9145L;
            public const string TwdGuidString = "59bc6fd9-92ec-410c-8670-27a1473ba75d";
            public static readonly Guid TwdGuid = new Guid(TwdGuidString);
            public const string TwdCode = "TWD";

            public const long TzsId = 9146L;
            public const string TzsGuidString = "0e47b3e1-ae91-4666-90d8-d609a7faa0b1";
            public static readonly Guid TzsGuid = new Guid(TzsGuidString);
            public const string TzsCode = "TZS";

            public const long UgxId = 9147L;
            public const string UgxGuidString = "6dfe0c94-b1c2-4e4f-a933-fdcd70280954";
            public static readonly Guid UgxGuid = new Guid(UgxGuidString);
            public const string UgxCode = "UGX";

            public const long UyuId = 9148L;
            public const string UyuGuidString = "e54c40b0-f7e6-493d-b8aa-5990fb133f23";
            public static readonly Guid UyuGuid = new Guid(UyuGuidString);
            public const string UyuCode = "UYU";

            public const long UzsId = 9149L;
            public const string UzsGuidString = "49d520ab-6cac-48c3-ac20-e56665709214";
            public static readonly Guid UzsGuid = new Guid(UzsGuidString);
            public const string UzsCode = "UZS";

            public const long VndId = 9150L;
            public const string VndGuidString = "8a108cd1-0b19-4b93-808e-3aca6e7e7db1";
            public static readonly Guid VndGuid = new Guid(VndGuidString);
            public const string VndCode = "VND";

            public const long VuvId = 9151L;
            public const string VuvGuidString = "85e73203-8e89-4441-a46a-9ff9c703335a";
            public static readonly Guid VuvGuid = new Guid(VuvGuidString);
            public const string VuvCode = "VUV";

            public const long WstId = 9152L;
            public const string WstGuidString = "178d9cb0-de1c-41d4-8005-afd176445328";
            public static readonly Guid WstGuid = new Guid(WstGuidString);
            public const string WstCode = "WST";

            public const long XafId = 9153L;
            public const string XafGuidString = "b6335fe2-c1bc-432c-932f-91f5b5062520";
            public static readonly Guid XafGuid = new Guid(XafGuidString);
            public const string XafCode = "XAF";

            public const long XagId = 9154L;
            public const string XagGuidString = "60862336-43f0-4083-a6f5-43ce7bddca5a";
            public static readonly Guid XagGuid = new Guid(XagGuidString);
            public const string XagCode = "XAG";

            public const long XauId = 9155L;
            public const string XauGuidString = "3f483e57-d9d5-4c63-8c08-e629f6776a67";
            public static readonly Guid XauGuid = new Guid(XauGuidString);
            public const string XauCode = "XAU";

            public const long XcdId = 9156L;
            public const string XcdGuidString = "423078bd-1f9f-4492-b4a5-f214ce2e5f63";
            public static readonly Guid XcdGuid = new Guid(XcdGuidString);
            public const string XcdCode = "XCD";

            public const long XdrId = 9157L;
            public const string XdrGuidString = "93fa948b-07a0-4627-bb85-3aa24111dd2a";
            public static readonly Guid XdrGuid = new Guid(XdrGuidString);
            public const string XdrCode = "XDR";

            public const long XofId = 9158L;
            public const string XofGuidString = "9baf8bea-7d6e-44d2-b6e9-0c49dd973277";
            public static readonly Guid XofGuid = new Guid(XofGuidString);
            public const string XofCode = "XOF";

            public const long XpfId = 9159L;
            public const string XpfGuidString = "776c42d2-5e88-4ac5-a4cb-7d4bcaff6192";
            public static readonly Guid XpfGuid = new Guid(XpfGuidString);
            public const string XpfCode = "XPF";

            public const long YerId = 9160L;
            public const string YerGuidString = "8ed58dcd-1f71-4ba5-a764-ae08e2ce8f18";
            public static readonly Guid YerGuid = new Guid(YerGuidString);
            public const string YerCode = "YER";

            public const long ZarId = 9161L;
            public const string ZarGuidString = "ecd4061c-90dd-4590-86ed-4cdf6c8d15fd";
            public static readonly Guid ZarGuid = new Guid(ZarGuidString);
            public const string ZarCode = "ZAR";

            public const long ZmwId = 9162L;
            public const string ZmwGuidString = "c9a29d3c-6e84-4b00-af94-b11ab6cfa305";
            public static readonly Guid ZmwGuid = new Guid(ZmwGuidString);
            public const string ZmwCode = "ZMW";

            public const long ZwlId = 9163L;
            public const string ZwlGuidString = "26b46018-25c5-4f25-863a-2c6b56a2fac9";
            public static readonly Guid ZwlGuid = new Guid(ZwlGuidString);
            public const string ZwlCode = "ZWL";

            #endregion

            private static IList<PredefinedCurrency> GeneratePredefinedCurrencies()
            {
                return new List<PredefinedCurrency>
                {
                    #region entries

                    new PredefinedCurrency(UsdId, UsdCode),
                    new PredefinedCurrency(EurId, EurCode),
                    new PredefinedCurrency(UahId, UahCode),
                    new PredefinedCurrency(GbpId, GbpCode),
                    new PredefinedCurrency(PlnId, PlnCode),
                    new PredefinedCurrency(CzkId, CzkCode),
                    new PredefinedCurrency(HufId, HufCode),
                    new PredefinedCurrency(DkkId, DkkCode),
                    new PredefinedCurrency(NokId, NokCode),
                    new PredefinedCurrency(SekId, SekCode),
                    new PredefinedCurrency(MdlId, MdlCode),
                    new PredefinedCurrency(GelId, GelCode),
                    new PredefinedCurrency(ChfId, ChfCode),
                    new PredefinedCurrency(HrkId, HrkCode),
                    new PredefinedCurrency(BgnId, BgnCode),
                    new PredefinedCurrency(BtcId, BtcCode),
                    new PredefinedCurrency(CadId, CadCode),
                    new PredefinedCurrency(AudId, AudCode),
                    new PredefinedCurrency(IlsId, IlsCode),
                    new PredefinedCurrency(AedId, AedCode),
                    new PredefinedCurrency(AfnId, AfnCode),
                    new PredefinedCurrency(AllId, AllCode),
                    new PredefinedCurrency(AmdId, AmdCode),
                    new PredefinedCurrency(AngId, AngCode),
                    new PredefinedCurrency(AoaId, AoaCode),
                    new PredefinedCurrency(ArsId, ArsCode),
                    new PredefinedCurrency(AwgId, AwgCode),
                    new PredefinedCurrency(AznId, AznCode),
                    new PredefinedCurrency(BamId, BamCode),
                    new PredefinedCurrency(BbdId, BbdCode),
                    new PredefinedCurrency(BdtId, BdtCode),
                    new PredefinedCurrency(BhdId, BhdCode),
                    new PredefinedCurrency(BifId, BifCode),
                    new PredefinedCurrency(BmdId, BmdCode),
                    new PredefinedCurrency(BndId, BndCode),
                    new PredefinedCurrency(BobId, BobCode),
                    new PredefinedCurrency(BrlId, BrlCode),
                    new PredefinedCurrency(BsdId, BsdCode),
                    new PredefinedCurrency(BtnId, BtnCode),
                    new PredefinedCurrency(BwpId, BwpCode),
                    new PredefinedCurrency(BynId, BynCode),
                    new PredefinedCurrency(BzdId, BzdCode),
                    new PredefinedCurrency(CdfId, CdfCode),
                    new PredefinedCurrency(ClfId, ClfCode),
                    new PredefinedCurrency(ClpId, ClpCode),
                    new PredefinedCurrency(CnyId, CnyCode),
                    new PredefinedCurrency(CopId, CopCode),
                    new PredefinedCurrency(CrcId, CrcCode),
                    new PredefinedCurrency(CucId, CucCode),
                    new PredefinedCurrency(CupId, CupCode),
                    new PredefinedCurrency(CveId, CveCode),
                    new PredefinedCurrency(DjfId, DjfCode),
                    new PredefinedCurrency(DopId, DopCode),
                    new PredefinedCurrency(DzdId, DzdCode),
                    new PredefinedCurrency(EgpId, EgpCode),
                    new PredefinedCurrency(ErnId, ErnCode),
                    new PredefinedCurrency(EtbId, EtbCode),
                    new PredefinedCurrency(FjdId, FjdCode),
                    new PredefinedCurrency(FkpId, FkpCode),
                    new PredefinedCurrency(GgpId, GgpCode),
                    new PredefinedCurrency(GhsId, GhsCode),
                    new PredefinedCurrency(GipId, GipCode),
                    new PredefinedCurrency(GmdId, GmdCode),
                    new PredefinedCurrency(GnfId, GnfCode),
                    new PredefinedCurrency(GtqId, GtqCode),
                    new PredefinedCurrency(GydId, GydCode),
                    new PredefinedCurrency(HkdId, HkdCode),
                    new PredefinedCurrency(HnlId, HnlCode),
                    new PredefinedCurrency(HtgId, HtgCode),
                    new PredefinedCurrency(IdrId, IdrCode),
                    new PredefinedCurrency(ImpId, ImpCode),
                    new PredefinedCurrency(InrId, InrCode),
                    new PredefinedCurrency(IqdId, IqdCode),
                    new PredefinedCurrency(IrrId, IrrCode),
                    new PredefinedCurrency(IskId, IskCode),
                    new PredefinedCurrency(JepId, JepCode),
                    new PredefinedCurrency(JmdId, JmdCode),
                    new PredefinedCurrency(JodId, JodCode),
                    new PredefinedCurrency(JpyId, JpyCode),
                    new PredefinedCurrency(KesId, KesCode),
                    new PredefinedCurrency(KgsId, KgsCode),
                    new PredefinedCurrency(KhrId, KhrCode),
                    new PredefinedCurrency(KmfId, KmfCode),
                    new PredefinedCurrency(KpwId, KpwCode),
                    new PredefinedCurrency(KrwId, KrwCode),
                    new PredefinedCurrency(KwdId, KwdCode),
                    new PredefinedCurrency(KydId, KydCode),
                    new PredefinedCurrency(KztId, KztCode),
                    new PredefinedCurrency(LakId, LakCode),
                    new PredefinedCurrency(LbpId, LbpCode),
                    new PredefinedCurrency(LkrId, LkrCode),
                    new PredefinedCurrency(LrdId, LrdCode),
                    new PredefinedCurrency(LslId, LslCode),
                    new PredefinedCurrency(LydId, LydCode),
                    new PredefinedCurrency(MadId, MadCode),
                    new PredefinedCurrency(MgaId, MgaCode),
                    new PredefinedCurrency(MkdId, MkdCode),
                    new PredefinedCurrency(MmkId, MmkCode),
                    new PredefinedCurrency(MntId, MntCode),
                    new PredefinedCurrency(MopId, MopCode),
                    new PredefinedCurrency(MroId, MroCode),
                    new PredefinedCurrency(MurId, MurCode),
                    new PredefinedCurrency(MvrId, MvrCode),
                    new PredefinedCurrency(MwkId, MwkCode),
                    new PredefinedCurrency(MxnId, MxnCode),
                    new PredefinedCurrency(MyrId, MyrCode),
                    new PredefinedCurrency(MznId, MznCode),
                    new PredefinedCurrency(NadId, NadCode),
                    new PredefinedCurrency(NgnId, NgnCode),
                    new PredefinedCurrency(NioId, NioCode),
                    new PredefinedCurrency(NprId, NprCode),
                    new PredefinedCurrency(NzdId, NzdCode),
                    new PredefinedCurrency(OmrId, OmrCode),
                    new PredefinedCurrency(PabId, PabCode),
                    new PredefinedCurrency(PenId, PenCode),
                    new PredefinedCurrency(PgkId, PgkCode),
                    new PredefinedCurrency(PhpId, PhpCode),
                    new PredefinedCurrency(PkrId, PkrCode),
                    new PredefinedCurrency(PygId, PygCode),
                    new PredefinedCurrency(QarId, QarCode),
                    new PredefinedCurrency(RonId, RonCode),
                    new PredefinedCurrency(RsdId, RsdCode),
                    new PredefinedCurrency(RubId, RubCode),
                    new PredefinedCurrency(RwfId, RwfCode),
                    new PredefinedCurrency(SarId, SarCode),
                    new PredefinedCurrency(SbdId, SbdCode),
                    new PredefinedCurrency(ScrId, ScrCode),
                    new PredefinedCurrency(SdgId, SdgCode),
                    new PredefinedCurrency(SgdId, SgdCode),
                    new PredefinedCurrency(ShpId, ShpCode),
                    new PredefinedCurrency(SllId, SllCode),
                    new PredefinedCurrency(SosId, SosCode),
                    new PredefinedCurrency(SrdId, SrdCode),
                    new PredefinedCurrency(StdId, StdCode),
                    new PredefinedCurrency(SvcId, SvcCode),
                    new PredefinedCurrency(SypId, SypCode),
                    new PredefinedCurrency(SzlId, SzlCode),
                    new PredefinedCurrency(ThbId, ThbCode),
                    new PredefinedCurrency(TjsId, TjsCode),
                    new PredefinedCurrency(TmtId, TmtCode),
                    new PredefinedCurrency(TndId, TndCode),
                    new PredefinedCurrency(TopId, TopCode),
                    new PredefinedCurrency(TryId, TryCode),
                    new PredefinedCurrency(TtdId, TtdCode),
                    new PredefinedCurrency(TwdId, TwdCode),
                    new PredefinedCurrency(TzsId, TzsCode),
                    new PredefinedCurrency(UgxId, UgxCode),
                    new PredefinedCurrency(UyuId, UyuCode),
                    new PredefinedCurrency(UzsId, UzsCode),
                    new PredefinedCurrency(VndId, VndCode),
                    new PredefinedCurrency(VuvId, VuvCode),
                    new PredefinedCurrency(WstId, WstCode),
                    new PredefinedCurrency(XafId, XafCode),
                    new PredefinedCurrency(XagId, XagCode),
                    new PredefinedCurrency(XauId, XauCode),
                    new PredefinedCurrency(XcdId, XcdCode),
                    new PredefinedCurrency(XdrId, XdrCode),
                    new PredefinedCurrency(XofId, XofCode),
                    new PredefinedCurrency(XpfId, XpfCode),
                    new PredefinedCurrency(YerId, YerCode),
                    new PredefinedCurrency(ZarId, ZarCode),
                    new PredefinedCurrency(ZmwId, ZmwCode),
                    new PredefinedCurrency(ZwlId, ZwlCode),


                    #endregion
                };
            }
        }
    }
}

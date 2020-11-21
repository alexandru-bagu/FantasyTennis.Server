using System;

using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
    /**
    * A class that provides Blowfish key encryption operations,
    * such as encoding data and generating keys.
    * All the algorithms herein are from Applied Cryptography
    * and implement a simplified cryptography interface.
    * 
    * Fantasy Tennis client uses custom Ps and S-boxes. Additionally BouncyCastle's 
    * implementation uses BIG ENDIAN while Fantasy Tennis uses LITTLE ENDIAN.
    */
    public sealed class FantasyTennisBlowfishEngine
        : IBlockCipher
    {
        private readonly static uint[] KP =
        {
            0xE7481E35, 0x95CF7307, 0x140512B9, 0x3476545E,
            0x49E6EED2, 0xEFBE3A7F, 0xC7FBD9D6, 0x21A87D67,
            0x3D20E8BA, 0x2AA08653, 0xDDEF206D, 0x931D11FE,
            0x8D1482F3, 0x3E1709B1, 0x8DD9B864 ,0x1CC3F818,
            0x4E1CC0A6, 0x84287C74
        },
        KS0 =
        {
            0x732A156E, 0x3671FBDA, 0x18BEAFA9, 0xF5DE8BF3,
            0xCC152020, 0x1EA8D42E, 0xB62A6244, 0x277B1CC4,
            0xBA560620, 0xD95B12C5, 0x85729FD4, 0x65EF0774,
            0x2255C970, 0xF227BB0B, 0x9E1A3816, 0x5BA53848,
            0x45B55BEC, 0xE3843A48, 0x636212E7, 0x8B672931,
            0x1A5A6B5B, 0x6DD52934, 0x4C5A7A5D, 0x2870ACD4,
            0x92FEB87D, 0x64F22786, 0xBD6C744B, 0xDD7EBF9D,
            0xF3435CA2, 0x0241A386, 0xD27009DA, 0xA45F5953,
            0xA3BB2431, 0xB3BCB199, 0x2C3C1897, 0x9103BB1F,
            0x7684D53E, 0x6B0C5555, 0xC533A502, 0xA88AC124,
            0x014A8A98, 0x6D0E550C, 0x4356AF9B, 0x2656B08D,
            0x696474D9, 0x25EC0BE3, 0x40D0F77B, 0xD41C8A1B,
            0x35DBB978, 0x7029325F, 0x9F10555B, 0xD96DD9B4,
            0x98003B55, 0xF1AEB8EE, 0xDE4A08A5, 0x8952817C,
            0xCB76E64B, 0x5FE10D85, 0xE096848B, 0xB0521258,
            0xD5470DC2, 0x55AEF124, 0x4474450F, 0xED831406,
            0xDBF229BE, 0xA29ECB68, 0xAB661C97, 0x75A05AAB,
            0xF47DB46B, 0x995F71A3, 0x15F702FC, 0xEA94D365,
            0xF8007933, 0xE3DC7CE3, 0x2653E31E, 0x308B5858,
            0x4FB95F4A, 0xCBC71685, 0xFB50F468, 0x3081F9BE,
            0x3F9FBABF, 0x13ADCC46, 0xFC040270, 0xB45755FB,
            0xC0E99F8E, 0xBC816153, 0xA650B9FC, 0x30D96028,
            0xCC272E2D, 0xE33313D5, 0xDEF38318, 0x93DB3DA5,
            0x2A4B681C, 0x823B770A, 0xC51BCB9C, 0x1FC006AC,
            0x44BFFB78, 0xCBA844C9, 0x80EF58CB, 0x2989A15C,
            0x71731689, 0x7436241D, 0x8F2A90D9, 0x776D0E4C,
            0x4CE82FD2, 0x07D77F4D, 0x981D55FC, 0x8E60B698,
            0x7D49E224, 0x31C95571, 0xBDCACB7E, 0xF92A3F77,
            0x8F72B724, 0x15220301, 0xE3F0B24C, 0x24745647,
            0x3C88F2E9, 0x19601B62, 0x0E1CAA87, 0x25D70419,
            0xBC836883, 0xB8FBB279, 0xA1368C12, 0x0FEF7C48,
            0x1DC1CA8E, 0xD177ADBC, 0x411539A3, 0x42E86B09,
            0x86957940, 0xF86D963E, 0x940CE4D6, 0x38904A72,
            0x15D75279, 0xC422E542, 0x1E7C6B39, 0x59E42918,
            0xA4750559, 0x2113DACB, 0x06641E5A, 0x4927050F,
            0x9E0359C7, 0x9E85C1AA, 0x6D70955F, 0x39671408,
            0xCF470B07, 0x409BCE11, 0xBF08018F, 0xB41816D9,
            0x344F9047, 0xD05DE21E, 0xFD617464, 0x749EA590,
            0xC7FDEFB2, 0x2ACC6672, 0x138F3B9B, 0xADDE8883,
            0x569B0C7C, 0x8FF68FBC, 0x21132547, 0x614F7DDC,
            0xCEE4F877, 0x757E3AC8, 0x52EA5BD9, 0x2C070FB1,
            0x8B1DC61F, 0x55B4B216, 0x2BA0AA3D, 0x194FDF0C,
            0x2C1A54A8, 0xFC9D065F, 0x56DCD7D9, 0x6D327EFD,
            0xDBD91D19, 0xDD0C5732, 0x77746BAD, 0x7A0BB130,
            0x290F0ECC, 0x5F2AA977, 0x7AFA075B, 0xEF95CDF4,
            0xD22FD00B, 0x2C89B189, 0x62503137, 0x27017FD1,
            0x16881D9A, 0x0237A842, 0x9D322757, 0x787B9C17,
            0x824C8749, 0x8349970B, 0xCEC9ABA8, 0x064477EA,
            0xC422D681, 0x896EAAEA, 0x233FD778, 0x8FBEAADA,
            0xFD354BD9, 0x6E7E8199, 0x21486B87, 0xC0FC6B6A,
            0x0CC7F5A2, 0x638BFC8C, 0xF4B59C1B, 0x81D25BA7,
            0x5F4187F9, 0xB9EF8F89, 0xC309E70B, 0xC5E65434,
            0xC63C1C54, 0xBC5F8DB5, 0xFBFC5F53, 0xDDBC3A5B,
            0x429B0F96, 0x767701A5, 0xA49CFB21, 0x47CF4C9E,
            0xD295CA1F, 0x89CD72EB, 0xAA4F6B67, 0xF9957246,
            0xC8459755, 0xF9033EAC, 0xB469E4EA, 0x3B9A9174,
            0x143CEA40, 0x01CE62A7, 0x72BAF152, 0xEC8AD5AE,
            0x9691398E, 0x5C94CF4F, 0xE925463A, 0xDB4187F4,
            0xEF7046AA, 0x9E6F38D2, 0x4B240742, 0x915C58CC,
            0xD3A8F24A, 0xFB43E2B3, 0xBD5F2599, 0xA64C35D3,
            0xD1418D81, 0xA04FF5CF, 0x31BF2319, 0x8B61934E,
            0x2D85A44B, 0x79B84B74, 0xAE78EC46, 0xDEDC4537,
            0xA816D3A0, 0x8AA23EF0, 0xA41A236D, 0xBB02C3D3,
            0x55781505, 0x3A347D20, 0xBCA36C2E, 0x0A2586BB
        },
        KS1 =
        {
            0x66281115, 0x2532597F, 0x2891460B, 0xCCBBCA71,
            0xFDA4EC1C, 0xE7079439, 0xEE6A54F7, 0x72ECEB6D,
            0x01849E20, 0xF75933B6, 0xC25731EE, 0xA820AD31,
            0x6203B9EE, 0x6E974DB2, 0x4EAABC7B, 0xA58E8DD2,
            0x748FBDB6, 0x56895BC6, 0x0375ECCC, 0x01D31490,
            0xBD5E6C0C, 0x80E00A79, 0xE8141FC5, 0xF67B2460,
            0xBFFB1282, 0xD0470653, 0x75C3660B, 0xC891CA7F,
            0x50575B37, 0x117050EA, 0xD0AB5A96, 0x7C341082,
            0x64552162, 0xBEA90B75, 0x2F72EBC3, 0x39A4C463,
            0x6060BEE7, 0x5B68CAD7, 0x9AC93162, 0x8F52D417,
            0x69F8D866, 0xBB5C6638, 0x4704B445, 0xCD6F1516,
            0xB543B4C8, 0x5BFEC708, 0x609CC852, 0xCE7F17F1,
            0x59190654, 0xA722B89C, 0xD94E5713, 0x47E77563,
            0x1C1ABE3A, 0xD200B7B4, 0xBFA3ACC2, 0x9B7C245A,
            0x433ADDA6, 0x24CD4494, 0x867E4FE2, 0x2A15C10D,
            0xE4D442CF, 0x46C7B08C, 0xDAB24A44, 0x1D9A680B,
            0xB9B7F70A, 0x9947728A, 0x71907F9E, 0x3D177C48,
            0x663708D0, 0x00C9EDAF, 0x56F4F59B, 0xBDC37932,
            0x533E4D5F, 0x320B4D5A, 0xC0592BE6, 0x0E1DC93D,
            0x775A3EB9, 0x8C0CCB36, 0x5D666340, 0xAAF00D73,
            0xA950403D, 0x5E2A8752, 0x22FE7A8C, 0x6B6DF189,
            0xF328F6B7, 0x3CA8CD2A, 0x9CD6AEDF, 0x55B2FDE6,
            0xDD43926D, 0x4F47F1B7, 0x43FBF916, 0x6AE0E1BD,
            0x3E612530, 0xA5CE1887, 0x456BD6DA, 0xF5ABCA16,
            0x0FBF6C6C, 0x7E1D07C1, 0xD89F1A3E, 0x6265ABDF,
            0xB99A253B, 0xA0BEF5C1, 0x0E9DBBC4, 0x85921781,
            0xE746D86D, 0x26F5E11E, 0x1B8B2FF4, 0x707A0469,
            0x513A3122, 0xCB5055BE, 0xAF3AA667, 0x42B4291B,
            0x8FA6C654, 0x44B2C06B, 0xC4B771DA, 0xF438C2C4,
            0x6EFCED66, 0x866DC659, 0xE8E0C03F, 0xAE6F6CC9,
            0x37070ABA, 0x1EC70ABD, 0x94EEFDC7, 0x13C5E655,
            0x04715FBA, 0x7794815E, 0x790897FA, 0x10B66F68,
            0x90615BEE, 0xB8BD441E, 0xCF4F5444, 0xB2E08EEE,
            0x067D6D87, 0x8758DE10, 0x29761F81, 0xF112E447,
            0x5182D3F1, 0x60B09B87, 0x3E4B5991, 0x025A005A,
            0xED5667E6, 0x635D5C24, 0x404AAC69, 0x229B2428,
            0x368E73F9, 0x24CF60E9, 0xAAAB5621, 0x6E19A356,
            0x3688FE29, 0x7C609CC5, 0x8E747B81, 0x31052943,
            0x7BF81F6F, 0x57600629, 0xE488F519, 0xAE948813,
            0xE1754852, 0x87AD6512, 0x62382948, 0x760F9042,
            0x650C9CF0, 0x913B241F, 0x41524AD3, 0x395C5C34,
            0x74CFBC18, 0x7EA8201C, 0x13B03872, 0x8C921FC4,
            0xBAE6184F, 0x2CCBF716, 0x95CB46DF, 0xC90F7655,
            0xF59EBAE8, 0x1DC55BE8, 0x7BC80FE8, 0x517A3ADE,
            0xC5F9A090, 0x47925FCD, 0xBF8B4301, 0x6262CD81,
            0x75BE0361, 0x66924972, 0xF642F849, 0x67C56E95,
            0xD48A2A6E, 0xC723DF7C, 0x1E7B7D2B, 0xC6A1813A,
            0x805FBAD6, 0x9D2ABEA6, 0xE8312362, 0xB2086865,
            0xB9B48B55, 0xD1A621C7, 0x155B948C, 0x7D29CFF4,
            0xAE05EC50, 0xCDC0B568, 0xB6FE213A, 0x60E7FDBD,
            0xCCE37FE7, 0xD259EDCD, 0x0BBD0E80, 0xD3E8A09A,
            0x95060588, 0x439B4D0D, 0x49E76887, 0xDC21247F,
            0xE8DAA77B, 0xF78B381C, 0x6D89D11B, 0xDA67FA87,
            0xD49054F0, 0x0F16C5DE, 0x0E7D5338, 0xD903F504,
            0xEA2E8519, 0x37A191B8, 0x2A792C22, 0x653E0C8D,
            0x8BA1922B, 0x861E079C, 0x77A3A1ED, 0xCE71B294,
            0x374A017E, 0x4592349A, 0x369C4E91, 0x8B982571,
            0xDD0FD68F, 0xC13499F6, 0x7B1373F9, 0xF5E1BCF1,
            0xB26AE59C, 0x9D6D7AAE, 0x08544B96, 0x2A3B3AEB,
            0x75009FAA, 0x20F0AB14, 0x13D8D167, 0x4C629BCD,
            0xC9A2E29A, 0x814F63D4, 0x1CD81C93, 0x60F7652A,
            0x7FEECE3A, 0x407D0E8E, 0x3CD6A472, 0x158FF94F,
            0xE9D68BD0, 0xF06E95DF, 0xF2349D1F, 0x933A62CD,
            0x2B2E2732, 0x0919B8F4, 0x78BFBC0A, 0x551E2691
        },
        KS2 =
        {
            0x0640D84D, 0xB592571A, 0x8E468F05, 0x6CFE136B,
            0x2EDFD5BA, 0x24154448, 0x4D1FCAD4, 0xD7D41424,
            0x956FA350, 0x5817943D, 0x77C097C2, 0xD457FB0B,
            0x407BE52F, 0x7C57EE7F, 0x454FE52A, 0xAB8FD989,
            0x92442C51, 0x2B6D5D7A, 0xB8AA3C8A, 0xFE68C62C,
            0x9E4E499E, 0x45DB9A04, 0x6C018817, 0x21BC34B8,
            0x7B7497F8, 0x3F9A65F4, 0x625EEA44, 0xDDEABEBD,
            0x0BF6D4CC, 0x722F4DB3, 0x55BA90DB, 0x50E0FE1E,
            0xD20AEAA3, 0x6BB50646, 0x088BF409, 0x2AAC56A5,
            0x48E843B1, 0xBD74B1E0, 0x95D4406C, 0x1112BF96,
            0x206313E4, 0x4CE9B8F8, 0xC1B48D28, 0xE08F21BB,
            0x1FECB0F7, 0xA3E093D1, 0x47FABEF1, 0x037B9B76,
            0xECB1DD02, 0xC0F99E8D, 0x2BB04C24, 0x400859CE,
            0x5D1E1F01, 0x66C06640, 0x87AE1649, 0x09DCDE05,
            0xC8F901F1, 0xEA397BF8, 0x63A9AF33, 0xCC205A9F,
            0x53EC735E, 0x0A70C059, 0xF7C4B482, 0xC70A76FB,
            0x45179265, 0x330F3822, 0x0D9F17BF, 0xD0F3A661,
            0x551DF7D7, 0x5863D941, 0x3F65ECE2, 0x2A69F78B,
            0x7AB70D3C, 0x4378DE65, 0xD163C5E8, 0xD4B6E2BE,
            0x3CC55AE8, 0xDE9F928C, 0x850FF561, 0xDDF698D6,
            0x03DBD10D, 0x8B062113, 0x5D9D6601, 0x2CABD554,
            0x65D22AC2, 0xED3FEAC4, 0xF810E8AC, 0x56C1B3F3,
            0xFB59A320, 0xBEDA51EE, 0x5EC7048D, 0xE92D6F34,
            0x30045EC6, 0x9BF00AE9, 0x4B0FC4A2, 0xC7ECC7ED,
            0x895B2973, 0xD331E830, 0x87B10F4A, 0xE6273FE1,
            0x816D508D, 0xBFF538EB, 0xB2846CD9, 0x2D307744,
            0x515EED38, 0x07530384, 0x137D5CA5, 0x3E1DF956,
            0x42773CE2, 0x78A5E4B1, 0xE73D269A, 0xC5550CEA,
            0x7EB5E155, 0x56A25F0B, 0xBA222442, 0x4FA2F8FE,
            0xDFDC45C5, 0xA6E92216, 0xA9D99ADF, 0x913DEC47,
            0x408559E2, 0x85136257, 0xBDEA8276, 0x40E1B83E,
            0xC92DEF69, 0x6FC0A465, 0x38CDDADB, 0x59DA2BB5,
            0x484789AF, 0x9AAD12F2, 0x62F5F84C, 0x7897DAE6,
            0x75CC24B6, 0xBCBDC6E2, 0xDAE3D7F2, 0xA5B57984,
            0x4DCA8E3C, 0x620E1D57, 0xEC36E982, 0xA3952344,
            0xDAF3B1C7, 0xBA8486C3, 0x5AB768BD, 0xC368AD76,
            0x0A3068BD, 0x6A6352F7, 0x2C729F8B, 0xB241FA90,
            0x77AD496C, 0xDAD20633, 0x84BAC587, 0xC722C4C2,
            0x3EEEFA1E, 0x07F326B5, 0xECC4460F, 0xD892EF7F,
            0xCB5A0028, 0xD1F38ACD, 0xA9B110D1, 0x07A75B15,
            0x2BCE0CFB, 0xCF96AE6A, 0x859D6CE5, 0x0F18B135,
            0x5BAC4F33, 0x99CB7EA7, 0xA1B4C851, 0x9BCDB50C,
            0x186EC8A7, 0x1EB829E1, 0xCB3E891F, 0x117295C9,
            0xAFAE94F8, 0xEE4F7044, 0x45B0566E, 0xE302BA3A,
            0x4F3FBEA6, 0x915A75DA, 0x9BBE727E, 0x60D7164B,
            0x55391198, 0xD28A9020, 0x71E78444, 0x01C2F4A7,
            0x20086333, 0x8F0E188B, 0xD408E679, 0x3E904E3B,
            0xDDFD6BE4, 0x8A9AB527, 0x096C7F25, 0xDA22924E,
            0x5D610DB9, 0x3CFD371A, 0xDB5B06B9, 0x35F97E8C,
            0x5C802BE7, 0x9FB15ABC, 0x71295A16, 0x9949E79D,
            0x5CB9F6DD, 0x05669F23, 0x9749D31F, 0x11850EAA,
            0xEA973ED9, 0x5F179B36, 0x14DA0C4E, 0xAD736BF7,
            0x78533F72, 0x17174237, 0xF3B9B8BB, 0xE0B804F1,
            0xA56F76AC, 0xD924BB5D, 0x5D906FB9, 0xC7EEB736,
            0x91436C83, 0x6774325A, 0xDF668056, 0x7AAC0EB1,
            0xAC0A0A82, 0x65C72071, 0xC0B0C1F9, 0x5E1E8A23,
            0x8773E3CC, 0x2C762683, 0xCF605DE9, 0xF30F7C31,
            0xA4B71033, 0x1A83D3A2, 0xB3F326E3, 0xABFA497B,
            0x44217141, 0x602AFB20, 0xBA876224, 0xAB20C4A7,
            0xB6A188CB, 0xD5ED8399, 0xAE6622FE, 0x2D8EF8F3,
            0x2FDE4405, 0x44AEB20F, 0x9D9489E7, 0xC0377F44,
            0x1044D389, 0x39B102ED, 0xB068A108, 0x26F9C6B3,
            0xB912F0F1, 0x5939EDA4, 0x77122DCA, 0x17BA69A5,
            0xDFEE3661, 0x2A0DC4AE, 0x3AB0726F, 0x9BEE7E55
        },
        KS3 =
        {
            0x55F4EC16, 0xE891F5A9, 0xCA5E8E19, 0x572AE362,
            0x5CC5DAF9, 0x52D165DD, 0xD1C546DA, 0xD8359467,
            0xF79594B2, 0x7B9337D7, 0x21ACD1CF, 0xEC167383,
            0xBCBF4EAF, 0x1DE522F1, 0x8106B223, 0xEA279F6D,
            0x9ED327BD, 0x62B14063, 0x840380A2, 0x0923C106,
            0x3F290292, 0xBCC5DCD3, 0xD5A1B4D1, 0xA436DADF,
            0x4468495F, 0x306DC5EB, 0x063C05F6, 0x1E8C9AD9,
            0xA2A04BE3, 0xA5FC9B62, 0xDF9CA92A, 0x9CE326A6,
            0xEF5A8077, 0x3BE0208C, 0xB70832E7, 0x661DEE64,
            0x2D9CE39D, 0x122F08EE, 0xB7D15320, 0x2CC702A3,
            0xA388B995, 0x1FB74A4F, 0x32E83AC4, 0xDC375500,
            0xA76268E9, 0x7E91EFC0, 0x746C58DB, 0x720E1829,
            0x6E24C27F, 0x3C316435, 0x12B5378E, 0xC1D08478,
            0x5D8E5BA8, 0x2C70C690, 0x36EC4339, 0xCFF4B07C,
            0x5C35D0AF, 0x3424B3B2, 0xF497A37C, 0x9AF15B8E,
            0x201121EC, 0x9F289F8C, 0x9B29834A, 0xEBCFBCDA,
            0x0092F953, 0xEEF51E2F, 0x7F90E179, 0x2D395978,
            0x43AD04FE, 0x232C3658, 0x4DC9E853, 0x30874EF2,
            0xF0EB3ACA, 0x96253106, 0x5AEFB524, 0x87D6235D,
            0x21FBB1DA, 0xC302E808, 0x73CA2C4C, 0xCB159CE5,
            0x4A42EFAD, 0x9B411B09, 0xAEDE474E, 0x7810835D,
            0x94683AAE, 0xD1C43826, 0x3BFD6761, 0x3009804D,
            0xA9EB60C2, 0x306DB1FA, 0x2F57237E, 0x153F6586,
            0x7FB1935E, 0x63A24BB1, 0x57079773, 0x13867C32,
            0xB192B10B, 0xCB64EA93, 0x8D273970, 0xB451DADF,
            0x47EB1881, 0xD0DC6B9B, 0xFDDF2197, 0x6FC3B095,
            0x89B2EF35, 0xAAEF6480, 0x8172DE10, 0x744698E0,
            0xD27F03E4, 0xB74A86C9, 0xE6534895, 0x038E65E8,
            0xDB9F8928, 0x4A715DDF, 0xC6B04B83, 0xB435F6F7,
            0x8F277805, 0xF8552D97, 0x5105B96A, 0xCFC30513,
            0xD67ED3FC, 0xEEDDB9C6, 0x9EAA1C1E, 0x98C57305,
            0xEDF37C99, 0xB97C9752, 0x7D6782C6, 0x1C579F6F,
            0xADC90480, 0x9EBC813D, 0x4A7FD06C, 0x87B6AEDD,
            0xE2C87806, 0x64521FBA, 0xB245148D, 0x7251E54E,
            0x17CEB536, 0xA5AADFBD, 0x12A34F2C, 0xB0576E4A,
            0xE65D38E8, 0x247BC088, 0x3A39475A, 0x214CAFF0,
            0x4D2E69D1, 0x925625BB, 0x435C5D4F, 0x808F1A09,
            0x763E7010, 0x6B341F65, 0x60B455F6, 0x3977F70F,
            0x0F60033E, 0xB806C597, 0x2CC529FD, 0x2ED63F4A,
            0x92CA3400, 0x6EC97FEE, 0x787DDBE4, 0x1195DED4,
            0x1F2CC717, 0x3091D8AB, 0x20CCC311, 0xAFBB8FB2,
            0xBF36FDEF, 0x271ECD38, 0xD62B5ED8, 0xC37F255F,
            0x44B065AF, 0x52E41BC2, 0x77349F18, 0x43DD5E5C,
            0x8789AACA, 0x52A79445, 0x55ACC23C, 0x2F9FB342,
            0x4860690E, 0x3E016C1D, 0x0D151755, 0x67F22653,
            0xF41F7B30, 0x71F18712, 0x53BFD3A8, 0xF4751385,
            0xFA01C8E8, 0xD878CF6F, 0xC159E5BF, 0xE0C58298,
            0x99A717F4, 0x4999FD8C, 0xB07B3F72, 0xFB94F423,
            0x2EAA5A30, 0xC8F46D61, 0x793EAA80, 0xB7B2B5A2,
            0x8926059F, 0x6352EE15, 0xD246981B, 0x6EA2AA8A,
            0x394CD884, 0x79B2128E, 0x1AD7ECF7, 0x37A9A3D8,
            0x5EF3B3EB, 0x0D617A03, 0xA460D4DC, 0x395C2B1F,
            0xF628633C, 0x9702AC08, 0x908C1336, 0x72B15598,
            0xB3BA7548, 0xD6A16121, 0x13574FA3, 0x0F119037,
            0x45537FDF, 0x1AC6D151, 0xCB18E905, 0x39E3F7B2,
            0x2BFBFCD9, 0x96818A2B, 0x1191C68F, 0x66A31C9D,
            0x0738922D, 0x35F83961, 0xBF07A1DB, 0x28EB5B6D,
            0x690BE578, 0xE381FDD4, 0x91475CF5, 0x7A8BAE12,
            0xA2136995, 0xE426BD26, 0x653F4F6B, 0x188D7601,
            0x168FAD2D, 0x1E3AEB44, 0x900697DE, 0x4753CFC7,
            0x83F632BF, 0x696DE07E, 0x36F46A15, 0x2A1CE19A,
            0xDD0535B9, 0xE4D42512, 0x0B2EE207, 0x901BAAE4,
            0x18C98183, 0x4403C53E, 0x32B3CF70, 0x4302D85B,
            0x733BC012, 0x9D1020CD, 0x83730B5F, 0x5B160F06
        };

        //====================================
        // Useful constants
        //====================================

        private static readonly int ROUNDS = 16;
        private const int BLOCK_SIZE = 8;  // bytes = 64 bits
        private static readonly int SBOX_SK = 256;
        private static readonly int P_SZ = ROUNDS + 2;

        private readonly uint[] S0, S1, S2, S3;     // the s-boxes
        private readonly uint[] P;                  // the p-array

        private bool encrypting;

        private byte[] workingKey;

        public FantasyTennisBlowfishEngine()
        {
            S0 = new uint[SBOX_SK];
            S1 = new uint[SBOX_SK];
            S2 = new uint[SBOX_SK];
            S3 = new uint[SBOX_SK];
            P = new uint[P_SZ];
        }

        /**
        * initialise a Blowfish cipher.
        *
        * @param forEncryption whether or not we are for encryption.
        * @param parameters the parameters required to set up the cipher.
        * @exception ArgumentException if the parameters argument is
        * inappropriate.
        */
        public void Init(
            bool forEncryption,
            ICipherParameters parameters)
        {
            if (!(parameters is KeyParameter))
                throw new ArgumentException("invalid parameter passed to Blowfish init - " + Platform.GetTypeName(parameters));

            this.encrypting = forEncryption;
            this.workingKey = ((KeyParameter)parameters).GetKey();
            SetKey(this.workingKey);
        }

        public string AlgorithmName
        {
            get { return "Blowfish"; }
        }

        public bool IsPartialBlockOkay
        {
            get { return false; }
        }

        public int ProcessBlock(
            byte[] input,
            int inOff,
            byte[] output,
            int outOff)
        {
            if (workingKey == null)
                throw new InvalidOperationException("Blowfish not initialised");

            Check.DataLength(input, inOff, BLOCK_SIZE, "input buffer too short");
            Check.OutputLength(output, outOff, BLOCK_SIZE, "output buffer too short");

            if (encrypting)
            {
                EncryptBlock(input, inOff, output, outOff);
            }
            else
            {
                DecryptBlock(input, inOff, output, outOff);
            }

            return BLOCK_SIZE;
        }

        public void Reset()
        {
        }

        public int GetBlockSize()
        {
            return BLOCK_SIZE;
        }

        //==================================
        // Private Implementation
        //==================================

        private uint F(uint x)
        {
            return (((S0[x >> 24] + S1[(x >> 16) & 0xff]) ^ S2[(x >> 8) & 0xff]) + S3[x & 0xff]);
        }

        /**
        * apply the encryption cycle to each value pair in the table.
        */
        private void ProcessTable(
            uint xl,
            uint xr,
            uint[] table)
        {
            int size = table.Length;

            for (int s = 0; s < size; s += 2)
            {
                xl ^= P[0];

                for (int i = 1; i < ROUNDS; i += 2)
                {
                    xr ^= F(xl) ^ P[i];
                    xl ^= F(xr) ^ P[i + 1];
                }

                xr ^= P[ROUNDS + 1];

                table[s] = xr;
                table[s + 1] = xl;

                xr = xl;            // end of cycle swap
                xl = table[s];
            }
        }

        private void SetKey(byte[] key)
        {
            /*
            * - comments are from _Applied Crypto_, Schneier, p338
            * please be careful comparing the two, AC numbers the
            * arrays from 1, the enclosed code from 0.
            *
            * (1)
            * Initialise the S-boxes and the P-array, with a fixed string
            * This string contains the hexadecimal digits of pi (3.141...)
            */
            Array.Copy(KS0, 0, S0, 0, SBOX_SK);
            Array.Copy(KS1, 0, S1, 0, SBOX_SK);
            Array.Copy(KS2, 0, S2, 0, SBOX_SK);
            Array.Copy(KS3, 0, S3, 0, SBOX_SK);

            Array.Copy(KP, 0, P, 0, P_SZ);

            /*
            * (2)
            * Now, XOR P[0] with the first 32 bits of the key, XOR P[1] with the
            * second 32-bits of the key, and so on for all bits of the key
            * (up to P[17]).  Repeatedly cycle through the key bits until the
            * entire P-array has been XOR-ed with the key bits
            */
            int keyLength = key.Length;
            int keyIndex = 0;

            for (int i = 0; i < P_SZ; i++)
            {
                // Get the 32 bits of the key, in 4 * 8 bit chunks
                uint data = 0x0000000;
                for (int j = 0; j < 4; j++)
                {
                    // create a 32 bit block
                    data = (data << 8) | (uint)key[keyIndex++];

                    // wrap when we get to the end of the key
                    if (keyIndex >= keyLength)
                    {
                        keyIndex = 0;
                    }
                }
                // XOR the newly created 32 bit chunk onto the P-array
                P[i] ^= data;
            }

            /*
            * (3)
            * Encrypt the all-zero string with the Blowfish algorithm, using
            * the subkeys described in (1) and (2)
            *
            * (4)
            * Replace P1 and P2 with the output of step (3)
            *
            * (5)
            * Encrypt the output of step(3) using the Blowfish algorithm,
            * with the modified subkeys.
            *
            * (6)
            * Replace P3 and P4 with the output of step (5)
            *
            * (7)
            * Continue the process, replacing all elements of the P-array
            * and then all four S-boxes in order, with the output of the
            * continuously changing Blowfish algorithm
            */

            ProcessTable(0, 0, P);
            ProcessTable(P[P_SZ - 2], P[P_SZ - 1], S0);
            ProcessTable(S0[SBOX_SK - 2], S0[SBOX_SK - 1], S1);
            ProcessTable(S1[SBOX_SK - 2], S1[SBOX_SK - 1], S2);
            ProcessTable(S2[SBOX_SK - 2], S2[SBOX_SK - 1], S3);
        }

        /**
        * Encrypt the given input starting at the given offset and place
        * the result in the provided buffer starting at the given offset.
        * The input will be an exact multiple of our blocksize.
        */
        private void EncryptBlock(
            byte[] src,
            int srcIndex,
            byte[] dst,
            int dstIndex)
        {
            uint xl = Pack.LE_To_UInt32(src, srcIndex);
            uint xr = Pack.LE_To_UInt32(src, srcIndex + 4);

            xl ^= P[0];

            for (int i = 1; i < ROUNDS; i += 2)
            {
                xr ^= F(xl) ^ P[i];
                xl ^= F(xr) ^ P[i + 1];
            }

            xr ^= P[ROUNDS + 1];

            Pack.UInt32_To_LE(xr, dst, dstIndex);
            Pack.UInt32_To_LE(xl, dst, dstIndex + 4);
        }

        /**
        * Decrypt the given input starting at the given offset and place
        * the result in the provided buffer starting at the given offset.
        * The input will be an exact multiple of our blocksize.
        */
        private void DecryptBlock(
            byte[] src,
            int srcIndex,
            byte[] dst,
            int dstIndex)
        {
            uint xl = Pack.LE_To_UInt32(src, srcIndex);
            uint xr = Pack.LE_To_UInt32(src, srcIndex + 4);

            xl ^= P[ROUNDS + 1];

            for (int i = ROUNDS; i > 0; i -= 2)
            {
                xr ^= F(xl) ^ P[i];
                xl ^= F(xr) ^ P[i - 1];
            }

            xr ^= P[0];

            Pack.UInt32_To_LE(xr, dst, dstIndex);
            Pack.UInt32_To_LE(xl, dst, dstIndex + 4);
        }
    }
}
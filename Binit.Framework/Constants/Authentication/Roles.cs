using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Binit.Framework.Constants.Authentication
{
    /// <summary>
    /// Role constants class.
    /// Contains all the application roles represented by constants.
    /// </summary>
    public static class Roles
    {
        #region Backoffice Realm

        // Super admin.
        public const string BackofficeSuperAdministrator = "Backoffice.SuperAdministrator";
        private static Guid BackofficeSuperAdministratorId = Guid.Parse("E68ACD69-3282-4D49-8DCF-9C78BF5D01F2");

        // Roles per entity.
        public const string BackofficeBackofficeUserAdministrator = "Backoffice.BackofficeUserAdministrator";
        private static Guid BackofficeBackofficeUserAdministratorId = Guid.Parse("ED63B004-3AA6-4A01-92EE-C43D0BF6AA7C");

        public const string BackofficeFrontUserAdministrator = "Backoffice.FrontUserAdministrator";
        private static Guid BackofficeFrontUserAdministratorId = Guid.Parse("A4E0835D-78D3-403A-872E-2E5D01F6B7BA");

        public const string BackofficeCategoryAdministrator = "Backoffice.CategoryAdministrator";
        private static Guid BackofficeCategoryAdministratorId = Guid.Parse("9AA3BA9E-09D0-456C-B32F-D32160D10FBB");

        public const string BackofficeCategoryUser = "Backoffice.CategoryUser";
        private static Guid BackofficeCategoryUserId = Guid.Parse("E32F964B-893B-48A1-8D5A-A9D10AFC224D");

        public const string BackofficeEventAdministrator = "Backoffice.EventAdministrator";
        private static Guid BackofficeEventAdministratorId = Guid.Parse("25556110-3729-44CA-80E5-DD10511EA721");

        public const string BackofficeEventUser = "Backoffice.EventUser";
        private static Guid BackofficeEventUserId = Guid.Parse("64E18892-A032-4650-921C-B2F815B69020");

        public const string BackofficeHolidayAdministrator = "Backoffice.HolidayAdministrator";
        private static Guid BackofficeHolidayAdministratorId = Guid.Parse("88B52366-A0AF-470A-89DF-EE6FB666FFAC");

        public const string BackofficeHolidayUser = "Backoffice.HolidayUser";
        private static Guid BackofficeHolidayUserId = Guid.Parse("079379DD-7DFA-4ADC-87D1-80F241E2BB69");

        public const string BackofficeHolidayTypeAdministrator = "Backoffice.HolidayTypeAdministrator";
        private static Guid BackofficeHolidayTypeAdministratorId = Guid.Parse("F721A643-AE92-48F8-9FF8-AAE0D3C71473");

        public const string BackofficeHolidayTypeUser = "Backoffice.HolidayTypeUser";
        private static Guid BackofficeHolidayTypeUserId = Guid.Parse("D7384672-E410-43E5-9BA6-44F084C827A3");

        public const string BackofficeProductAdministrator = "Backoffice.ProductAdministrator";
        private static Guid BackofficeProductAdministratorId = Guid.Parse("0E79A12B-C81A-4F83-9D00-E309A5004A0B");

        public const string BackofficeProductUser = "Backoffice.ProductUser";
        private static Guid BackofficeProductUserId = Guid.Parse("58278BB9-CCD4-4117-89E6-4A56789DD4B0");

        public const string BackofficePlayUser = "Backoffice.PlayUser";
        private static Guid BackofficePlayUserId = Guid.Parse("A8942706-CF1E-4712-8598-B572AF55443B");

        #endregion

        #region Front Realm

        // Super admin.
        public const string FrontSuperAdministrator = "Front.SuperAdministrator";
        private static Guid FrontSuperAdministratorId = Guid.Parse("1792BCC1-7A65-47BD-B0FC-AD0A3B1962B4");

        // Roles per entity.
        public const string FrontFrontUserAdministrator = "Front.FrontUserAdministrator";
        private static Guid FrontFrontUserAdministratorId = Guid.Parse("9E11FC55-C2EA-47E4-83E0-6A6D3C5E2BBC");

        public const string FrontCategoryAdministrator = "Front.CategoryAdministrator";
        private static Guid FrontCategoryAdministratorId = Guid.Parse("5C6D2ADD-2698-4374-B7EF-BC983F03FA37");

        public const string FrontCategoryUser = "Front.CategoryUser";
        private static Guid FrontCategoryUserId = Guid.Parse("755813DE-DAA3-4AC1-A30F-47C0C4B8D8CD");

        public const string FrontEventAdministrator = "Front.EventAdministrator";
        private static Guid FrontEventAdministratorId = Guid.Parse("34823D83-52DF-4336-9AF3-44BF3D3F539D");

        public const string FrontEventUser = "Front.EventUser";
        private static Guid FrontEventUserId = Guid.Parse("49F74697-627F-441B-8EAD-D54BB4FA9447");

        public const string FrontHolidayAdministrator = "Front.HolidayAdministrator";
        private static Guid FrontHolidayAdministratorId = Guid.Parse("ABACD34C-9447-49EE-B1B5-1ABCDA079BE3");

        public const string FrontHolidayUser = "Front.HolidayUser";
        private static Guid FrontHolidayUserId = Guid.Parse("C9A897B5-B412-4ED6-A322-E7C9C824114B");

        public const string FrontHolidayTypeAdministrator = "Front.HolidayTypeAdministrator";
        private static Guid FrontHolidayTypeAdministratorId = Guid.Parse("68BDB122-5A6A-463D-8A62-FC08FD8763BF");

        public const string FrontHolidayTypeUser = "Front.HolidayTypeUser";
        private static Guid FrontHolidayTypeUserId = Guid.Parse("59F3DB1E-77AE-4E33-8155-A063081E3584");

        public const string FrontProductAdministrator = "Front.ProductAdministrator";
        private static Guid FrontProductAdministratorId = Guid.Parse("DE66C857-3C37-445E-A19B-546EDF5BCF1F");

        public const string FrontProductUser = "Front.ProductUser";
        private static Guid FrontProductUserId = Guid.Parse("D1BFAB2A-E541-407D-92F2-DBD2CB3DB73A");

        #endregion


        /// <summary>
        /// Obtains and returns all the roles defined as constants in this class by reflection.
        /// </summary>
        public static ICollection<string> GetAllRoles(bool includeSuperAdmin = false)
        {
            var roles = typeof(Roles)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Select(f => f.GetValue(null).ToString());

            if (!includeSuperAdmin)
            {
                roles = roles.Where(f => f != BackofficeSuperAdministrator && f != FrontSuperAdministrator);
            }

            return roles.ToList();
        }

        public static ICollection<string> GetRolesByRealm(string realm, bool includeSuperAdmin = false)
        {
            return GetAllRoles(includeSuperAdmin)
                .Where(r => r.StartsWith($"{realm}."))
                .ToList();
        }

        public static Guid GetId(string rol)
        {
            var publicFields = typeof(Roles).GetFields(BindingFlags.Static | BindingFlags.Public);
            var rolName = publicFields.FirstOrDefault(f => f.GetValue(null).ToString() == rol).Name;

            var privateFields = typeof(Roles).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            return Guid.Parse(privateFields.FirstOrDefault(f => f.Name == $"{rolName}Id").GetValue(null).ToString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleBot
{
    public static class Env
    {
        public static readonly string BOT_API_TOKEN = "5666252527:AAFpWhGB2mDeayW9AW7-9yiMqnQY9yd1whA";
        public static readonly SpreadSheetsIds SpreadSheets = new SpreadSheetsIds();
        public static readonly string GoogleCloudDeveloperConsoleCredentials = "{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"eminent-prism-365418\",\r\n  \"private_key_id\": \"7e36fb6b91eb8afe010613f3494c25d5050c7d8a\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC93d8ASp1Oh5J8\\niqhVto3dwht5VUWzROZaeDE8ts0zUj5IRBdX4wt9nLi0KH+ajVZSbD572HFVMl35\\n+of8J7WLVGSRFt06lgserC0IZRc9BF8MGyASrfAexlPyxZtvHqy52WI6fkDGFj0z\\nmWDyGyIIcAmS7ahA385C3PgW90IXdWPhO5bXSz5rClnP1wj0KDKErKzrgDaLIfIR\\n0nRFAaFhs35YpRd1r+uW+cHSIJHsJNrqTABztNrkyKbm4F1Ly+DTc738Vcsxx524\\nY+FR3zuyxqzk1X88z1+UQ7J4knqV8Dy3HjqcoRUBxDUDpfp6CpUtyVBxuDwf5ntj\\nJhktbi1jAgMBAAECggEAC8JhHGiCo6l75iDEWkKrOK/b+cDRR1AeID5Pdl4wCVyv\\npNQqyfOy832wZuAXPyA/120C1bLLGia7cU8V+wUOlmmMrPvIiedfGvA4/csdDFdn\\n95z6eI2zUISiEGgyv0gcFIqlLdBB/MLF2ZtHSNdrXTBrN2FmyVXTNYrwBqQbwM4W\\nIh6We39FtfZiy3uoNtnep0UBqVW0Cz0oyurrFVaauhYU6NpS5xO88soofQNhlJuf\\nzNJiFc7yjv2v2rNn6jcLe4TzecfWwSv0lUX3MnNLSSDJ2wIH8H1eh69WKwziAj4x\\n+VKkjLTVhboiFoKhikg3ywV9pmGlnrIqx3MP9lX5mQKBgQDmtVPz9u21w0RtrNEE\\n7K+CGsx22f2kOoUJkuWPAjPuO4/4txxg1iDEW12STfQA6+WAgden+VMTJ4BLn/j1\\nkr5R1lDfusMBalgUXVian2Ahy8d42HRsRwG2LurGXKBIvIyuRw5c1b2IFfOm4Heq\\nu0L1MsTo/NAyh8PmJTDQB4UcvwKBgQDSrlnBJQAtcRXo/djQV9Ek5QJmO22r7MK/\\nfuFqs0bKIDjnJBLZ+ugknRvyhF8cYr/dD3OYOr4XVlZeJ1pHhKUCDjNkjGrU0MnO\\nBi5ADHdMFEa5tLgtOjbsf6flFYJ+mNsPUdAMPL3yTUd0DI59WaGW0gEE4wHlXuGX\\nWPZE1ZBEXQKBgQC/3LCSxRnu/RMdOokhVUyoIG1Fsggz2c7lpVpvUd+qEbKrnmxl\\nQ+5AHdN1ZoqzcXqNIm4cbUZfOYyCV+mNIJpSKK9dQE4IEU5qsS6MeFpUAMpEf45i\\ntbKAtgfdKw7JLrS4ArO5FK/oSok0kJw0SkNm8u+66vSh8TJDZU5w0rAqtQKBgQCc\\nU7zLwV3JWjEMylc3+QF6Y+eM3in5uVukZkxzNQWg85nlgPHwBup8SIu1Px2n3WpK\\naK24VlY40NqTUJX/nWAnD0x5Hmwi763ejt2Arv9SV3VHVN6YyKp6gBqftH842skp\\nK+a5PdUf1tHfPXX5cymg1MDm1F7SyL2R5PQPjXxYDQKBgGYTBbQU9WplcheQC1N4\\nGMGFem3pQNItKmdhofrkpImrgJh60AolLR25Rhu4iTHOgZjJZIH6KpT3RHQGLdjr\\ncgl2Jrc1nFw3GFV4hn3MVq4KvNLM7mX0ChC2PT1r5fLig4t92IVqFNUjYw5scnWE\\nsPtNAhCQq0Me5Ue7fKjmttUv\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"rozkladaccount@eminent-prism-365418.iam.gserviceaccount.com\",\r\n  \"client_id\": \"103328237115911108473\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/rozkladaccount%40eminent-prism-365418.iam.gserviceaccount.com\"\r\n}\r\n";
    }

    public class SpreadSheetsIds 
    {
        public readonly string Bakalavr1 = "1n8aU85K284sf3xBCkCK3cdU1k3y8YuLD";
        public readonly string Bakalavr2 = "16WDnTvltl8qyuj9lz-uEp8rYcEqBdkvk";
        public readonly string Bakalavr3 = "1D4I_X7mAOmALsGsZXaYsY50_AZXxdHPi";
        public readonly string Bakalavr4 = "1lNi_4BYDd_jZz5rB6HpYr6Gi5YkUdiia";

        public readonly string Test = "13uEI7ekFk67GJPuTBO0FPxQdZDaiY7_AKN3fNYC9CFI";
    }

}

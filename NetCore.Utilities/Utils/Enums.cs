using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Utilities.Utils
{
    public class Enums
    {
        // 암호화 유형
        // https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/data-protection/configuration/overview.md

        // PersistKeyToFileSystem : 키관리
        public enum CryptoType
        {
            Unmanaged = 1,
            Managed = 2,
            CngCbc = 3,
            CngGcm = 4
        }

    }
}

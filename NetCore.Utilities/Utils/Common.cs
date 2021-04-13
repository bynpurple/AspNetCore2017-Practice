using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetCore.Utilities.Utils
{
    public static class Common
    {
        /// <summary>
        /// Data Proction 지정하기
        /// </summary>
        /// <param name="services">등록할 서비스</param>
        /// <param name="keyPath">키 경로</param>
        /// <param name="applicationName">애플리케이션명</param>
        /// <param name="cryptoType">암호화유형</param>

        public static void SetDataProtection(IServiceCollection services, string keyPath, string applicationName, Enum cryptoType)
        {
            var Builder = services.AddDataProtection()
                // 키 저장소 공급자, 파일 시스템 기반 키 리포지토리를 구성
                //@"C:\Users\user\Documents\AspNet\DataProtector\"
                .PersistKeysToFileSystem(new DirectoryInfo(keyPath))
                // 해당 키의 만료기간 설정
                .SetDefaultKeyLifetime(TimeSpan.FromDays(7))
                // 애플리케이션 이름 // "NetCore2017-Practice"
                .SetApplicationName(applicationName);

            switch (cryptoType)
            {
                // [!NOTE] The SymmetricAlgorithm must have a key length of ≥ 128 bits and a block size of ≥ 64 bits, 
                // and it must support CBC-mode encryption with PKCS #7 padding. 
                // The KeyedHashAlgorithm must have a digest size of >= 128 bits, and it must support keys of length equal to the hash algorithm's digest length. 
                // The KeyedHashAlgorithm isn't strictly required to be HMAC.
                case Enums.CryptoType.Unmanaged:
                    // AES (Advanced Encryption Standard)
                    // Two-Way : 암호화, 복호화
                    Builder.UseCryptographicAlgorithms(
                        new AuthenticatedEncryptorConfiguration()
                        {
                            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                            // SHA (Secure Hash Algorithm
                            // One-way : Only 암호화
                            ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
                        });

                    break;
                case Enums.CryptoType.Managed:

                    Builder.UseCustomCryptographicAlgorithms(
                        new ManagedAuthenticatedEncryptorConfiguration()
                        {
                            // A type that subclasses SymmetricAlgorithm
                            EncryptionAlgorithmType = typeof(Aes),

                            // Specified in bits
                            EncryptionAlgorithmKeySize = 256,

                            // A type that subclasses KeyedHashAlgorithm
                            ValidationAlgorithmType = typeof(HMACSHA256)
                        });

                    break;
                case Enums.CryptoType.CngCbc:
                    //Specifying custom Windows CNG algorithms
                    // CNG Algorithm
                    // Cryptography API Next Generation
                    // CBC-mode : Cipher Block Chaining
                    Builder.UseCustomCryptographicAlgorithms(
                        new CngCbcAuthenticatedEncryptorConfiguration()
                        {
                            // Passed to BCryptOpenAlgorithmProvider
                            EncryptionAlgorithm = "AES",
                            EncryptionAlgorithmProvider = null,

                            // Specified in bits
                            EncryptionAlgorithmKeySize = 256,

                            // Passed to BCryptOpenAlgorithmProvider
                            HashAlgorithm = "SHA512",
                            HashAlgorithmProvider = null
                        });
                    break;
                case Enums.CryptoType.CngGcm:
                    // Windows CNG algorithm using Galois/Counter Mode encryption
                    // GCM-mode : Galois - Counter Mode

                    Builder.UseCustomCryptographicAlgorithms(
                        new CngGcmAuthenticatedEncryptorConfiguration()
                        {
                            // Passed to BCryptOpenAlgorithmProvider
                            EncryptionAlgorithm = "AES",
                            EncryptionAlgorithmProvider = null,

                            // Specified in bits
                            EncryptionAlgorithmKeySize = 256
                        });
                    break;
                

            }
        }
    }
}

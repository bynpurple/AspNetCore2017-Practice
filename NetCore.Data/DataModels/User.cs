using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCore.Data.DataModels
{
    // Data annotations
    public class User
    {
        // Key Annoatation : Primary Key
        [Key, StringLength(50), Column(TypeName = "varchar(50)", Order = 0)]
        public string UserId { get; set; }

        // nvarchar : not null varchar
        [Required, StringLength(100), Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; }

        // 중복 안되는 인덱스 지정
        [Required, StringLength(320), Column(TypeName = "nvarchar(320)")]
        public string UserEmail { get; set; }


        [Required, StringLength(130), Column(TypeName = "nvarchar(130)")]
        public string Password { get; set; }

        // Nullable, ? 로 설정 가능
        [Required]
        public bool IsMemberShipWithDrawn { get; set; }

        [Required]
        public DateTime JoinedUtcDate { get; set; }

        // Foreign Key 설정
        [ForeignKey("UserId")]
        public virtual ICollection<UserRolesByUser> UserRolesByUsers { get; set; }
    }
}

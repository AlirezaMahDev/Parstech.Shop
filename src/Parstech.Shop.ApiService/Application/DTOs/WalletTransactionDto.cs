using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.WalletTransaction
{
    public class WalletTransactionDto
    {
        public int Id { get; set; }

        public int WalletId { get; set; }

        public int Price { get; set; }
        public string InputPrice { get; set; }

        public string Type { get; set; } = null!;

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public string? TrackingCode { get; set; }

        public string? Description { get; set; }

        public int? Persent { get; set; }

        public int? Month { get; set; }

        public bool? Start { get; set; }
        public bool? Active { get; set; }
        public string StartName { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateDateShamsi { get; set; }

        public DateTime? ExpireDate { get; set; }
        public string ExpireDateShamsi { get; set; }

        public string? FileName { get; set; }
    }

    public class WalletTransactionParameterDto
    {
        public int CurrentPage { get; set; }
        public int TakePage { get; set; }
        public int PageCount { get; set; }
        public string Filter { get; set; }
        public int WalletId { get; set; }
        public string Type { get; set; }

    }

    public class WalletTransactionResult
    {
        public bool isSuccessed { get; set; }
        public WalletTransactionDto walletTransaction { get; set; }
    }
}

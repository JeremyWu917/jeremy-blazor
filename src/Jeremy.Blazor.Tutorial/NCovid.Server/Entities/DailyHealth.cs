using NCovid.Shared.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCovid.Server.Entities
{
    public class DailyHealth
    {
        public int EmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Display(Name = "健康状况")]
        public HealthCondition HealthCondition { get; set; }

        [Display(Name = "体温")]
        public float Temperature { get; set; }

        [Display(Name = "备注")]
        [StringLength(100, ErrorMessage = "{0}的长度不能超过{1}")]
        public string Remark { get; set; }
    }
}

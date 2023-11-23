

using System.ComponentModel.DataAnnotations.Schema;

namespace CustompgAdmin.DataAccess.Entities;

public class Function:BaseEntity
{
    public int DatabaseId { get; set; }
    public string CreatedQuery { get; set; }


    [ForeignKey("OrganizationId")]
    [InverseProperty("Functions")]
    public virtual DatabaseEntity DatabaseEntity { get; set; }
}

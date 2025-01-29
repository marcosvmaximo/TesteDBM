using System.ComponentModel.DataAnnotations;

namespace Teste.Core;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}
using System.ComponentModel.DataAnnotations;
namespace TodojsAspire.ApiService;

public class Todo
{
    public int Id{get;set;}
    [Required]
    public string Title{get;set;}=default!;
    public bool IsComplete{get;set;}=false;
    // The position of the todo in the list, used for ordering.
    // When updating this value, make sure to not duplicate values.
    // To move an item up/down it's best to swap the values of the position
    [Required]
    public int Position{get;set;}=0;
}

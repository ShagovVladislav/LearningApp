namespace LearningApp.api.Models;

public class Lesson
{
    public Guid Id { get; set; }
    public string Title  { get; set; }
    public int Number  { get; set; }
    public string Content  { get; set; }
    public Guid CourseId { get; set; }
}
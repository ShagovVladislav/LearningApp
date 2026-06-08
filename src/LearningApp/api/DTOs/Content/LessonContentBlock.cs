using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearningApp.api.DTOs.Content;

[JsonPolymorphic(TypeDiscriminatorPropertyName = LessonContentBlockTypeNames.TypePropertyName)]
[JsonDerivedType(typeof(HeadingLessonBlock), LessonContentBlockTypeNames.Heading)]
[JsonDerivedType(typeof(ParagraphLessonBlock), LessonContentBlockTypeNames.Paragraph)]
[JsonDerivedType(typeof(ImageLessonBlock), LessonContentBlockTypeNames.Image)]
[JsonDerivedType(typeof(CodeLessonBlock), LessonContentBlockTypeNames.Code)]
[JsonDerivedType(typeof(VideoLessonBlock), LessonContentBlockTypeNames.Video)]
[JsonDerivedType(typeof(WarningLessonBlock), LessonContentBlockTypeNames.Warning)]
[JsonDerivedType(typeof(TaskLessonBlock), LessonContentBlockTypeNames.Task)]
public abstract class LessonContentBlock
{
    [Range(0, int.MaxValue)]
    public int Order { get; set; }
}

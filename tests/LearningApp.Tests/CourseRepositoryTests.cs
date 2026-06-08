using FluentAssertions;
using LearningApp.api.Exceptions;
using LearningApp.api.Models;
using LearningApp.api.Repositories;
using NUnit.Framework;

namespace LearningApp.Tests;

[TestFixture]
[NonParallelizable]
public sealed class CourseRepositoryTests
{
    private static PostgreSqlFixture Fixture => PostgreSqlTestEnvironment.Fixture;

    [SetUp]
    public async Task SetUp()
    {
        await Fixture.ResetDatabaseAsync();
    }

    [Test]
    public async Task CreateCourseAsync_PersistsCourse()
    {
        await using var dbContext = Fixture.CreateDbContext();
        var repository = new CourseRepository(dbContext);
        var course = CreateCourse("C# basics", "Intro course");

        var createdCourse = await repository.CreateCourseAsync(course);

        createdCourse.Should().BeEquivalentTo(course);

        var storedCourse = await repository.GetCourseByIdAsync(course.Id);
        storedCourse.Should().BeEquivalentTo(course);
    }

    [Test]
    public async Task GetAllCoursesAsync_ReturnsPersistedCourses()
    {
        await using var dbContext = Fixture.CreateDbContext();
        var repository = new CourseRepository(dbContext);
        var firstCourse = CreateCourse("C# basics", "Intro course");
        var secondCourse = CreateCourse("ASP.NET Core", null);
        await repository.CreateCourseAsync(firstCourse);
        await repository.CreateCourseAsync(secondCourse);

        var courses = await repository.GetAllCoursesAsync();

        courses.Should().HaveCount(2);
        courses.Should().ContainEquivalentOf(firstCourse);
        courses.Should().ContainEquivalentOf(secondCourse);
    }

    [Test]
    public async Task GetCourseByIdAsync_ReturnsCourse_WhenCourseExists()
    {
        await using var dbContext = Fixture.CreateDbContext();
        var repository = new CourseRepository(dbContext);
        var course = CreateCourse("Databases", "PostgreSQL");
        await repository.CreateCourseAsync(course);

        var foundCourse = await repository.GetCourseByIdAsync(course.Id);

        foundCourse.Should().BeEquivalentTo(course);
    }

    [Test]
    public async Task GetCourseByIdAsync_ReturnsNull_WhenCourseDoesNotExist()
    {
        await using var dbContext = Fixture.CreateDbContext();
        var repository = new CourseRepository(dbContext);

        var course = await repository.GetCourseByIdAsync(Guid.NewGuid());

        course.Should().BeNull();
    }

    [Test]
    public async Task UpdateCourseAsync_UpdatesExistingCourse()
    {
        await using var dbContext = Fixture.CreateDbContext();
        var repository = new CourseRepository(dbContext);
        var course = CreateCourse("Old title", "Old description");
        await repository.CreateCourseAsync(course);
        var update = new Course
        {
            Id = course.Id,
            Title = "New title",
            Description = "New description",
        };

        var updatedCourse = await repository.UpdateCourseAsync(update);

        updatedCourse.Should().BeEquivalentTo(update);

        var storedCourse = await repository.GetCourseByIdAsync(course.Id);
        storedCourse.Should().BeEquivalentTo(update);
    }

    [Test]
    public async Task UpdateCourseAsync_ThrowsNotFoundException_WhenCourseDoesNotExist()
    {
        await using var dbContext = Fixture.CreateDbContext();
        var repository = new CourseRepository(dbContext);
        var course = CreateCourse("Missing course", null);

        var act = () => repository.UpdateCourseAsync(course);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task DeleteCourseAsync_RemovesExistingCourse()
    {
        await using var dbContext = Fixture.CreateDbContext();
        var repository = new CourseRepository(dbContext);
        var course = CreateCourse("Course to delete", null);
        await repository.CreateCourseAsync(course);

        await repository.DeleteCourseAsync(course.Id);

        var deletedCourse = await repository.GetCourseByIdAsync(course.Id);
        deletedCourse.Should().BeNull();
    }

    [Test]
    public async Task DeleteCourseAsync_DoesNothing_WhenCourseDoesNotExist()
    {
        await using var dbContext = Fixture.CreateDbContext();
        var repository = new CourseRepository(dbContext);

        await repository.DeleteCourseAsync(Guid.NewGuid());

        var courses = await repository.GetAllCoursesAsync();
        courses.Should().BeEmpty();
    }

    private static Course CreateCourse(string title, string? description)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
        };
    }
}

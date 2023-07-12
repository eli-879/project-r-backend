using NetArchTest.Rules;
using System.Reflection;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "ProjectR.Domain";
    private const string ApplicationNamespace = "ProjectR.Application";
    private const string InfrastructureNamespace = "ProjectR.Infrastructure";
    private const string PresentationNamespace = "ProjectR.Presentation";
    private const string WebNamespace = "ProjectR.WebAPI";

    [Fact]
    public void Domain_Should_Not_HaveDependenceyOnOtherProjects()
    {
        // Arrange
        var assembly = GetAssemblyByProjectName(DomainNamespace);

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var testResults = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert

        testResults.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependenceyOnOtherProjects()
    {
        // Arrange
        var assembly = GetAssemblyByProjectName(ApplicationNamespace);

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var testResults = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert

        testResults.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependenceyOnOtherProjects()
    {
        // Arrange
        var assembly = GetAssemblyByProjectName(InfrastructureNamespace);

        var otherProjects = new[]
        {
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var testResults = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert

        testResults.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_HaveDependenceyOnOtherProjects()
    {
        // Arrange
        var assembly = GetAssemblyByProjectName(PresentationNamespace);

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            WebNamespace
        };

        // Act
        var testResults = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert

        testResults.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_Have_DependencyOnDomain()
    {
        // Arrange
        var assembly = GetAssemblyByProjectName(ApplicationNamespace);

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Controllers_Should_HaveDependenceyOnMediatR()
    {
        // Arrange
        var assembly = GetAssemblyByProjectName(PresentationNamespace);

        // Act
        var testResults = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controlller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        // Assert

        testResults.IsSuccessful.Should().BeTrue();
    }

    private static Assembly GetAssemblyByProjectName(string projectName)
    {
        string projectPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            $"{projectName}.dll");

        return Assembly.LoadFrom(projectPath);
    }

}
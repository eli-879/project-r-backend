using NetArchTest.Rules;
using System.Reflection;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Domain";
    private const string ApplicationNamespace = "Application";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string PresentationNamespace = "Presentation";
    private const string WebNamespace = "Web";

    [Fact]
    public void Domain_Should_Not_HaveDependenceyOnOtherProjects()
    {
        // Arrange
        var assembly = GetAssemblyByProjectName("ProjectR.Domain");

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

    private static Assembly GetAssemblyByProjectName(string projectName)
    {
        Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        return
            loadedAssemblies
            .FirstOrDefault(assembly =>
                !assembly.IsDynamic &&
            !string.IsNullOrEmpty(assembly.Location) &&
                assembly.GetName().Name == projectName) ??
                throw new FileNotFoundException($"No assembly could be found with {projectName} project name.");
    }

}
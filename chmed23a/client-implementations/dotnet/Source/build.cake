//////////////////////////////////////////////////////////////////////
// ADD-INS
//////////////////////////////////////////////////////////////////////
#addin nuget:?package=Cake.Git&version=3.0.0
#addin nuget:?package=Cake.Coverlet&version=3.0.4

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var _target = Argument("target", "Default");
var _configuration = Argument("configuration", "Release");
var _applicationVersion = Argument("applicationVersion", "0.0.0.1");
var _nugetServer = Argument("nugetServer", "http://shcnbtfs01.hcisolutions.ch/NuGet");

//////////////////////////////////////////////////////////////////////
// PROPERTIES
//////////////////////////////////////////////////////////////////////

var _applicationBaseName = "Emediplan.ChMed23A";

var _artifactsDir = MakeAbsolute(Directory("artifacts"));
var _packagesDir = _artifactsDir.Combine(Directory("packages"));

var _gitRepoDir = Directory("../");

var _testOutputDir = MakeAbsolute(Directory("./test-output"));
var _testCoverageResultsDir = MakeAbsolute(_testOutputDir.Combine(Directory("coverage-results")));
var _testResultsDir = MakeAbsolute(_testOutputDir.Combine(Directory ("test-results")));

var _solutionPath = "Emediplan.ChMed23A.sln";

// Defines the exhaustive list of projects for which a nuget package will be egenerated when executing the 'Pack' target
var _nugetProjects = new [] {
    "Emediplan.ChMed23A.csproj"
};

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
    {
        // Test and coverage reports
        CleanDirectory (_testCoverageResultsDir);
        Information ($"Deleted all content in '{_testCoverageResultsDir}'");
        CleanDirectory (_testResultsDir);
        Information ($"Deleted all content in '{_testResultsDir}'");

        // Artifacts
        CleanDirectory (_artifactsDir);
        Information ($"Deleted all content in '{_artifactsDir}'");

        // Build directories
        CleanDirectories($"**/obj/{_configuration}");
        Information($"Deleted all content in '**/obj/{_configuration}'.");
        CleanDirectories($"**/bin/{_configuration}");
        Information($"Deleted all content in '**/bin/{_configuration}'.");
    });

Task("Restore-NuGet-Packages")
    .Does(() => {
        DotNetRestore(_solutionPath);
        Information($"Restored nuget packages for solution '{_solutionPath}'.");
    });
	
Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() => {
        DotNetBuild(
            _solutionPath,
            new DotNetBuildSettings{
                NoRestore = true,
                Configuration = _configuration
            }
        );
        Information($"Finished building solution '{_solutionPath}'.");
    });

Task("Release-Prepare")
    .Does(() => {
        // Compute the package version
        var gitCommitHash = GitLogTip(_gitRepoDir)?.Sha;
        var gitCommitHashShort = gitCommitHash?.Substring(0, 8) ?? "Unknown";
        var currentDateTime = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var packageVersion = $"{_applicationVersion}-{gitCommitHashShort}-{currentDateTime}";

        // Versions having to be modified in *.csproj
        var versionXPath = "/Project/PropertyGroup/Version";
        var assemblyVersionXPath = "/Project/PropertyGroup/AssemblyVersion";
        var fileVersionXPath = "/Project/PropertyGroup/FileVersion";

        // Set versions for all projects (except testing)
        var projectFilePaths = GetFiles("./**/*.csproj");
        foreach(var projectFilePath in projectFilePaths) {
            if (projectFilePath.FullPath.Contains("Tests")
                || projectFilePath.FullPath.Contains("Examples")) {
                // Do not version test projects
                continue;
            }

            XmlPoke(projectFilePath, versionXPath, packageVersion);
            XmlPoke(projectFilePath, assemblyVersionXPath, _applicationVersion);
            XmlPoke(projectFilePath, fileVersionXPath, _applicationVersion);
        }

        Information ($"Updated projects with AssemblyVersion/FileVersion='{_applicationVersion}' and Version='{packageVersion}'.");
    });

Task("Unit-Test")
    .IsDependentOn("Build")
    .Does (() => {
        RunTests(GetFiles($"./**/{_applicationBaseName}*.Tests.Unit.csproj"));
    });

Task("Integration-Test")
    .IsDependentOn("Build")
    .Does (() => {
        RunTests(GetFiles($"./**/{_applicationBaseName}*.Tests.Integration.csproj"));
    });

Task("Test")
    .IsDependentOn("Build")
    .IsDependentOn("Unit-Test")
    .IsDependentOn("Integration-Test");

var _testSettings = new DotNetTestSettings {
    NoBuild = true,
    NoRestore = true,
    Configuration = _configuration,
    ResultsDirectory = _testResultsDir
};

var _coverletSettings = new CoverletSettings {
    CollectCoverage = true,
    CoverletOutputFormat = CoverletOutputFormat.cobertura,
    CoverletOutputDirectory = _testCoverageResultsDir,
    Exclude = new List<string> { "[xunit*]*", $"[{_applicationBaseName}.Tests.*]*" },
    Include = new List<string> { $"[{_applicationBaseName}*]*" }
};

private void RunTests(FilePathCollection projectFiles) {
    foreach(var projectFile in projectFiles)
    {
        var projectFilePath = projectFile.Segments.Last();

        // Test result customization
        var testResultFileName = $"test-results-{projectFilePath}.xml";
        _testSettings.ArgumentCustomization = args => args.Append($"--logger:trx;LogFileName={testResultFileName}");

        // Coverage result customization
        var coverageResultFileName = $"coverage-results-{projectFilePath}.cobertura.xml";
        _coverletSettings.CoverletOutputName = coverageResultFileName;

        DotNetTest(projectFile.FullPath, _testSettings, _coverletSettings);
    }
}

////////////////////////////////////////////
// Nuget packages
////////////////////////////////////////////
Task("Pack")
    .IsDependentOn("Clean")
    .IsDependentOn("Release-Prepare")
    .IsDependentOn("Build")
    .Does(() => {
        var settings = new DotNetPackSettings
        {
            Configuration = _configuration,
            NoBuild = true,
            NoRestore = true,
            IncludeSymbols = false,
            OutputDirectory = _packagesDir,
            MSBuildSettings = new DotNetMSBuildSettings()
                .WithProperty("PackageVersion", _applicationVersion)
                .WithProperty("Copyright", $"Copyright HCI Solutions AG {DateTime.Now.Year}")
        };

		// Pack defined projects
        var projectFilePaths = GetFiles("./**/*.csproj");
        foreach(var projectFile in projectFilePaths) {
            if(!_nugetProjects.Any(nugetProject => projectFile.FullPath.EndsWith(nugetProject))) {
                continue;
            }
            
            DotNetPack(projectFile.FullPath, settings);
            Information($"Packed '{projectFile.FullPath}'");
        }

        Information($"Created nuget packages for defined projects.");
    });

Task("NuGetPush")
    .IsDependentOn("Pack")
    .Does(() => {
        var settings = new NuGetPushSettings{
            Source = _nugetServer,
        };

        GetFiles(_packagesDir + "/*.nupkg")
            .ToList()
            .ForEach(nugetFile => {
                NuGetPush(nugetFile.FullPath, settings);
                Information($"Pushed '{nugetFile.FullPath}'");
            });

        Information($"Pushed all nuget packages");
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(_target);
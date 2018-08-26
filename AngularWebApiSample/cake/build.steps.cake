// addins
#addin nuget:?package=Cake.CodeAnalysisReporting&prerelease
#addin nuget:?package=Cake.FileHelpers&prerelease
#addin nuget:?package=Cake.Issues&prerelease
#addin nuget:?package=Cake.Issues.EsLint&prerelease
#addin nuget:?package=Cake.Issues.InspectCode&prerelease
#addin nuget:?package=Cake.Issues.MSBuild&prerelease
#addin nuget:?package=Cake.Issues.Reporting&prerelease
#addin nuget:?package=Cake.Issues.Reporting.Generic&prerelease
#addin nuget:?package=Cake.Issues.Testing&prerelease
#addin nuget:?package=Cake.MicrosoftTeams&prerelease
#addin nuget:?package=Cake.Npm&prerelease
#addin nuget:?package=Cake.Yarn&prerelease
#addin nuget:?package=Cake.Path&prerelease
#addin nuget:?package=Cake.Powershell&prerelease
#addin nuget:?package=Cake.ReSharperReports&prerelease
#addin nuget:?package=Cake.Sonar
#addin nuget:?package=Cake.StyleCop&prerelease

// tools
#tool nuget:?package=JetBrains.dotCover.CommandLineTools
#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2018.1.4
#tool nuget:?package=Microsoft.CodeDom.Providers.DotNetCompilerPlatform
#tool nuget:?package=Microsoft.IdentityModel&version=6.1.7600.16394&loaddependencies=true
#tool nuget:?package=Microsoft.Net.Compilers&version=2.8.2&loaddependencies=true
#tool nuget:?package=Microsoft.PowerShell.5.ReferenceAssemblies&version=1.1.0&loaddependencies=true
#tool nuget:?package=MSBuild.Extension.Pack
#tool nuget:?package=MSBuild.SonarQube.Runner.Tool
#tool nuget:?package=NuGet.CommandLine
#tool nuget:?package=System.Collections.Immutable&version=1.3.1&loaddependencies=true
#tool nuget:?package=System.Runtime.InteropServices.RuntimeInformation&loaddependencies=true
#tool nuget:?package=xunit.runner.console&version=2.4.0&loaddependencies=true

// usings
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Cake.Common.IO;
using Cake.Issues;
using Cake.Common.Tools.XUnit;
using Cake.Core.Configuration;
using Cake.Common.Xml;

DirectoryPath rootPath;
FilePath solutionPath;
DirectoryPath artifactsPath;
DirectoryPath webPath;
IFile solution;
IDirectory artifactsDirectory;

// methods
void SetupPaths(
    out DirectoryPath rootPath,
    out FilePath solutionPath,
    out DirectoryPath artifactsPath,
    out DirectoryPath webPath,
    out IFile solution,
    out IDirectory artifactsDirectory)
    {
        rootPath = new DirectoryPath(RootFolder);

        solutionPath = rootPath.CombineWithFilePath(new FilePath(SolutionFileName));
        Information(artifactsDirectoryName);
        artifactsPath = rootPath.Combine(new DirectoryPath(artifactsDirectoryName));
        Information(artifactsPath);
        webPath = rootPath.Combine(new DirectoryPath(WebDirectoryName));

        solution = Context.FileSystem.GetFile(solutionPath);
        artifactsDirectory = Context.FileSystem.GetDirectory(artifactsPath);

        Information(
            "IFileSystem: {0}\r\n\tIFile: {1}\r\n\tIDirectory: {2}",
            Context.FileSystem.GetType(),
            solution.GetType(),
            artifactsDirectory.GetType());
    }  

// tasks
Task("Clean-ObjBin")
  .Does(()=>{
    var masks = new []{
        "/src/**/obj",
        "/src/**/bin",
        "/test/**/obj",
        "/test/**/bin"
    };

    masks.Select(m => $"{RootFolder}{m}").ToList().ForEach(m => CleanDirectories(m));
  });

Task("Clean-Artifacts")
  .Does(()=>{
    CleanDirectory(artifactsPath);
  });


Task("Clean-Sonarqube")
  .WithCriteria(BuildSystem.IsLocalBuild)
  .Does(()=>{
    var sonarQubePath = artifactsPath.Combine(new DirectoryPath("../.sonarqube"));
    CleanDirectory(sonarQubePath);
  });   
  
  
Task("Clean")
  .WithCriteria(BuildSystem.IsLocalBuild)
  .IsDependentOn("Clean-ObjBin")
  .IsDependentOn("Clean-Artifacts")
  .IsDependentOn("Clean-Sonarqube");

Task("Npm-Install-rimraf")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .Does(() => {
        var settings = new NpmInstallSettings {
            Global = true
        };
        settings.AddPackage("rimraf");
        NpmInstall(settings);
    });

Task("Npm-Install-typescript")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .Does(() => {
        var settings = new NpmInstallSettings {
            Global = true
        };
        settings.AddPackage("typescript");
        NpmInstall(settings);
    });

Task("Npm-Install-tslint")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .Does(() => {
        var settings = new NpmInstallSettings {
            Global = true
        };
        settings.AddPackage("tslint");
        NpmInstall(settings);
    });

Task("Npm-Install-webpack")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .Does(() => {
        var settings = new NpmInstallSettings {
            Global = true
        };
        settings.AddPackage("webpack");
        NpmInstall(settings);
    });

Task("Npm-Install-angularcli")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .Does(() => {
        var settings = new NpmInstallSettings {
            Global = true
        };
        settings.AddPackage("@angular/cli");
        NpmInstall(settings);
    });  

Task("Npm-Install-yarn")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .Does(() => {
        var settings = new NpmInstallSettings {
            Global = true
        };
        settings.AddPackage("yarn");
        NpmInstall(settings);
    });      

Task("Npm-Install-Tools")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .IsDependentOn("Npm-Install-rimraf")
    .IsDependentOn("Npm-Install-typescript")
    .IsDependentOn("Npm-Install-tslint")
    .IsDependentOn("Npm-Install-webpack")
    .IsDependentOn("Npm-Install-angularcli")
    .IsDependentOn("Npm-Install-yarn")
    ;  

Task("Npm-Clean")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .Does(() => {
        var nodeModulesPath = webPath.Combine(new DirectoryPath("ClientApp/node_modules"));
        Information(nodeModulesPath);
        if(Context.FileSystem.Exist(nodeModulesPath)) {
            var directory = Context.FileSystem.GetDirectory(nodeModulesPath);
            directory.Delete(true);
        }
    });

Task("Yarn-Install")
    .Does(() => {
        var clientAppPath = webPath.Combine(new DirectoryPath("ClientApp"));
        Information(clientAppPath.FullPath);
        Yarn.Install(settings => settings.WorkingDirectory = clientAppPath.FullPath);
    });

Task("Create-Artifacts-Dir")    
    .Does(() => {
        if(!Context.FileSystem.Exist(artifactsPath)) {
            var directory = Context.FileSystem.GetDirectory(artifactsPath);
            directory.Create();
        }
    });

Task("Yarn-Tslint")
    .Does(() => {
        var clientAppPath = webPath.Combine(new DirectoryPath("ClientApp"));
        Yarn.RunScript("ts-lint", settings => settings.WorkingDirectory = clientAppPath.FullPath);
    });

Task("Yarn-Build")
    .Does(() => {
        var clientAppPath = webPath.Combine(new DirectoryPath("ClientApp"));
        Yarn.RunScript("build", settings => settings.WorkingDirectory = clientAppPath.FullPath);
    });

Task("Yarn-Coverage")
    .Does(() => {
        var clientAppPath = webPath.Combine(new DirectoryPath("ClientApp"));
        Yarn.RunScript("test:coverage", settings => settings.WorkingDirectory = clientAppPath.FullPath);
    });

Task("Nuget-Restore")
    .Does(()=>{
        var nugetPath = Context.Tools.Resolve("nuget.exe");
        var nugetRestoreSettings = new NuGetRestoreSettings {
            ToolPath = nugetPath,
       }; 
       NuGetRestore(solutionPath, nugetRestoreSettings);
    });

Task("Sonar-Begin")
  .Does(() => {
    var artifactsPathAbsolute = artifactsPath.MakeAbsolute(Context.Environment);
    var dotCoverPath = artifactsPathAbsolute.CombineWithFilePath(new FilePath("DotCover.html"));
    var nunitReportsPath = $"{artifactsPathAbsolute.FullPath}/*.nunit.xml";
    var exclusions = new List<string>{
        "*Designer.cs",
        "**/wwwroot/*",
        "**/jest.config.js",
        "**/App_Data/*",
        "**/*.spec.ts"
    };
    
    var tslintReportPath = artifactsPathAbsolute.CombineWithFilePath(new FilePath("tslint-result.json")).FullPath;
    var tsjestReportPath = artifactsPathAbsolute.CombineWithFilePath(new FilePath("frontend-test-report.xml")).FullPath;
    var clientAppPathAbsolute = webPath.Combine(new DirectoryPath("ClientApp/src")).MakeAbsolute(Context.Environment);

    SonarBegin(new SonarBeginSettings {
        Key = "AngularWebApiSample",
        Name = "AngularWebApiSample",
        Version = "1.0",
        Url = "https://localhost:9000",
        Login = "local",
        Password = "local",
        DotCoverReportsPath = dotCoverPath.FullPath,
        Exclusions = string.Join(",", exclusions.ToArray()),
        NUnitReportsPath = nunitReportsPath,
        TypescriptCoverageReportsPath = artifactsPathAbsolute
            .Combine(new DirectoryPath("tscoverage"))
            .CombineWithFilePath(new FilePath("lcov.info")).FullPath,
        ArgumentCustomization =
            args => args.Append("/d:sonar.scm.disabled=true")
                        .Append($"/d:sonar.typescript.tslint.reportPaths={tslintReportPath}")
                        .Append($"/d:sonar.testExecutionReportPaths={tsjestReportPath}")
                        .Append($"/d:sonar.tests={clientAppPathAbsolute.FullPath}")
                        .Append($"/d:sonar.test.inclusions={clientAppPathAbsolute.FullPath}/**/*.spec.ts")
    });
  });

Task("MSBuild")
  .Does(() => {
    var logDir = artifactsPath.CombineWithFilePath(new FilePath("msbuild.log"));
    var msbuildPath = Context.Tools.Resolve("MSBuild.exe");
    var msbuildSettings = new MSBuildSettings()
    //.WithProperty("RunCodeAnalysis","true")
    .WithProperty("DebugType","full")
    .WithProperty("TreatWarningsAsErrors","false")
    .WithTarget("Build")
    .SetMaxCpuCount(0)
    .SetConfiguration(configuration)
    .SetNodeReuse(false)
    .WithLogger(
        Context.Tools.Resolve("MSBuild.ExtensionPack.Loggers.dll").FullPath,
        "XmlFileLogger",
        string.Format(
            "logfile=\"{0}\";verbosity=Detailed;encoding=UTF-8",
            logDir.ToString()));

    MSBuild(solutionPath, msbuildSettings);
    
  });

private FilePath GetXunitConsoleDllFilePath(ICakeContext context) {
    var cakeConfigurationProvider = new CakeConfigurationProvider(context.FileSystem, context.Environment);
    var cakeConfiguration = cakeConfigurationProvider.CreateConfiguration(
        new DirectoryPath("./"),
        new Dictionary<string,string>());
    var toolPath = cakeConfiguration.GetToolPath(new DirectoryPath("./"), context.Environment);
    var toolDirectory = context.FileSystem.GetDirectory(toolPath);
    var xunitConsole = "xunit.runner.console";
    var regex = new System.Text.RegularExpressions.Regex($@"{xunitConsole}(\.\d.*)?$");
    var localToolDir = toolDirectory.GetDirectories(
        $"{xunitConsole}*",
        SearchScope.Current,
        (fsi) => regex.IsMatch(fsi.Path.FullPath)).LastOrDefault();
    var localToolPath = localToolDir.Path.Combine(new DirectoryPath("tools/netcoreapp2.0"));
    var xunitDll = "xunit.console.dll";
    var xunitPath = localToolPath.CombineWithFilePath(new FilePath(xunitDll));
    return xunitPath;
}

private FilePath ExtractXslt(FilePath xunitPath, string name) {
    var xsltOutputPath = artifactsPath.CombineWithFilePath(new FilePath(name));
    using(var resource = System.Reflection.Assembly.LoadFile(xunitPath.FullPath).GetManifestResourceStream(name))
    using(var file = new FileStream(xsltOutputPath.FullPath, FileMode.Create, FileAccess.Write))
    {
        resource.CopyTo(file);
    }

    return xsltOutputPath;
}

Task("DotCover-Analysis")
    .Does(() => {
        var names = new[] {
            "AngularWebApiSample.Test",
        };

        var xunitPath = GetXunitConsoleDllFilePath(Context);

        var nunitXsltOutputPath = ExtractXslt(xunitPath, "Xunit.ConsoleClient.NUnitXml.xslt");
        var htmlXsltOutputPath = ExtractXslt(xunitPath, "Xunit.ConsoleClient.HTML.xslt");
        
        var rootPathAbsolute = rootPath.MakeAbsolute(Context.Environment);

        foreach(var name in names) {
            var workFolder = rootPathAbsolute.Combine(new DirectoryPath($"test/{name}/bin/{configuration}/netcoreapp2.1"));
            var scope = $"{workFolder.FullPath}/AngularWebApiSample*.dll";
            Information(scope);

            var dotCoverCoverSettings = new DotCoverCoverSettings()
                .WithScope(scope)
                .WithFilter("+:AngularWebApiSample*")
                .WithFilter("-:*Test");

            DotCoverCover(tool => {
                var filePath = workFolder.CombineWithFilePath(new FilePath($"{name}.dll"));
                var xmlXunitPath = artifactsPath.CombineWithFilePath(new FilePath($"{name}.xunit.xml")).FullPath;
                var settings = new DotNetCoreExecuteSettings {
                    FrameworkVersion = "2.1.0"
                };

                tool.DotNetCoreExecute(
                    xunitPath,
                    $"{filePath.FullPath} -xml {xmlXunitPath} -parallel collections -maxthreads unlimited -noshadow -notrait \"Category=Skip\"",
                    settings);
            },
            artifactsPath.CombineWithFilePath(new FilePath($"{name}.dcvr")),
            dotCoverCoverSettings);
        }

        var dvcrsFiles = names.Select(n => artifactsPath.CombineWithFilePath(new FilePath($"{n}.dcvr")));
        var target = artifactsPath.CombineWithFilePath(new FilePath("DotCover.dcvr"));
        DotCoverMerge(dvcrsFiles, target);

        var xmlTransformationSettings = new XmlTransformationSettings();
        xmlTransformationSettings.Indent = true;
        xmlTransformationSettings.IndentChars = "    ";
        xmlTransformationSettings.NewLineHandling = NewLineHandling.Replace;
        xmlTransformationSettings.NewLineChars = "\r\n";
        xmlTransformationSettings.Overwrite = true;

        foreach(var name in names) {
            var xmlXunitPath = artifactsPath.CombineWithFilePath(new FilePath($"{name}.xunit.xml")).FullPath;
            var xmlPath = artifactsPath.CombineWithFilePath(new FilePath($"{name}.nunit.xml")).FullPath;
            var htmlPath = artifactsPath.CombineWithFilePath(new FilePath($"{name}.html")).FullPath;
            XmlTransform(nunitXsltOutputPath, xmlXunitPath, xmlPath, xmlTransformationSettings);
            XmlTransform(htmlXsltOutputPath, xmlXunitPath, htmlPath, xmlTransformationSettings);
        }
    });

Task("DotCover-Report")
    .Does(() => {
        var source = artifactsPath.CombineWithFilePath(new FilePath("DotCover.dcvr"));
        var destination = artifactsPath.CombineWithFilePath(new FilePath("DotCover.html"));
        var settings = new DotCoverReportSettings {
            ReportType = DotCoverReportType.HTML
        };
        DotCoverReport(
            source,
            destination,
            settings);
    });

Task("Sonar-End")
    .Does(() => {
        SonarEnd(new SonarEndSettings{
            Login = "local",
            Password = "local"
        });
    });

Task("StyleCop")
    .Does(() => {
        // stylecop settings file
        var settingsFilePath = rootPath.CombineWithFilePath(new FilePath("Settings.stylecop"));
        
        // xml results file
        var resultFile = artifactsPath.CombineWithFilePath(new FilePath("StylecopResults.xml"));

        // html report file
        var htmlFile = artifactsPath.CombineWithFilePath(new FilePath("StylecopResults.html"));

        StyleCopAnalyse(settings => settings
            .WithSolution(solutionPath)
            .WithSettings(settingsFilePath)
            .ToResultFile(resultFile)
            .ToHtmlReport(htmlFile)
        );
    });

Task("Code-Analysis")
    .Does(() => {
        var logDir = artifactsPath.CombineWithFilePath(new FilePath("msbuild.log"));
        var issuesByAssembly = artifactsPath.CombineWithFilePath(new FilePath("issuesByAssembly.html"));
        var issuesByRule = artifactsPath.CombineWithFilePath(new FilePath("issuesByRule.html"));
        CreateMsBuildCodeAnalysisReport(
            logDir,
            Cake.CodeAnalysisReporting.CodeAnalysisReport.MsBuildXmlFileLoggerByAssembly,
            issuesByAssembly);

        CreateMsBuildCodeAnalysisReport(
            logDir,
            Cake.CodeAnalysisReporting.CodeAnalysisReport.MsBuildXmlFileLoggerByRule,
            issuesByRule);
    });

Task("Resharper-DupFinder")
    .Does(() => {
        var projectMask = $"{RootFolder}/**/*.csproj";
        var designerMask = $"{MakeAbsolute(Directory(RootFolder))}/**/*Designer.cs";
        var projects = GetFiles(projectMask);
        var settings = new DupFinderSettings {
            ShowStats = true,
            ShowText = true,
            ExcludePattern = new string[] {
               designerMask,
            },
            OutputFile = artifactsPath.CombineWithFilePath(new FilePath("dupfinder-output.xml")),
            ThrowExceptionOnFindingDuplicates = false
        };
        DupFinder(projects, settings);
    });

Task("Resharper-DupFinder-Reports")
    .Does(() => {
        ReSharperReports(
            artifactsPath.CombineWithFilePath(new FilePath("dupfinder-output.xml")),
            artifactsPath.CombineWithFilePath(new FilePath("dupfinder-output.html")));
    });    

Task("Resharper-InspectCode")
    .Does(() => {
        var msBuildProperties = new Dictionary<string, string>();
        var settings = new InspectCodeSettings {
            SolutionWideAnalysis = true,
            Profile = rootPath.CombineWithFilePath(new FilePath("AngularWebApiSample.sln.DotSettings")),
            MsBuildProperties = msBuildProperties,
            OutputFile = artifactsPath.CombineWithFilePath(new FilePath("inspectcode-output.xml")),
            ThrowExceptionOnFindingViolations = false
        };
        InspectCode(solutionPath, settings);
    });  

Task("Resharper-InspectCode-Reports")
    .Does(() => {
        ReSharperReports(
            artifactsPath.CombineWithFilePath(new FilePath("inspectcode-output.xml")),
            artifactsPath.CombineWithFilePath(new FilePath("inspectcode-output.html"))
        );
    });

Task("Issues")
    .Does(() => {
        var eslintcodepath = artifactsPath.CombineWithFilePath(new FilePath("tslint-result.json"));
        var inspectcodepath = artifactsPath.CombineWithFilePath(new FilePath("inspectcode-output.xml"));
        var dupfinderpath = artifactsPath.CombineWithFilePath(new FilePath("dupfinder-output.xml"));
        var msbuildlogpath = artifactsPath.CombineWithFilePath(new FilePath("msbuild.log"));
        
        var issues = ReadIssues(new List<IIssueProvider>
        {
            // EsLintIssuesFromFilePath(
            //     eslintcodepath,
            //     EsLintJsonFormat),
            InspectCodeIssuesFromFilePath(
                inspectcodepath),
            // InspectCodeIssuesFromFilePath(
            //     dupfinderpath),
            // MsBuildIssuesFromFilePath(
            //     msbuildlogpath,
            //     MsBuildXmlFileLoggerFormat),
            
        },
        topFolder);

        var outputPath = artifactsPath.CombineWithFilePath(new FilePath("IssuesReport.html"));
        var settings = new CreateIssueReportSettings(topFolder, outputPath);
        CreateIssueReport(
            issues,
            GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
            settings);
    });  

Task("Bootstrap")
    .Does(() => {});

  
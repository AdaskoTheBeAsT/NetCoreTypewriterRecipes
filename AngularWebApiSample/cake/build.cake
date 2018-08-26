//modules
#module nuget:?package=Cake.LongPath.Module&loaddependencies=true&prerelease

// subcomponents
#l "./build.steps.cake"

// arguments
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var topFolder = Argument<string>("topFolder",  "..");

// variables
IDirectory directory;
IFile file;
var isRunningOnBamboo = Bamboo.IsRunningOnBamboo;

//  related
var SolutionFileName = "AngularWebApiSample.sln";
var artifactsDirectoryName = "cake/.artifacts";
var WebDirectoryName = "src/AngularWebApiSample";
var RootFolder = Argument<string>("RootFolder",  "..");

// setup / teardown
Setup(ctx =>
{
  // Executed BEFORE the first task.
  Information(" build started...");

  // long file support
  file = Context.FileSystem.GetFile("build.cake");
  directory = Context.FileSystem.GetDirectory("tools");
  
  //SetupCommon(out toolPath);

  SetupPaths(
    out rootPath,
    out solutionPath,
    out artifactsPath,
    out webPath,
    out solution,
    out artifactsDirectory);

  Information(
      "IFileSystem: {0}\r\n\tIFile: {1}\r\n\tIDirectory: {2}",
      Context.FileSystem.GetType(),
      file.GetType(),
      directory.GetType());
});

Teardown(ctx =>
{
  // Executed AFTER the last task.
  Information(" build completed.");
});

Task("Default")
  .IsDependentOn("Clean")
  //.IsDependentOn("Npm-Install-Tools")
  .IsDependentOn("Npm-Clean")
  .IsDependentOn("Yarn-Install")
  .IsDependentOn("Create-Artifacts-Dir")
  .IsDependentOn("Yarn-Tslint")
  .IsDependentOn("Yarn-Build")
  .IsDependentOn("Yarn-Coverage")  
  .IsDependentOn("Nuget-Restore")
  //.IsDependentOn("Sonar-Begin")
  .IsDependentOn("MSBuild")
  //.IsDependentOn("DotCover-Analysis")
  //.IsDependentOn("DotCover-Report")
  //.IsDependentOn("Sonar-End")
  //#.IsDependentOn("StyleCop") 
  .IsDependentOn("Code-Analysis")
  .IsDependentOn("Resharper-DupFinder")
  //#.IsDependentOn("Resharper-DupFinder-Reports")
  .IsDependentOn("Resharper-InspectCode")
  //#.IsDependentOn("Resharper-InspectCode-Reports")
  //.IsDependentOn("Issues")
  ;

Task("BackendBuild")
  .IsDependentOn("Nuget-Restore")
  //.IsDependentOn("Sonar-Begin")
  .IsDependentOn("MSBuild")
  .IsDependentOn("DotCover-Analysis")
  .IsDependentOn("DotCover-Report")
  //.IsDependentOn("Sonar-End")
  //#.IsDependentOn("StyleCop") 
  .IsDependentOn("Code-Analysis")
  .IsDependentOn("Resharper-DupFinder")
  //#.IsDependentOn("Resharper-DupFinder-Reports")
  .IsDependentOn("Resharper-InspectCode")
  //#.IsDependentOn("Resharper-InspectCode-Reports")
  //.IsDependentOn("Issues")
  ;

Task("Sonar")
  .IsDependentOn("Nuget-Restore")
  .IsDependentOn("Sonar-Begin")
  .IsDependentOn("MSBuild")
  .IsDependentOn("DotCover-Analysis")
  .IsDependentOn("DotCover-Report")
  .IsDependentOn("Sonar-End")
  ;

Task("Build")
  .IsDependentOn("Yarn-Install")
  .IsDependentOn("Create-Artifacts-Dir")
  .IsDependentOn("Yarn-Tslint")
  .IsDependentOn("Yarn-Build")
  .IsDependentOn("Yarn-Coverage")  
  .IsDependentOn("Nuget-Restore")
  .IsDependentOn("Sonar-Begin")
  .IsDependentOn("MSBuild")
  .IsDependentOn("DotCover-Analysis")
  .IsDependentOn("DotCover-Report")
  .IsDependentOn("Sonar-End")
  ;

// tasks
RunTarget(target);
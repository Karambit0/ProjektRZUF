var target = Argument("target","Publish");
var configuration = Argument("configuration","Release");
var solutionFolder = "./";
var outputFolder = "./files";

Task("Clean")
    .Does(() => {
        CleanDirectory(outputFolder);
    });
Task("CleanRzuf")
    .Does(() => {
        CleanDirectory("./rzuf/bin");
        CleanDirectory("./rzuf/obj");
    });
Task("Restore")
    .Does(() => {
        DotNetRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Restore")
    .IsDependentOn("Clean")
    .Does(() => {
        DotNetBuild(solutionFolder, new DotNetBuildSettings
        {
            NoRestore = true,
            Configuration = configuration
        });
    });

Task("Publish")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetPublish(solutionFolder, new DotNetPublishSettings
        {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true,
            OutputDirectory = outputFolder
        });
    });

RunTarget(target);
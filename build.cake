var target = Argument("target","Publish");
var configuration = Argument("configuration","Release");
var solutionFolder = "./";
var outputFolder = "./files";
var rzufFolder = "./rzuf";

Task("Clean")
    .Does(() => {
        CleanDirectory(outputFolder);
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
        CreateDirectory(outputFolder+"/resources");
        CopyDirectory(rzufFolder+"/resources",outputFolder+"/resources");
        CleanDirectory(rzufFolder+"/bin");
        CleanDirectory(rzufFolder+"/obj");
    });

RunTarget(target);
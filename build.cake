#addin nuget:?package=Cake.Git
#load "./buildConfig.cake"

var config = BuildConfig.Create(Context, BuildSystem);

void DotNetCoreTestGlob(string glob)
{
	var directories = GetFiles(glob + "*.Tests/project.json");
	
	foreach(var directory in directories)
	{
	    Information("Found directory {0}", directory);

		var directoryName = directory.GetDirectory().Segments.Last();
		var resultsFile = "./artifacts/testResults-" + directoryName + ".xml";
		var arg = string.Format("-xml \"{0}\"", resultsFile);
		var settings = new DotNetCoreTestSettings(){
			ArgumentCustomization = args => args.Append(arg)
		};
		DotNetCoreTest(directory.FullPath, settings);
	}
}

bool IsTestable(string path) 
{
	var projectFile = path + "\\project.json";
	return System.IO.File.Exists(projectFile);
}

Setup(context => {
    Information("Preparing to build version {0}:", config.SemVer);
});

Task(BuildTasks.UpdateVersion)
    .Does(() => {
        var files = GetFiles("./**/project.json");
        foreach(var file in files)
        {
            Information("Bumping version: {0}", file);

            var path = file.ToString();
            var trg = new StringBuilder();
            var regExVersion = new System.Text.RegularExpressions.Regex("\"version\":(\\s)?\"\\d.\\d.\\d-\\*\",");
            using (var src = System.IO.File.OpenRead(path))
            {
                using (var reader = new StreamReader(src))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if(line == null)
                            continue;

                        line = regExVersion.Replace(line, string.Format("\"version\": \"{0}-*\",", config.Version));

                        trg.AppendLine(line);
                    }
                }
            }

            System.IO.File.WriteAllText(path, trg.ToString());
        }
    });


Task(BuildTasks.UpdateAppVeyor)
    .WithCriteria(() => config.IsCiBuild)
    .Does(() => { 
        AppVeyor.UpdateBuildVersion(config.SemVer +"+" + AppVeyor.Environment.Build.Number);
    });

Task(BuildTasks.Clean)
    .Does(() => {
        CleanDirectory(config.ArtifactsDir);
        // CleanDirectory("./**/bin/");
        // CleanDirectory("./**/obj/");
    });

Task(BuildTasks.Build)
    .IsDependentOn(BuildTasks.Clean)
    .IsDependentOn(BuildTasks.UpdateVersion)
    .Does(() => {
        DotNetCoreRestore();

        var buildSettings = new DotNetCoreBuildSettings(){
            Configuration = config.Configuration,
            VersionSuffix = config.Suffix,
            NoIncremental = true,
        };

        DotNetCoreBuild(config.SrcDir + "**/project.json");
        DotNetCoreBuild(config.TestDir + "**/project.json");
    });

Task(BuildTasks.Test)
    .IsDependentOn(BuildTasks.Build)
    .Does(() => {
        DotNetCoreTestGlob(config.TestDir);
    });

Task(BuildTasks.Package)
    .IsDependentOn(BuildTasks.Test)
    .Does(() =>{
        var settings = new DotNetCorePackSettings {
            Configuration = config.Configuration,
            OutputDirectory = config.ArtifactsDir,
            VersionSuffix = config.Suffix
        };

        DotNetCorePack(config.SrcDir + "FluentModelBuilder", settings);
        DotNetCorePack(config.SrcDir + "FluentModelBuilder.Relational", settings);
    });

Task(BuildTasks.UploadArtifacts)
    .IsDependentOn(BuildTasks.Package) 
    .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
            {
                var artifacts = GetFiles(config.ArtifactsDir + "*.nupkg");
				AppVeyor.AddInformationalMessage("Uploading artifacts");
                foreach(var artifact in artifacts.Select(x => x.ToString()))
                {
					var fileInfo = System.IO.Path.GetFileName(artifact);
					Information("Adding " + fileInfo);
                    AppVeyor.UploadArtifact(artifact, settings => settings.SetArtifactType(AppVeyorUploadArtifactType.NuGetPackage));
                }
            }
    );

Task(BuildTasks.UploadTestResults)
    .IsDependentOn(BuildTasks.UpdateAppVeyor)
	.IsDependentOn(BuildTasks.Test)
	.WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
	.Does(() => {
		var testResults = GetFiles(config.ArtifactsDir + "testResults*.xml");
		foreach(var result in testResults)
		{
			AppVeyor.UploadTestResults(result, AppVeyorTestResultsType.XUnit);
		}
	});

Task(BuildTasks.Publish)
	.IsDependentOn(BuildTasks.UploadTestResults)
    .IsDependentOn(BuildTasks.UploadArtifacts)
    .Does(() => {
        Information("Published");
    });

Task(BuildTasks.Default)
    .IsDependentOn(BuildTasks.Test);

RunTarget(config.Target);

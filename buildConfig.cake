#tool "nuget:?package=GitVersion.CommandLine"
#addin nuget:?package=Cake.Git

public class BuildConfig
{
    public readonly string SrcDir = "./src/";
    public readonly string TestDir = "./test/";
    public readonly string ArtifactsDir = "./artifacts/";
    
    public string Target  { get; private set; }
    
    public string SemVer  { get; private set; }
    public string Suffix  { get; private set; }
    public string Version { get; private set; }
    public string Configuration { get; private set; }

    public bool IsCiBuild { get; private set; }

    public static BuildConfig Create(ICakeContext context, BuildSystem buildSystem)
    {
        return new BuildConfig(context, buildSystem);
    }

    private BuildConfig(ICakeContext context, BuildSystem buildSystem)
    {
        if (context == null)
            throw new ArgumentNullException("context");

        InitializeConfig(context, buildSystem);
    }

    private void InitializeConfig(ICakeContext context, BuildSystem buildSystem)
    {
        Target = context.Argument("target", BuildTasks.Default);
        Configuration = context.Argument("configuration", Configurations.Release);
        IsCiBuild = IsRunningOnAppVeyor(buildSystem);
        
        var gitVersion = GetSemanticVersion(context);
        Version = gitVersion.MajorMinorPatch;
        SemVer = gitVersion.LegacySemVerPadded + gitVersion.BuildMetaDataPadded;
        Suffix = gitVersion.PreReleaseLabel + gitVersion.PreReleaseNumber.PadLeft(3, '0') + gitVersion.BuildMetaDataPadded;
    }

    private bool IsRunningOnAppVeyor(BuildSystem buildSystem) 
    {
        return buildSystem.AppVeyor.IsRunningOnAppVeyor;
    }

    private GitVersion GetSemanticVersion(ICakeContext context)
    {
        var gitVersionSettings = new GitVersionSettings(){
            ArgumentCustomization = args => args.Append("/nofetch")
        };
        return context.GitVersion(gitVersionSettings);
    }
}

public class BuildTasks
{
    public static string UpdateVersion = "Update-Version";
    public static string UpdateAppVeyor = "Update-AppVeyor";

    public static string Clean = "Clean";
    public static string Build = "Build"; 
    public static string Test = "Test";
    public static string Package = "Package";

    public static string UploadArtifacts = "Upload-Artifacts";
    public static string UploadTestResults = "Upload-TestResults";

    public static string Publish = "Publish";
    public static string Default = "Default";
}

public class Configurations
{
    public static string Debug = "Debug";
    public static string Release = "Release";
}
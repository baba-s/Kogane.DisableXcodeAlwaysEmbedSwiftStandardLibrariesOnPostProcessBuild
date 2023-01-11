using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Kogane.Internal
{
    /// <summary>
    /// iOS ビルド完了時に Xcode プロジェクト UnityFramework の「Always Embed Swift Standard Libraries」を「No」にするエディタ拡張
    /// https://kan-kikuchi.hatenablog.com/entry/Always_Embed_Swift_Standard_Libraries
    /// https://forum.unity.com/threads/2019-3-validation-on-upload-to-store-gives-unityframework-framework-contains-disallowed-file.751112/
    /// </summary>
    internal static class DisableXcodeAlwaysEmbedSwiftStandardLibrariesOnPostProcessBuild
    {
        //================================================================================
        // 関数(static)
        //================================================================================
        /// <summary>
        /// ビルド完了時に呼び出されます
        /// </summary>
        [PostProcessBuild]
        private static void OnPostProcessBuild
        (
            BuildTarget buildTarget,
            string      pathToBuiltProject
        )
        {
            if ( buildTarget != BuildTarget.iOS ) return;

            var projectPath = PBXProject.GetPBXProjectPath( pathToBuiltProject );
            var project     = new PBXProject();

            const string name  = "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES";
            const string value = "NO";

            project.ReadFromFile( projectPath );
            project.SetBuildProperty( project.GetUnityFrameworkTargetGuid(), name, value );
            project.WriteToFile( projectPath );
        }
    }
}
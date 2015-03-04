# IST-446-Project

### Project Setup
1. Download Unity 5.
2. Clone this reposistory to your computer.
3. Open the project in Unity.
4. Set the editor to use Visible Meta Files (This is done by selecting Edit->Project Settings->Editor in the application 	menu and enabling External Version Control support by selecting Visible Meta 	Files in the dropdown for Version Control.)

### [Using External Version Control Systems with Unity](http://docs.unity3d.com/Manual/ExternalVersionControlSystemSupport.html)
Unity offers an Asset Server add-on product for easy integrated versioning of your projects and you can also use Perforce and PlasticSCM as external tools (see Version Control Integration for further details). If you for some reason are not able use these systems, it is possible to store your project in any other version control system, such as Subversion or Bazaar. This requires some initial manual setup of your project.

Before checking your project in, you have to tell Unity to modify the project structure slightly to make it compatible with storing assets in an external version control system. This is done by selecting Edit->Project Settings->Editor in the application menu and enabling External Version Control support by selecting Visible Meta Files in the dropdown for Version Control. This will show a text file for every asset in the Assets directory containing the necessary bookkeeping information required by Unity. The files will have a .meta file extension with the first part being the full file name of the asset it is associated with. Moving and renaming assets within Unity should also update the relevant .meta files. However, if you move or rename assets from an external tool, make sure to syncronize the relevant .meta files as well.

When checking the project into a version control system, you should add the Assets and the ProjectSettings directories to the system. The Library directory should be completely ignored - when using .meta files, it’s only a local cache of imported assets.

When creating new assets, make sure both the asset itself and the associated .meta file is added to version control.

### Links
[.gitignore source](http://answers.unity3d.com/questions/369755/unity-on-github.html)

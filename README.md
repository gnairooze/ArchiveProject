# ArchiveProject

Archive folder by copying files and subfolders of a source folder to a target folder and compress the target.

It uses a configuration file in the source folder to carry the path to the target folder, ignored subfolders list, whether or not to show a confirmation message.

## Sample

### Configuration

```xml
<configuration>
	<targetPath>full path to the target folder</targetPath>
	<attendedRun>false</attendedRun>
	<ignoreList>
		<ignore>bin</ignore>
		<ignore>obj</ignore>
		<ignore>.git</ignore>
		<ignore>.vs</ignore>
		<ignore>node_modules</ignore>
		<ignore>packages</ignore>
		<ignore>.jekyll-cache</ignore>
	</ignoreList>
</configuration>
```

### Command Line

```cmd
ArchiveProject.App "full path to the source folder"
```

To run the command from any path, add "fullpath\ArchiveProject.App.exe" to the user or system environment variable "Path".

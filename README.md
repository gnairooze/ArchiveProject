# ArchiveProject

Archive folder by copying files and subfolders of a source folder to a target folder and compress the target.

It uses a configuration file in the source folder to carry the path to the target folder, ignored subfolders list, whether or not to show a confirmation message.

## Sample Configuration

```xml
<configuration>
	<targetPath>f:\Data\My Projects\mssql-random</targetPath>
	<attendedRun>false</attendedRun>
	<ignoreList>
		<ignore>bin</ignore>
		<ignore>obj</ignore>
		<ignore>.git</ignore>
		<ignore>.vs</ignore>
		<ignore>node_modules</ignore>
	</ignoreList>
</configuration>
```

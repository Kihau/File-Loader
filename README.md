# File Loader
Simple C# program created to copy and override any type of files
## About program
- Program  automatically saves either destination path and added files after being closed
- Program saves and reads data from file named "dataset.xml"
- Data files with custom names can be created (File->Save), however they cannot be loaded automatically
- Program is available in two versions:
  - Self-contained - with all dlls pre-installed
  - Framework-dependent - without any .NET Core dlls
## How to use
1. Set destination path by clicking "Set new" button to open folder browser dialog (Destination->Set new) and select a folder
2. Click "Add" button and select the file you want to add to program list
3. In "File Editor" window enter file name that will be displayed in program list (doesn't affect real name of your file) and click "save" button
4. Click "Load" button in main window to copy/override your file in destination folder

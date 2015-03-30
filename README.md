### CosmosKernel
A command-line based operating system kernel.

Implemented functionalities include the running of batch files, several commands (listed below) and an integrated file system.
This kernel was created based on the CosmosOS user kit available at https://github.com/CosmosOS/Cosmos

Implemented commands
--


  <b>help</b>      - Displays the most common available options to the user.
  
  <b>echo</b>      - Prints the line following the <code>echo</code> keyword that was entered by the user.
  
  <b>time</b>      - Displays the current system time.
  
  <b>date</b>      - Displays the current system date.
  
  <b>dir</b>       - lists files/documents and sub-directories that are available inside the current directory as well as meta-data, which may include size and file-type.

  <b>run</b>       - runs the appropriate BATCH file(s) the following syntax is used <code>run [filename].bat</code> Running of multiple batch files is also supported using Round-Robin to run all files more efficiently. Similar syntax can be used to run multiple files by simply concatenating any other desired files to the end of the line.
  
  <b>create</b>    - creates the appropriate file based on the extension the following syntax is expected <code>create [filename].[ext]</code> whenever the extension ends in .bat or .txt, the user will be shown a series of lines which will be recorded into the file that will be created. This can be called to finish by tying 'save' alone in one line at any time during the creation of the file. 
  
  <b>mkdir</b>     - creates a new directory inside the current directory. By default the user is inside the home directory <code>C:\\\\</code>.
  
  <b>cd</b>       - changes to the specified directory if it exists, otherwise no change takes place. To navigate up the directory tree, the user may enter the following syntax "<code>cd ..</code>"

-
<b>This project was written in C#</b>
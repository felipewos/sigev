#ifndef MyAppVersion
#define MyAppVersion "1.0.0"
#endif

#define MyAppName "SIGEV"
#define MyAppFullName "SIGEV - Sistema Inteligente de Gestão da Evasão"
#define MyAppPublisher "Felipe Wagner"
#define MyAppExeName "PrimeiraTelaWinUI.exe"
#define MyAppIcon "..\AppAssets\Assets\SIGEV.ico"
#define MyAppSourceDir "..\dist\SIGEV-Distribuicao"

[Setup]
AppId={{CEB2C4EA-7919-4475-B287-6CDC66677F79}
AppName={#MyAppFullName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppFullName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL=https://github.com/felipewos/sadmat
AppSupportURL=https://github.com/felipewos/sadmat
AppUpdatesURL=https://github.com/felipewos/sadmat
DefaultDirName={localappdata}\Programs\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=..\dist
OutputBaseFilename=SIGEV-Setup-x64
SetupIconFile={#MyAppIcon}
UninstallDisplayIcon={app}\Assets\SIGEV.ico
UninstallDisplayName={#MyAppFullName}
VersionInfoCompany={#MyAppPublisher}
VersionInfoDescription=SIGEV Setup
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion=1.0.0.0
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=lowest
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

[Languages]
Name: "brazilianportuguese"; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"

[Tasks]
Name: "desktopicon"; Description: "Criar atalho na Área de Trabalho"; GroupDescription: "Atalhos:"; Flags: checkedonce

[Files]
Source: "{#MyAppSourceDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[InstallDelete]
Type: files; Name: "{localappdata}\Programs\SIGEV\Desinstalar SIGEV.bat"
Type: filesandordirs; Name: "{localappdata}\Programs\SADMAT"
Type: files; Name: "{autodesktop}\SADMAT.lnk"
Type: files; Name: "{autoprograms}\SADMAT.lnk"

[Registry]
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\SIGEV"; Flags: deletekey
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\SADMAT"; Flags: deletekey

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; WorkingDir: "{app}"; IconFilename: "{app}\Assets\SIGEV.ico"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; WorkingDir: "{app}"; IconFilename: "{app}\Assets\SIGEV.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Abrir o {#MyAppName}"; Flags: nowait postinstall skipifsilent

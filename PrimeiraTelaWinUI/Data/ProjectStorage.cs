using System;
using System.IO;
using System.Text;

namespace PrimeiraTelaWinUI.Data;

public static class ProjectStorage
{
	private const string CurrentAppFolderName = "SIGEV";

	private static string AppFolderPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CurrentAppFolderName);

	public static string ProjectsRootPath => Path.Combine(AppFolderPath, "Projects");

	public static string ProjectsFilePath => Path.Combine(AppFolderPath, "projects.json");

	public static string EnsureProjectFolder(string projectName, string? existingFolderPath)
	{
		if (!string.IsNullOrWhiteSpace(existingFolderPath))
		{
			Directory.CreateDirectory(existingFolderPath);
			return existingFolderPath;
		}
		return CreateProjectFolder(projectName);
	}

	public static string CreateProjectFolder(string projectName)
	{
		Directory.CreateDirectory(ProjectsRootPath);
		string folderName = BuildUniqueFolderName(projectName);
		string text = Path.Combine(ProjectsRootPath, folderName);
		Directory.CreateDirectory(text);
		return text;
	}

	public static string RenameProjectFolder(string? currentFolderPath, string newProjectName)
	{
		if (string.IsNullOrWhiteSpace(currentFolderPath) || !Directory.Exists(currentFolderPath))
		{
			return CreateProjectFolder(newProjectName);
		}
		Directory.CreateDirectory(ProjectsRootPath);
		string folderName = BuildUniqueFolderName(newProjectName, currentFolderPath);
		string targetFolderPath = Path.Combine(ProjectsRootPath, folderName);
		if (string.Equals(currentFolderPath, targetFolderPath, StringComparison.OrdinalIgnoreCase))
		{
			return currentFolderPath;
		}
		Directory.Move(currentFolderPath, targetFolderPath);
		return targetFolderPath;
	}

	public static void DeleteProjectFolder(string? folderPath)
	{
		if (!string.IsNullOrWhiteSpace(folderPath) && Directory.Exists(folderPath))
		{
			Directory.Delete(folderPath, recursive: true);
		}
	}

	private static string BuildUniqueFolderName(string projectName, string? folderPathToIgnore = null)
	{
		string baseName = SanitizeFolderName(projectName);
		string candidateName = baseName;
		int suffix = 2;
		while (true)
		{
			string candidatePath = Path.Combine(ProjectsRootPath, candidateName);
			if (!Directory.Exists(candidatePath) || string.Equals(candidatePath, folderPathToIgnore, StringComparison.OrdinalIgnoreCase))
			{
				break;
			}
			candidateName = $"{baseName} ({suffix++})";
		}
		return candidateName;
	}

	private static string SanitizeFolderName(string projectName)
	{
		string obj = (string.IsNullOrWhiteSpace(projectName) ? "Projeto" : projectName.Trim());
		char[] invalidChars = Path.GetInvalidFileNameChars();
		StringBuilder builder = new StringBuilder(obj.Length);
		string text = obj;
		foreach (char c in text)
		{
			builder.Append((Array.IndexOf(invalidChars, c) >= 0) ? '_' : c);
		}
		string sanitized = builder.ToString().Trim().TrimEnd('.', ' ');
		if (!string.IsNullOrWhiteSpace(sanitized))
		{
			return sanitized;
		}
		return "Projeto";
	}
}

using System.Diagnostics;

namespace Bau.Libraries.LibHelper.Processes;

/// <summary>
///		Clase de ayuda para el tratamiento de procesos (ejecutables)
/// </summary>
public class SystemProcessHelper
{
	/// <summary>
	///		Ejecuta la aplicación asociada a un tipo de documento
	/// </summary>
	public bool ExecuteApplicationForFile(string fileName, bool waitForExit, TimeSpan timeOut)
	{
		return ExecuteApplication(null, fileName, waitForExit, timeOut);
	}

	/// <summary>
	///		Ejecuta una aplicación pasándole los argumentos especificados
	/// </summary>
	public bool ExecuteApplication(string? executable, string arguments, bool waitForExit, TimeSpan timeOut, bool checkPrevious = false)
	{
		bool executed = false;

			// Inicializa los mensajes de salida y errores
			ProcessOutput = string.Empty;
			ProcessErrorOutput = string.Empty;
			ExecutionError = string.Empty;
			// Ejecuta la aplicación
			if (!checkPrevious || !CheckProcessing(executable))
			{
				Process process = new Process();

					// Inicializa las propiedades del proceso
					process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
					process.StartInfo.CreateNoWindow = true;
					// Habilitamos para que pueda lanzar el evento Exited
					process.EnableRaisingEvents = true;
					// Preparamos para recoger los datos del proceso
					process.StartInfo.UseShellExecute = false; // para poder leer los stream
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.StandardOutputEncoding = System.Text.Encoding.ASCII;
					process.StartInfo.RedirectStandardInput = true;
					process.StartInfo.RedirectStandardError = true;
					process.StartInfo.StandardErrorEncoding = System.Text.Encoding.ASCII;
					// Asigna los manejadores de eventos
					//process.OutputDataReceived += (sender, args) =>
					//									{
					//										if (!string.IsNullOrEmpty(args.Data))
					//											Console.WriteLine(args.Data);
					//									};
					//process.ErrorDataReceived += (sender, args) =>
					//									{
					//										if (!string.IsNullOrEmpty(args.Data))
					//											Console.WriteLine(args.Data);
					//									};
					// Asigna los argumentos del proceso
					if (string.IsNullOrEmpty(executable))
					{
						process.StartInfo.FileName = arguments ?? "";
						process.StartInfo.Arguments = "";
					}
					else
					{
						process.StartInfo.FileName = executable;
						process.StartInfo.Arguments = arguments ?? "";
					}
					// Ejecuta el proceso
					executed = process.Start();
					// Esperamos a que termine de ejecutar
					if (waitForExit && !process.WaitForExit((int) timeOut.TotalMilliseconds))
						ExecutionError = "End the execution process timeout";
					// Iniciamos la lectura de los buffer de salida y errores
					if (string.IsNullOrEmpty(ExecutionError)) // ... si no hemos salido por tiempo de espera
					{
						ProcessErrorOutput = process.StandardError.ReadToEnd();
						ProcessOutput = process.StandardOutput.ReadToEnd();
					}
			}
			// Devuelve el valor que indica si se ha ejecutado
			return executed;
	}

	/// <summary>
	///		Comprueba si se está procesando una aplicación
	/// </summary>
	public bool CheckProcessing(string? executable) => GetActiveProcesses(executable).Count > 0;

	/// <summary>
	///		Obtiene los procesos activos de un ejecutable
	/// </summary>
	public List<Process> GetActiveProcesses(string? executable)
	{
		List<Process> processes = new List<Process>();

			// Obtiene los procesos del ejecutable
			if (!string.IsNullOrEmpty(executable))
			{
				string name = Path.GetFileNameWithoutExtension(executable);

					// Recorre los procesos comprobando el nombre de archivo y/o el nombre de proceso
					foreach (Process process in Process.GetProcesses())
						if (process.ProcessName.Equals(name, StringComparison.CurrentCultureIgnoreCase) ||
								process.StartInfo.FileName.Equals(executable, StringComparison.CurrentCultureIgnoreCase))
							processes.Add(process);
			}
			// Devuelve la colección de procesos
			return processes;
	}

	/// <summary>
	///		Elimina un proceso de memoria
	/// </summary>
	public bool Kill(Process process, out string error)
	{ 
		// Inicializa los argumentos de salida
		error = "";
		// Elimina el proceso de memoria
		try
		{
			process.Kill();
		}
		catch (Exception exception)
		{
			error = exception.Message;
		}
		// Devuelve el valor que indica si se ha eliminado el proceso
		return string.IsNullOrWhiteSpace(error);
	}

	/// <summary>
	///		Obtiene los procesos del sistema
	/// </summary>
	public List<Process> GetProcesses()
	{
		List<Process> processes = new List<Process>();

			// Obtiene una lista de procesos
			foreach (Process process in Process.GetProcesses())
				processes.Add(process);
			// Devuelve la colección de procesos
			return processes;
	}

	/// <summary>
	///		Salida de la ejecución de un proceso
	/// </summary>
	public string ProcessOutput { get; private set; } = default!;

	/// <summary>
	///		Errores de ejecución de un proceso
	/// </summary>
	public string ProcessErrorOutput { get; private set; } = default!;

	/// <summary>
	///		Errores en la ejecución del proceso
	/// </summary>
	public string ExecutionError { get; private set; } = default!;
}

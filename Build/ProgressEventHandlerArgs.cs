using System;

namespace Bygg.Build
{
	public class ProgressEventHandlerArgs : EventArgs
	{
		public string Message { get; set; }

		public ProgressEventHandlerArgs(string message)
		{
			Message = message;
		}
	}
}
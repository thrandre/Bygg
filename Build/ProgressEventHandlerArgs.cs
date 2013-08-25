using System;

namespace Bygg.Build
{
	public class ProgressEventHandlerArgs : EventArgs
	{
		public string Message { get; set; }

		public ProgressEventHandlerArgs(String message)
		{
			Message = message;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AppCenter.Crashes;
using MrRondon.Services.Interfaces;

namespace MrRondon.Services
{
    public class ExceptionService : IExceptionService
    {
        public void TrackError(string messageError)
        {
            Debug.WriteLine("-------------START--------------");
            Debug.WriteLine(messageError);
            Debug.WriteLine("--------------END-----------------");
            Crashes.TrackError(null, new Dictionary<string, string> { { "error", messageError } });
        }

        public void TrackError(Exception e)
        {
            WriteError(e);
            Crashes.TrackError(e);
        }

        public void TrackError(Exception e, Dictionary<string, string> properties)
        {
            WriteError(e);
            Crashes.TrackError(e, properties);
        }

        private void WriteError(Exception ex)
        {
            if (ex == null) return;

            var error = ex.Message;
            Debug.WriteLine("-------------START--------------");

            var stackTrace = new StackTrace(ex, true);
            var errorFileName = stackTrace.GetFrame(0).GetFileName();
            if (!string.IsNullOrWhiteSpace(errorFileName)) Debug.WriteLine($"File: {errorFileName}");

            var errorLineNumber = stackTrace.GetFrame(0).GetFileLineNumber();
            if (errorLineNumber != 0) Debug.WriteLine($"Line: {errorLineNumber}");

            if (!string.IsNullOrWhiteSpace(error)) Debug.WriteLine(error);
            var detailError = ex.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detailError))
                if (!detailError.Equals(error))
                    Debug.WriteLine(detailError);

            var detailErrorFurther = ex.InnerException?.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detailErrorFurther))
                if (!detailErrorFurther.Equals(error) && !detailErrorFurther.Equals(detailError))
                    Debug.WriteLine(detailErrorFurther);

            Debug.WriteLine("--------------END-----------------");
        }
    }
}
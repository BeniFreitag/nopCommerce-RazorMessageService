using System;
using System.Linq;
using System.Text;
using Nop.Core.Domain.Logging;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Tasks;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Stores;
using Nop.Services.Tasks;
using ToSic.Nop.Plugins.RazorMessageService.Services;

namespace ToSic.Nop.Plugins.RazorMessageService.ScheduledTasks
{
	public class CompileRazorMessagesTask : ITask
	{
		private readonly ILogger _logger;
		private readonly ILanguageService _languageService;
		private readonly IMessageTemplateService _messageTemplateService;
		private readonly IStoreService _storeService;
		private readonly ISettingService _settingService;
		private const string EventLogMessageStart = "Compile RazorMessages started";
		private const string EventLogMessageCompleted = "Compile RazorMessages completed";
		private const string EventLogMessageError = "Compile RazorMessages completed with errors";

		public CompileRazorMessagesTask(ILogger logger, ILanguageService languageService, IMessageTemplateService messageTemplateService, IStoreService storeService, ISettingService settingService)
		{
			_logger = logger;
			_languageService = languageService;
			_messageTemplateService = messageTemplateService;
			_storeService = storeService;
			_settingService = settingService;
		}

		public void Execute()
		{
			var logMessage = new StringBuilder();
			var loggingEnabled = _settingService.GetSettingByKey("razormessageservice.compiletask.enablelogging", false);
			if (loggingEnabled)
				_logger.InsertLog(LogLevel.Debug, EventLogMessageStart, logMessage.ToString());

			try
			{
				var stores = _storeService.GetAllStores();
				var model = new DummyMessageModel();

				foreach (var store in stores)
				{
					var storeLanguages = _languageService.GetAllLanguages(storeId: store.Id);
					var activeMessageTemplates = _messageTemplateService.GetAllMessageTemplates(store.Id).Where(t => t.IsActive);
					model.Store = store;

					foreach (var messageTemplate in activeMessageTemplates)
					{
						foreach (var language in storeLanguages)
						{
							var subjectSuccess = false;
							var bodySuccess = false;

							try
							{
								logMessage.AppendFormat("{2:HH:mm:ss} Will parse template {0} (Id: {1}) Language {3} (Id: {4})\n", messageTemplate.Name, messageTemplate.Id, DateTime.Now, language.Name, language.Id);
								ParseTemplate(messageTemplate, language.Id, model, out subjectSuccess, out bodySuccess);
							}
							catch (Exception ex)
							{
								logMessage.AppendFormat("Failed: {0}\n", ex.Message);
							}

							logMessage.AppendFormat("Subject success: {0}, Body success: {1}\n", subjectSuccess, bodySuccess);
						}
					}
				}

				if (loggingEnabled)
					_logger.InsertLog(LogLevel.Debug, EventLogMessageCompleted, logMessage.ToString());
			}
			catch (Exception ex)
			{
				if (loggingEnabled)
					_logger.InsertLog(LogLevel.Error, EventLogMessageError, logMessage + ex.Message + " " + ex.StackTrace);
			}
		}

		private static void ParseTemplate(MessageTemplate messageTemplate, int languageId, dynamic model, out bool subjectSuccess, out bool bodySuccess)
		{
			var subject = messageTemplate.GetLocalized(mt => mt.Subject, languageId);
			var body = messageTemplate.GetLocalized(mt => mt.Body, languageId);

			RazorTemplateParser.ParseSafe(messageTemplate.Id, subject, model, out subjectSuccess);

			RazorTemplateParser.ParseSafe(messageTemplate.Id, body, model, out bodySuccess);
		}

		#region Add/Remove ScheduleTask to/from ScheduleTaskService
		internal static void EnsureScheduleTask(IScheduleTaskService scheduleTaskService)
		{
			string typeString;
			var task = GetScheduleTask(scheduleTaskService, out typeString);
			if (task == null)
			{
				scheduleTaskService.InsertTask(new ScheduleTask
				{
					Name = "Precompile RazorMessages",
					Seconds = 60,
					Enabled = true,
					Type = typeString
				});
			}
		}

		private static ScheduleTask GetScheduleTask(IScheduleTaskService scheduleTaskService, out string typeString)
		{
			var taskType = typeof(CompileRazorMessagesTask);
			typeString = taskType.FullName + ", " + taskType.Assembly.GetName().Name;
			return scheduleTaskService.GetTaskByType(typeString);
		}

		internal static void RemoveScheduleTask(IScheduleTaskService scheduleTaskService)
		{
			string typeString;
			var task = GetScheduleTask(scheduleTaskService, out typeString);
			if (task != null)
				scheduleTaskService.DeleteTask(task);
		}
		#endregion
	}
}

﻿// Copyright (C) 2014 Victor Baybekov

using System;
using System.Threading.Tasks;

namespace QuikSharp {

    public interface IServiceFunctions : IQuikFunctions {

        /// <summary>
        /// Функция возвращает путь, по которому находится файл info.exe, исполняющий данный скрипт, без завершающего обратного слэша («\»). Например, C:\QuikFront. 
        /// </summary>
        /// <returns></returns>
        Task<string> GetWorkingFolder();

        /// <summary>
        /// Функция предназначена для определения состояния подключения клиентского места к серверу. Возвращает «1», если клиентское место подключено и «0», если не подключено. 
        /// </summary>
        /// <returns></returns>
        Task<bool> IsConnected();

        /// <summary>
        /// Функция возвращает путь, по которому находится запускаемый скрипт, без завершающего обратного слэша («\»). Например, C:\QuikFront\Scripts 
        /// </summary>
        /// <returns></returns>
        Task<string> GetScriptPath();

        /// <summary>
        /// Функция возвращает значения параметров информационного окна (пункт меню Связь / Информационное окно…). 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> GetInfoParam(InfoParams param);

        /// <summary>
        /// Функция отображает сообщения в терминале QUIK.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="iconType"></param>
        /// <returns></returns>
        Task<bool> Message(string message, NotificationType iconType);

        Task<bool> PrintDbgStr(string message);
    }

    /// <summary>
    /// Service functions implementations
    /// </summary>
    public class ServiceFunctions : IServiceFunctions {
        public ServiceFunctions(int port) { QuikService = QuikService.Create(port); }

        public QuikService QuikService { get; private set; }

        public async Task<string> GetWorkingFolder() {
            var response = await QuikService.Send<StringMessage>(
                (new StringMessage("", "getWorkingFolder")));
            return response.Data;
        }

        public async Task<bool> IsConnected() {
            var response = await QuikService.Send<StringMessage>(
                (new StringMessage("", "isConnected")));
            return response.Data == "1";
        }

        public async Task<string> GetScriptPath() {
            var response = await QuikService.Send<StringMessage>(
                (new StringMessage("", "getScriptPath")));
            return response.Data;
        }

        public async Task<string> GetInfoParam(InfoParams param) {
            var response = await QuikService.Send<StringMessage>(
                (new StringMessage(param.ToString(), "getInfoParam")));
            return response.Data;
        }

        public async Task<bool> Message(string message, NotificationType iconType = NotificationType.Info) {
            switch (iconType) {
                case NotificationType.Info:
                    await QuikService.Send<StringMessage>(
                        (new StringMessage(message, "message")));
                    break;
                case NotificationType.Warning:
                    await QuikService.Send<StringMessage>(
                        (new StringMessage(message, "warning_message")));
                    break;
                case NotificationType.Error:
                    await QuikService.Send<StringMessage>(
                        (new StringMessage(message, "error_message")));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("iconType");
            }
            return true;
        }

        public async Task<bool> PrintDbgStr(string message) {
            await QuikService.Send<StringMessage>(
                (new StringMessage(message, "PrintDbgStr ")));
            return true;
        }
    }
}
